using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlantBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FireL;
    public GameObject FireR;
    Animator animator;
    void Start()
    {
        animator = transform.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void setAttack()
    {
        animator.SetBool("isAttack", true);
    }
    void setIdle()
    {
        //fire
        animator.SetBool("isAttack", false);
    }
    void Attack()
    {
        Vector2 v3 = transform.position;
        if (transform.localScale.x > 0)
        {
            v3.x += -0.2129382f;
            v3.y += 0.03266411f;
            Instantiate(FireL, v3, Quaternion.identity);
        }
        else
        {
            v3.x -= -0.132f;
            v3.y -= -0.058f;
            Instantiate(FireR, v3, Quaternion.identity);
        }
        
        
    }
}
