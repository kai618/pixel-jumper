using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetector : MonoBehaviour
{
    private Player player;

    private Vector2 start = new Vector2(9999, 9999);
    private Vector2 distance = Vector2.zero;

    public readonly float minMagnitude = 0.5f;
    public readonly float maxMagnitude = 2f;

    private CurveRenderer cr;
    private ArcRenderer ar;

    private Jump jump;

    void Awake()
    {
        Input.multiTouchEnabled = false;
        player = GameObject.Find("Player").GetComponent<Player>();

        cr = GameObject.Find("Curve Renderer").GetComponent<CurveRenderer>();
        ar = GameObject.Find("Arc Renderer").GetComponent<ArcRenderer>();
    }

    void Update()
    {
        CheckUserTouch();
    }
    private void CheckUserTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                // return if the user touches an Event Object such as pause btn
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

                // using screen point to calculate distance allows independence on the position of player
                // this makes user able to do the trick of quick pre-jump
                // with pre-jump feature, user can swipe beforehand to prepare for a jump in the air
                // after player touching some collider, a curve will be drawn at once

                start = Input.touches[0].position;
                ar.SetOn(start);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (start.Equals(new Vector2(9999, 9999))) return;

                Vector2 end = Input.touches[0].position;
                distance = GetWorldDistance(start, end);

                jump = player.GenerateJump(distance);
                RenderCurve();
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                if (distance.Equals(Vector2.zero)) return;

                jump = player.GenerateJump(distance);
                RenderCurve();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (jump != null) player.SetJump(jump);

                Reset();
            }
            else if (touch.phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
    }


    private void RenderCurve()
    {
        if (jump != null) cr.RenderCurve(jump);
        else cr.SetOff();
    }

    private void Reset()
    {
        cr.SetOff();
        ar.SetOff();
        jump = null;
        start = new Vector2(9999, 9999);
        distance = Vector2.zero;
    }

    private Vector2 GetWorldDistance(Vector2 pixelStart, Vector2 pixelEnd)
    {
        Vector2 distance = Camera.main.ScreenToWorldPoint(pixelEnd) - Camera.main.ScreenToWorldPoint(pixelStart);

        // if the magnitude of the distance is too small or too big, clamp it according to the ratio
        if (distance.magnitude < minMagnitude)
        {
            distance *= minMagnitude / distance.magnitude;
        }
        else if (distance.magnitude > maxMagnitude)
        {
            distance *= maxMagnitude / distance.magnitude;
        }
        return distance;
    }
}


