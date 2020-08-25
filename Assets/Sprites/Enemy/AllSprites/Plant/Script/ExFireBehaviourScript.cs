using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExFireBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Exfire()
    {
        //Destroy(gameObject);
        animator.SetBool("isEx", false);
    }
    void isDetroy()
    {
        animator.SetBool("isDetroy", true);

    }
}
