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

    public GameObject firstRunText;

    public GameObject notFirstRunText;

    void Start()
    {
        CollectibleController cc = GameObject.Find("Level Controller").GetComponent<CollectibleController>();
        DeathController dc = GameObject.Find("Level Controller").GetComponent<DeathController>();
        GameData data = GameController.instance.Data;

        levelMoneySumText.text = cc.levelMoneySum.ToString();
        moneyTotalText.text = data.MoneyTotal.ToString();

        levelDeathCountText.text = dc.levelDeathCount.ToString();
        deathTotalText.text = data.DeathCount.ToString();
    }

    public void ShowFirstRunText()
    {
        firstRunText.SetActive(true);
    }

    public void ShowHNotFirstRunText()
    {
        notFirstRunText.SetActive(true);
    }
}
