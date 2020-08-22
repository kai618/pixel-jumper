using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    // we ignore level 01 and start from level 2
    public Button[] levelButtons = new Button[3];
    void Start()
    {
        EnableLevelButtons();
    }

    private void EnableLevelButtons()
    {
        int reachedLevel = GameController.instance.Data.ReachedLevel - 1;

        for (int i = 0; i < reachedLevel; i++)
        {
            levelButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            levelButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            levelButtons[i].interactable = true;
        }
    }
}
