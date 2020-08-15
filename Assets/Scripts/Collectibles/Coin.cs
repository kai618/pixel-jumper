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
}
