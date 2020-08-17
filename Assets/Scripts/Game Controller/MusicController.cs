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
        if (GameController.instance.Data.AudioEnabled) StartBackgroundMusic();
    }

    public void StartBackgroundMusic()
    {

        // TODO: continue to play from previous play time
        Debug.Log("Background music started");

    }
    public void StopBackgroundMusic()
    {

        // TODO: store play time
        Debug.Log("Background music stopped");

    }
}
