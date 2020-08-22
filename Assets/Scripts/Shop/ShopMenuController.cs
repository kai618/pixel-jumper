using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenuController : MonoBehaviour
{
    public Text moneyText;

    public GameObject confirmPanel;
    public Text contentItem;
    public Text contentPrice;
    public Button yesBtn;
    public Button noBtn;

    public GameObject notEnoughMoneyPanel;
    public Button okBtn;

    void Start()
    {
        SetMoneyText();
    }

    void SetMoneyText()
    {
        moneyText.text = GameController.instance.Data.MoneyTotal.ToString();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowConfirmPanel(BuySkinButton button, int index, string title, int price)
    {
        if (price > GameController.instance.Data.MoneyTotal)
        {
            notEnoughMoneyPanel.SetActive(true);
            okBtn.onClick.RemoveAllListeners();
            okBtn.onClick.AddListener(() =>
            {
                notEnoughMoneyPanel.SetActive(false);
            });
        }
        else
        {
            confirmPanel.SetActive(true);

            yesBtn.onClick.RemoveAllListeners();
            noBtn.onClick.RemoveAllListeners();

            contentItem.text = "BUY " + title.ToUpper();
            contentPrice.text = "FOR " + price.ToString() + " COINS?";

            yesBtn.onClick.AddListener(() =>
            {
                GameController.instance.Data.BoughtSkins[index] = true;
                GameController.instance.Data.MoneyTotal -= price;
                GameController.instance.PersistData();
                confirmPanel.SetActive(false);
                SetMoneyText();
                button.SetSold();
            });

            noBtn.onClick.AddListener(() =>
            {
                confirmPanel.SetActive(false);
            });
        }
    }
}
