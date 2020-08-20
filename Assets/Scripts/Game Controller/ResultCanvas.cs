using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour
{
    public Text levelMoneySumText;
    public Text moneyTotalText;

    public Text levelDeathCountText;
    public Text deathTotalText;

    void Start()
    {

        CollectibleController cc = GameObject.Find("Level Controller").GetComponent<CollectibleController>();
        DeathController dc = GameObject.Find("Level Controller").GetComponent<DeathController>();
        GameData data = GameController.instance.Data;

        data.MoneyTotal += cc.levelMoneySum;
        data.deathCount += dc.levelDeathCount;
        GameController.instance.PersistData();

        levelMoneySumText.text = cc.levelMoneySum.ToString();
        moneyTotalText.text = data.MoneyTotal.ToString();

        levelDeathCountText.text = dc.levelDeathCount.ToString();
        deathTotalText.text = data.deathCount.ToString();
    }
}
