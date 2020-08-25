using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlantBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Fire;
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
        Vector3 v3 = transform.position;
        v3.x += -0.2129382f;
        v3.y += 0.03266411f;
        Instantiate(Fire,v3, Quaternion.identity);
    }
}
