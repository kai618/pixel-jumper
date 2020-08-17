using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance != null) return;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartBackgroundMusic();
    }

    private void StartBackgroundMusic()
    {
        if (DataController.instance.data.AudioEnabled)
        {
            // TODO:
            Debug.Log("Playing background music");
        }
    }

}
