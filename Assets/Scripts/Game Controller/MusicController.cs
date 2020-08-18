using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    private AudioSource menuMusic;
    private float menuMusicPos = 0;

    void Awake()
    {
        BeSingleton();

        AudioSource[] audioSources = GetComponents<AudioSource>();

        menuMusic = audioSources[0];
    }

    private void BeSingleton()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        if (GameController.instance.Data.AudioEnabled) StartBackgroundMusic();


        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        if (scene.name == "MainScene")
        {
            Debug.Log("Menu music played");
        }

        else if (scene.name == "Level01")
        {
            Debug.Log("Level 01 music played");
        }
    }

    public void StartBackgroundMusic()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.time = menuMusicPos;
            menuMusic.Play();
        }

    }
    public void StopBackgroundMusic()
    {
        if (menuMusic.isPlaying)
        {
            menuMusicPos = menuMusic.time;
            menuMusic.Stop();
        }
    }
}
