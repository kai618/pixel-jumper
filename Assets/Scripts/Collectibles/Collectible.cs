using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected CollectibleController cc;

    public int moneyValue { get; protected set; } = 0;

    protected void Start()
    {
        cc = GameObject.Find("Level Controller").GetComponent<CollectibleController>();
        cc.Register(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.tag;
        if (tag == "Player")
        {
            cc.AddMoney(moneyValue);

            Destroy(gameObject);
        }
    }
}
