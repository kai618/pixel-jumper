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

    void Start() {
        PlayBgMusic();
    }

    private void PlayBgMusic() {

    }

    private void MakeSingleton()
    {
        if (instance != null) return;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
