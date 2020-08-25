using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TurtleBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    float rangeToPlayerx2=0;
    Transform playerTran,chamThan;
    Animator animator;

    void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        chamThan = transform.GetChild(0);
        chamThan.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        
        Vector2 Posi1, Posi2;
        Posi1 = transform.position;
        Posi2 = playerTran.position;
        double temp = Math.Round(((Posi1.x - Posi2.x) * (Posi1.x - Posi2.x) + (Posi1.y - Posi2.y) * (Posi1.y - Posi2.y)), 1);
        rangeToPlayerx2 = float.Parse(temp.ToString());

    }



    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (rangeToPlayerx2 < 40 && !animator.GetBool("Attacking"))
        {
            animator.SetBool("AfterAttack", true);
            chamThan.gameObject.SetActive(true);

        }
        else if(rangeToPlayerx2 >40 && animator.GetBool("Attacking"))
        {
            animator.SetBool("BeforeAttack", true);
        }
    }
    public void isAttacking()
    {
        animator.SetBool("AfterAttack", false);
        animator.SetBool("Attacking", true);
    }
    public void stopAttack()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("BeforeAttack", false);
        chamThan.gameObject.SetActive(false);

    }
}
