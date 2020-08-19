using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameplayController gc;

    void Start()
    {
        gc = GameObject.Find("Level Controller").GetComponent<GameplayController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.tag;
        if (tag == "Player")
        {
            gc.FinishLevel();
        }
    }
}
