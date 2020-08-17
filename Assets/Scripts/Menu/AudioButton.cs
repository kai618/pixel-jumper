using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Sprite[] spriteAudioButtons;   // [0] on, [1] off
#pragma warning disable 0649

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        if (GameController.instance.Data.AudioEnabled) image.sprite = spriteAudioButtons[0];
        else image.sprite = spriteAudioButtons[1];
    }

    public void SwitchAudio()
    {
        if (GameController.instance.Data.AudioEnabled)
        {
            GameController.instance.DisableAudio();
            image.sprite = spriteAudioButtons[1];
        }
        else
        {
            GameController.instance.EnableAudio();
            image.sprite = spriteAudioButtons[0];
        }
    }
}
