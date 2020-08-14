using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRenderer : MonoBehaviour
{
    private GameObject groundArc;
    private GameObject clingArc;

    private Player player;

    private new bool enabled;
    private Vector2 touchPosition;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        groundArc = GameObject.Find("Arc Ground");
        clingArc = GameObject.Find("Arc Cling");

        groundArc.SetActive(false);
        clingArc.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (enabled) RenderArc();
    }

    public void SetOn(Vector2 touchPos)
    {
        enabled = true;
        touchPosition = touchPos;

        RenderArc();
    }

    public void SetOff()
    {
        enabled = false;
        groundArc.SetActive(false);
        clingArc.SetActive(false);
    }

    private void RenderArc()
    {
        if (player.Grounded == player.Clinged) return;

        Vector2 rectPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        if (player.Grounded)
        {
            clingArc.SetActive(false);
            RectTransform rt = groundArc.GetComponent<RectTransform>();
            rt.position = rectPosition;

            groundArc.SetActive(true);
        }

        else if (player.Clinged)
        {
            groundArc.SetActive(false);
            float lsX = player.transform.localScale.x;

            // jump to the left
            if (lsX > 0)
            {
                RectTransform rt = clingArc.GetComponent<RectTransform>();
                rt.position = rectPosition;
                rt.localScale = new Vector3(-1, 1, 1);
            }
            // jump to the right
            else
            {
                RectTransform rt = clingArc.GetComponent<RectTransform>();
                rt.position = rectPosition;
                rt.localScale = new Vector3(1, 1, 1);
            }

            clingArc.SetActive(true);
        }
    }
}
