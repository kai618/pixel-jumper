using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnailBehaviourScript : MonoBehaviour
{
    Animator animator;
    float speed;
    Vector2 left, right,way;
    // Start is called before the first frame update
    void Start()
    {
        speed = -0.3f;
        animator = gameObject.GetComponent<Animator>();
        left = transform.parent.GetChild(0).transform.position;
        right = transform.parent.GetChild(1).transform.position;
        way = left;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("canMove"))
        {
            if (GoPosition(way))
            {
                animator.SetBool("canMove", false);
            }
        }
    }
    void CheckFace()
    {
        Vector2 v2;
        v2 = transform.localScale;
        if (speed < 0)
            v2.x = math.abs(v2.x);
        else
            v2.x = math.abs(v2.x)*-1;
        transform.localScale = v2;
    }
    public Vector2 FindWay()
    {
        if (transform.position.x <= left.x)
            way= right;
        
        if (transform.position.x >= right.x)
            way= left;

        animator.SetBool("canMove", true);
        speed *= -1;
        CheckFace();
        return way;
    }
    bool GoPosition(Vector2 position)
    {
        if (speed > 0 && transform.position.x >= position.x)
            return true;
        if (speed < 0 && transform.position.x < position.x)
            return true;
        Vector2 v2 = transform.position;
        v2.x += Time.deltaTime * speed;
        transform.position = v2;
        return false;
    }

}
