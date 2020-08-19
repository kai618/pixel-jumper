using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        pos.y = player.position.y;
        transform.position = pos;
    }
}
