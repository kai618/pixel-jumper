using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class PigBehaviourScript : MonoBehaviour
{
    float speed = -1;
    Vector2 firstPosi,ungroundL,ungroundR;
    Transform playerTran;
    float rangeToPlayerx2 = 0;
    Animator animator;
    Transform chamThan, chamHoi;

    bool canAttack=false,goBack=false,isAttacking=false,isLimit=false;
    void Start()
    {
        
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        firstPosi = transform.position;
        animator = transform.GetComponent<Animator>();

        chamThan = transform.parent.GetChild(0);
        chamThan.gameObject.SetActive(false);

        chamHoi = transform.parent.GetChild(1);
        chamHoi.gameObject.SetActive(false);

        ungroundL = transform.parent.GetChild(2).position;
        ungroundR = transform.parent.GetChild(3).position;


    }

    void Update()
    {
        BehaviourOfFuckingPig();
    }

    void BehaviourOfFuckingPig()
    {
        if (isLimit && canAttack)
        {
            chamHoi.gameObject.SetActive(true);
            chamThan.gameObject.SetActive(false);
            animator.SetBool("isAttack", false);
        }
        else if (isLimit && !canAttack)
        {
            StartCoroutine(WaitingAfterBack(1.5f));

        }
        if (canAttack && !isLimit)//trong zone
        {
            if (!animator.GetBool("isAttack"))//dang dung yen
            {
                //wait
                StartCoroutine(WaitingAfterAttack(1.5f));
                //isAttacking = true;
                //animator.SetBool("isAttack", true);
            }
            else//dang di chuyen
            {
                if (isAttacking)//dang tan cong
                {
                    chamThan.gameObject.SetActive(true);
                    GoPosition(playerTran.position);
                }
                else//dang quay ve
                {
                    animator.SetBool("isAttack", false);
                }
            }
        }
        else
        {
            if (isAttacking)
            {
                //wait
                isAttacking = false;
                StartCoroutine(WaitingAfterBack(1.5f));              
                //chamThan.gameObject.SetActive(false);
                //isAttacking = false;
                //GoPosition(firstPosi);
            }
            //Cham Grounded
            //else if {}
            else
            {
                if (!CheckFirstPosi())
                    GoPosition(firstPosi);
                else
                {
                    animator.SetBool("isAttack", false);
                    Vector2 v2 = transform.localScale;
                    v2.x = math.abs(v2.x);
                    transform.localScale = v2;
                    
                }
            }
        }
    }

    private void FixedUpdate()
    {
        
        Vector2 Posi1, Posi2;
        Posi1 = transform.position;
        Posi2 = playerTran.position;
        
        double temp = Math.Round( ((Posi1.x - Posi2.x) * (Posi1.x - Posi2.x)), 1);
        rangeToPlayerx2 = float.Parse(temp.ToString());
        canAttack = CheckPlayerInZone();

        chamHoi.position = new Vector2(transform.position.x, chamHoi.position.y);
        chamThan.position = new Vector2(transform.position.x,chamThan.position.y);

        isLimit = CheckIsLimit();
    }

    IEnumerator WaitingAfterAttack(float seconds)
    {
        chamHoi.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);
        isAttacking = true;
        animator.SetBool("isAttack", true);
        chamHoi.gameObject.SetActive(false);
    }
    IEnumerator WaitingAfterBack(float seconds)
    {
        chamThan.gameObject.SetActive(false);
        animator.SetBool("isAttack", false);
        chamHoi.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        chamHoi.gameObject.SetActive(false);
        //isAttacking = false;
        animator.SetBool("isAttack", true);
        GoPosition(firstPosi);
    }

    bool CheckPlayerInZone()
    {
        Vector2 player = playerTran.position;

        if (player.y < (transform.position.y + 1.3f) && player.y > (transform.position.y - 1f))
        {
            if (rangeToPlayerx2 > 40)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
        
    }
    void CheckFace()
    {
        Vector2 v2;
        v2 = transform.localScale;
        if (speed < 0)
            v2.x = math.abs(v2.x);
        else
            v2.x = math.abs(v2.x) * -1;
        transform.localScale = v2;

    }
    bool GoPosition(Vector2 position)
    {
        if (transform.position.x > position.x)
            speed = -1;
        else
            speed = 1;
        if (speed > 0 && transform.position.x >= position.x)
            return true;
        if (speed < 0 && transform.position.x < position.x)
            return true;

        CheckFace();
        Vector2 v2 = transform.position;
        v2.x += Time.deltaTime * speed;
        transform.position = v2;
        return false;
    }
    bool CheckFirstPosi()
    {
        Vector2 v2 = transform.position;
        if ((speed > 0 && v2.x > firstPosi.x) || (speed < 0 && v2.x < firstPosi.x))
            return true;
        return false;
    }
    bool CheckIsLimit()
    {
        Vector2 v2 = transform.position;
        if (v2.x > ungroundR.x || v2.x < ungroundL.x)
            return true;
        return false;

    }
}
