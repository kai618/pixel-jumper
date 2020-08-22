using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public Audio[] audios = SetAudios();

    private Audio currentMusic;
    private float menuMusicPos = 0;

    void Awake()
    {
        BeSingleton();

        foreach (Audio s in audios)
        {
            if (s.clip == null) continue;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        currentMusic = audios[0];
    }

    private static Audio[] SetAudios()
    {
        return new Audio[] {
            new Audio("Menu Music", 0.3f, true),
            new Audio("Shop Music", 0.3f, true),
            new Audio("Level 01 Music", 0.4f, true),
            new Audio("Level 02 Music", 0.3f, true),
            new Audio("Select Button", 0.1f), // 4
            new Audio("Pause", 0.5f),
            new Audio("Hit",0.8f),
            new Audio("Collide", 0.8f, false, 2),
            new Audio("Collect Coin", 0.5f),
            new Audio("Jump", 0.8f),
            new Audio("Spend Coin", 0.5f)
        };
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
        StopBackgroundMusic();

        if (scene.name == "MenuScene")
        {
            currentMusic = audios[0];
        }
        else if (scene.name == "ShopScene")
        {
            currentMusic = audios[1];
        }
        else if (scene.name == "Level_1")
        {
            currentMusic = audios[2];
        }
        else if (scene.name == "Level_2")
        {
            currentMusic = audios[3];
        }

        else if (scene.name == "HieuTestScene")
        {
            currentMusic = audios[2]; ;
        }

        if (GameController.instance.Data.AudioEnabled) StartBackgroundMusic();
    }

    public void StartBackgroundMusic()
    {
        if (!currentMusic.source.isPlaying)
        {
            if (currentMusic.name == "Menu Music") currentMusic.source.time = menuMusicPos;
            currentMusic.source.Play();
        }
    }


    public void StopBackgroundMusic()
    {
        if (currentMusic.source.isPlaying)
        {
            if (currentMusic.name == "Menu Music") menuMusicPos = currentMusic.source.time;
            currentMusic.source.Stop();
        }
    }
    public void PlaySelectSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        audios[4].source.Play();
    }
    public void PlayPauseSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        audios[5].source.Play();
    }

    public void PlayHitSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        audios[6].source.Play();
    }
    public void PlayCollideSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;
        if (audios[7].source.isPlaying) return;
        if (audios[9].source.isPlaying) audios[9].source.Stop();

        audios[7].source.Play();
    }
    public void PlayCollectCoinSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;
        if (audios[8].source.isPlaying) return;

        audios[8].source.Play();
    }
    public void PlayJumpSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;
        // if (sounds[9].source.isPlaying) return;

        audios[9].source.Play();
    }
    public void PlaySpendCoinSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        audios[10].source.Play();
    }
}

[Serializable]
public class Audio
{
    [HideInInspector]
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1;
    [Range(0.1f, 3f)] public float pitch = 1;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;

    public Audio(string name, float volume = 1f, bool loop = false, float pitch = 1f)
    {
        this.name = name;
        this.volume = volume;
        this.loop = loop;
        this.pitch = pitch;
    }
}