using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifghtBehaviourScript : MonoBehaviour
{
    float speed = 3;
    GameObject fire1;
    Animator fire2;

    void Start()
    {
        fire1 = transform.GetChild(0).gameObject;
        fire2 = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!fire2.GetBool("isEx"))
            Fly();

        if (fire2.GetBool("isDetroy"))
            Destroy(gameObject);

    }
    void Fly()
    {

        Vector2 v2 = transform.position;
        v2.x += Time.deltaTime * speed;
        transform.position = v2;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LimitOfPlantAttack")
        {
            Destroy(fire1);
            fire2.SetBool("isEx", true);

        }
    }



}
