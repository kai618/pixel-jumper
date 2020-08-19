using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible
{
    public readonly new int moneyValue = 5;

    new void Start()
    {
        base.Start();
        base.moneyValue = moneyValue;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.tag;
        if (tag == "Player")
        {
            cc.AddMoney(moneyValue);

            AudioController.instance.PlayCollectCoinSFX();
            Destroy(gameObject);
        }
    }
}
