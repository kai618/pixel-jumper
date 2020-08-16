using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveRenderer : MonoBehaviour
{

    public readonly float segmentWidth = 0.4f;

    private LineRenderer lr;
    private float g; // gravity force

    private Player player;
    private Vector3 originPos;
    private string ignoreTags = "Player Collectible";

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
        originPos = player.transform.position;

        // set the starting point of the curve to the player's legs
        originPos.y -= 0.5f;
        transform.position = originPos;
    }

    public void RenderCurve(Jump jump)
    {
        SetOn();
        Vector3[] posArr = CalculatePositionArray(jump);
        lr.positionCount = posArr.Length;
        lr.SetPositions(posArr);
    }

    // Ref:https://en.wikipedia.org/wiki/Projectile_motion
    private Vector3[] CalculatePositionArray(Jump jump)
    {
        float v = jump.velocity.magnitude;
        float t = jump.angle * Mathf.Deg2Rad;

        List<Vector3> posList = new List<Vector3> { Vector3.zero };

        // for vertical jump, x = 0
        if (jump.type == JumpType.GROUND_VERTICAL)
        {
            float h = v * v / (2 * g);
            Vector3 hit = new Vector3(0, h);

            int segmentCount = 6;
            for (int i = 0; i < segmentCount; i++)
            {
                Vector3 start = new Vector3(0, h * i / segmentCount);
                Vector3 end = new Vector3(0, h * (i + 1) / segmentCount);

                Vector3? hitPos = GetRelativeHitPosition(start, end, originPos);

                if (hitPos != null)
                {
                    hit = (Vector3)hitPos; // convert to relative position
                    break;
                }
            }
            posList.Add(hit);
        }
        else
        {
            float a = Mathf.Tan(t);
            float b = -g / (2 * v * v * Mathf.Cos(t) * Mathf.Cos(t));

            // find the end position (hitPos) of the guiding curve
            Vector3? hitPos = null;
            float step = GetSuitableStep(jump, segmentWidth);
            int i = 1;

            while (true)
            {
                float x2 = i * step;
                float y2 = a * x2 + b * x2 * x2;

                Vector3 start = posList[i - 1];
                Vector3 end = new Vector3(x2, y2);
                hitPos = GetRelativeHitPosition(start, end, originPos);

                if (hitPos == null) posList.Add(end);
                else
                {
                    posList.Add((Vector3)hitPos);
                    break;
                }
                i++;
            }
        }
        return posList.ToArray();
    }

    private float GetSuitableStep(Jump jump, float segmentWidth)
    {
        float step = (jump.velocity.x > 0) ? segmentWidth : -segmentWidth;

        // decrease [step] when the angle is large
        float ratio = Mathf.Abs((jump.velocity.x / jump.velocity.y));
        if (ratio < 0.5) step *= ratio * 1.2f;
        // or the velocity is too small
        if (Mathf.Abs(jump.velocity.x) < 10f) step *= 0.75f;
        if (jump.velocity.magnitude < 10f) step *= 0.5f;
        return step;
    }

    public static float CalculateAngle(Vector2 distance)
    {
        if (distance.x == 0) return 90;

        float angle = Vector2.Angle(distance, new Vector2(distance.x, 0));
        if (distance.y >= 0)
        {
            if (distance.x < 0) angle = 180 - angle;
        }
        else
        {
            if (distance.x >= 0) angle = -angle;
            else if (distance.x < 0) angle -= 180;
        }
        return angle;
    }

    private Vector3? GetRelativeHitPosition(Vector2 relativeStart, Vector2 relativeEnd, Vector2 origin)
    {
        RaycastHit2D hit = Physics2D.Linecast(relativeStart + origin, relativeEnd + origin);

        if (hit.collider == null || ignoreTags.Contains(hit.collider.tag))
        {
            return null;
        }
        return hit.point - origin;
    }
}
