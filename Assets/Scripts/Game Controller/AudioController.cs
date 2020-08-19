using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public Sound[] sounds = SetSoundName();

    private AudioSource menuMusic;
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

        menuMusic = sounds[0].source;
    }

    private static Sound[] SetSoundName()
    {
        return new Sound[] {
            new Sound("Menu Music"),
            new Sound("Shop Music"),
            new Sound("Level 01 Music"),
            new Sound("Level 02 Music"),
            new Sound("Select Button"),
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