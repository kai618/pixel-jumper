using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour
{
    private int collectibleCount = 0;
    public int moneySum { get; private set; } = 0;

    private Text moneySumText;

    void Start()
    {
        moneySumText = GameObject.Find("Money Sum").GetComponent<Text>();
    }

    public void Register(GameObject collectible)
    {
        collectibleCount++;
    }

    public void AddMoney(int value)
    {
        if (value > 0)
        {
            moneySum += value;
            RenderMoneySum();
        }
    }

    private void RenderMoneySum()
    {
        moneySumText.text = moneySum.ToString();
    }
}
