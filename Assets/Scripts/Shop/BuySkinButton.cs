using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkinButton : MonoBehaviour
{
    public int skinIndex;
    public int price;
    public string title;

    public GameObject moneyText;
    public GameObject soldText;
    public Text titleText;
    public Text titleTextBack;
    public Text priceText;

    private Button button;

    private ShopMenuController sc;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        sc = GameObject.FindObjectOfType<ShopMenuController>();

        titleText.text = title;
        titleTextBack.text = title;
        priceText.text = price.ToString();
        if (GameController.instance.Data.BoughtSkins[skinIndex]) SetSold();

        button.onClick.AddListener(() => { sc.ShowConfirmPanel(this, skinIndex, title, price); });
    }

    public void SetSold()
    {
        moneyText.SetActive(false);
        soldText.SetActive(true);
        button.interactable = false;
    }
}
