using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveRenderer : MonoBehaviour
{

    public readonly float segmentWidth = 0.4f;

    private LineRenderer lr;
    private float g; // gravity force

    private Player player;
    private Vector3 startPos;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        lr = GetComponent<LineRenderer>();
        lr.sortingLayerName = "Foreground";
        lr.sortingOrder = 4;
        lr.useWorldSpace = false;

        g = Mathf.Abs(Physics2D.gravity.y * player.GetComponent<Rigidbody2D>().gravityScale);
    }

    void Start()
    {
        SetOff();
    }

    public void SetOn()
    {
        SetPosition();
        gameObject.SetActive(true);
    }

    public void SetOff()
    {
        gameObject.SetActive(false);
    }

    private void SetPosition()
    {
        startPos = player.transform.position;

        // set the starting point of the curve to the player's legs
        startPos.y -= 0.5f;

        transform.position = startPos;
    }

    public void RenderCurve(Vector2 velocity)
    {
        SetOn();
        Vector3[] posArr = CalculatePositionArray(velocity);
        lr.positionCount = posArr.Length;
        lr.SetPositions(posArr);
    }

    private Vector3[] CalculatePositionArray(Vector2 velocity)
    {
        float v = velocity.magnitude;
        float t = CalculateAngle(velocity) * Mathf.Deg2Rad;

        List<Vector3> posList = new List<Vector3> { Vector3.zero };

        // for vertical jump, x = 0
        if (t == 90 * Mathf.Deg2Rad)
        {
            float h = v * v / (2 * g);
            int segment = 6;
            Vector3 end = new Vector3(0, h);

            for (int i = 0; i < segment; i++)
            {
                Vector3 hitPos = GetHitCollider(startPos + new Vector3(0, h * i / segment), startPos + new Vector3(0, h * (i + 1) / segment));
                if (hitPos != startPos)
                {
                    end = hitPos - startPos;
                    break;
                }
            }
            posList.Add(end);
        }
        else
        {
            float a = Mathf.Tan(t);
            float b = -g / (2 * v * v * Mathf.Cos(t) * Mathf.Cos(t));

            // find the end position (hitPos) of the guiding curve
            Vector3 hitPos = startPos;
            float x1 = 0.0f, y1;
            float x2, y2;
            float diff = (velocity.x > 0) ? segmentWidth : -segmentWidth;

            // decrease [diff] when the angle is large
            float ratio = Mathf.Abs((velocity.x / velocity.y));
            if (ratio < 0.5) diff *= ratio * 1.2f;
            // or the velocity is too small
            if (Mathf.Abs(velocity.x) < 10f) diff *= 0.75f;
            if (v < 10f) diff *= 0.5f;

            while (hitPos == startPos)
            {
                y1 = a * x1 + b * x1 * x1;

                x2 = x1 + diff;
                y2 = a * x2 + b * x2 * x2;

                // if the curve and a collider2D collides, break
                hitPos = GetHitCollider(startPos + new Vector3(x1, y1), startPos + new Vector3(x2, y2));
                posList.Add(new Vector3(x2, y2));
                x1 += diff;
            }
        }

        return posList.ToArray();
    }

    public static float CalculateAngle(Vector2 distance)
    {

        float angle = (distance.x == 0) ? 90 : Vector2.Angle(distance, new Vector2(distance.x, 0));

        if (distance.y >= 0)
        {
            if (distance.x < 0) angle = 180 - angle;
        }
        else
        {
            if (distance.x >= 0) angle = -angle;
            if (distance.x < 0) angle -= 180;
        }

        return angle;
    }

    private Vector3 GetHitCollider(Vector2 pos1, Vector2 pos2)
    {
        RaycastHit2D hit = Physics2D.Linecast(pos1, pos2);

        if (hit.collider == null || hit.collider.tag == "Player")
        {
            return startPos;
        }

        //string hitTag = hit.collider.tag;
        //if (!tags.Contains(hitTag)) return startPos;
        //Debug.Log(hit.collider.tag + " - " + hit.point);

        return hit.point;
    }
}
