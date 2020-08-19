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
}
