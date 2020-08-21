using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public Sound[] sounds = SetSoundName();

    private Sound currentMusic;
    private float menuMusicPos = 0;

    void Awake()
    {
        BeSingleton();

        foreach (Sound s in sounds)
        {
            if (s.clip == null) continue;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        currentMusic = sounds[0];
    }

    private static Sound[] SetSoundName()
    {
        return new Sound[] {
            new Sound("Menu Music"),
            new Sound("Shop Music"),
            new Sound("Level 01 Music"),
            new Sound("Level 02 Music"),
            new Sound("Select Button"), // 4
            new Sound("Pause"),
            new Sound("Hit"),
            new Sound("Collide"),
            new Sound("Collect Coin"),
            new Sound("Jump")
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
            currentMusic = sounds[0];
        }
        else if (scene.name == "ShopScene")
        {
            currentMusic = sounds[1];
        }
        else if (scene.name == "Level_1")
        {
            currentMusic = sounds[2];
        }
        else if (scene.name == "Level_2")
        {
            currentMusic = sounds[3];
        }

        else if (scene.name == "HieuTestScene")
        {
            currentMusic = sounds[2]; ;
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

        sounds[4].source.Play();
    }
    public void PlayPauseSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        sounds[5].source.Play();
    }

    public void PlayHitSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;

        sounds[6].source.Play();
    }
    public void PlayCollideSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;
        if (sounds[7].source.isPlaying) return;
        if (sounds[9].source.isPlaying) sounds[9].source.Stop();

        sounds[7].source.Play();
    }
    public void PlayCollectCoinSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;
        if (sounds[8].source.isPlaying) return;

        sounds[8].source.Play();
    }
    public void PlayJumpSFX()
    {
        if (!GameController.instance.Data.AudioEnabled) return;
        // if (sounds[9].source.isPlaying) return;

        sounds[9].source.Play();
    }
}

[Serializable]
public class Sound
{
    [HideInInspector]
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1;
    [Range(0.1f, 3f)] public float pitch = 1;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;

    public Sound(string name)
    {
        this.name = name;
    }
}