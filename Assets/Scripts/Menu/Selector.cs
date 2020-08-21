using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public Button[] selectorBtns = new Button[3];
    public Color visibleColor;
    public Color invisibleColor;

    void Start()
    {
        bool[] skins = GameController.instance.Data.BoughtSkins;
        int boughtSkinCount = 0;
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i])
            {
                int btnIndex = i + 1;
                selectorBtns[btnIndex].gameObject.SetActive(true);
                selectorBtns[btnIndex].onClick.AddListener(() => { OnSelectSkin(btnIndex); });
                boughtSkinCount++;
            }
        }

        if (boughtSkinCount > 0)
        {
            selectorBtns[0].gameObject.SetActive(true);
            selectorBtns[0].onClick.AddListener(() => { OnSelectSkin(0); });

            int selectedPlayer = GameController.instance.Data.SelectedSkin;
            Debug.Log(GameController.instance.Data.SelectedSkin);
            selectorBtns[selectedPlayer].image.color = visibleColor;
        }
    }

    private void OnSelectSkin(int index)
    {
        if (index == GameController.instance.Data.SelectedSkin) return;
        Debug.Log("changed to skin " + index);
        AudioController.instance.PlaySelectSFX();

        GameController.instance.Data.SelectedSkin = index;
        GameController.instance.PersistData();

        for (int i = 0; i < selectorBtns.Length; i++)
        {
            if (i == index) selectorBtns[i].image.color = visibleColor;
            else selectorBtns[i].image.color = invisibleColor;
        }
    }
}
