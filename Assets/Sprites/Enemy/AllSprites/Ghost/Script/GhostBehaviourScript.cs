using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviourScript : MonoBehaviour
{
    float rangeToPlayerx2 = 0;
    Transform playerTran, chamThan,chamHoi;
    Animator animator;

    void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        chamThan = transform.GetChild(0);
        chamHoi = transform.GetChild(1);

        chamThan.gameObject.SetActive(false);
        //chamHoi.gameObject.SetActive(false);

    }


    private void FixedUpdate()
    {

        Vector2 Posi1, Posi2;
        Posi1 = transform.position;
        Posi2 = playerTran.position;
        double temp = Math.Round(((Posi1.x - Posi2.x) * (Posi1.x - Posi2.x) + (Posi1.y - Posi2.y) * (Posi1.y - Posi2.y)), 1);
        rangeToPlayerx2 = float.Parse(temp.ToString());
        //Debug.Log(rangeToPlayerx2);
    }

    void LateUpdate()
    {
        GhostBehavious();
    }
    void GhostBehavious()
    {
        if (rangeToPlayerx2 > 30)
        {
            animator.SetBool("isAppear", false);
        }
        else
        {
            chamHoi.gameObject.SetActive(false);
            animator.SetBool("isAppear", true);
        }
    }

    void setIdle()
    {
        animator.SetBool("inZone", true);
        chamThan.gameObject.SetActive(true);
    }
    void setHide()
    {
        animator.SetBool("inZone", false);
        chamThan.gameObject.SetActive(false);
        chamHoi.gameObject.SetActive(true);
    }
}
