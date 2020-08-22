using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBehaviourScript : MonoBehaviour
{
    Transform playerTransform;
    Renderer m0, m1, m2, m3;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        //m0 = GameObject.Find("Quad1").GetComponent<Renderer>();
        m1 = GameObject.Find("Quad1").GetComponent<Renderer>();
        m2 = GameObject.Find("Quad2").GetComponent<Renderer>();
        m3 = GameObject.Find("Quad3").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v2 = playerTransform.position;
        v2.z = 1;
        transform.position = v2;

        //Vector2 offset = new Vector2(playerTransform.position.x * 0.009f, 0);
        //m0.material.mainTextureOffset = new Vector2(v2.x * 0.001f, v2.y * 0.0006f);
        m1.material.mainTextureOffset = new Vector2(v2.x * 0.001f, v2.y * 0.0006f);
        m2.material.mainTextureOffset = new Vector2(v2.x * 0.002f, v2.y * 0.0006f);
        m3.material.mainTextureOffset = new Vector2(v2.x * 0.003f, v2.y * 0.0006f);


        //offset = new Vector2(playerTransform.position.x * 0.009f, 0);
        //m1.material.mainTextureOffset = offset;
        //offset = new Vector2(playerTransform.position.x * 0.009f, 0);
        //m1.material.mainTextureOffset = offset;



    }
}
