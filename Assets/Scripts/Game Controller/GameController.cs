using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    private String fileName = "/pixeljumper.data";
    public static GameController instance;

    public GameData Data { get; private set; }

    void Awake()
    {
        BeSingleton();
    }

    private void BeSingleton()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            instance.FetchData();
        }
    }

    public void PersistData()
    {
        FileStream file = null;
        try
        {
            file = File.Create(Application.persistentDataPath + fileName);

            BinaryFormatter bf = new BinaryFormatter();
            if (Data != null) bf.Serialize(file, Data);
            Debug.Log("Data Saved");
        }
        finally
        {
            if (file != null) file.Close();
        }
    }

    private void FetchData()
    {
        FileStream file = null;
        try
        {
            file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();
            Data = (GameData)bf.Deserialize(file);
            Debug.Log("Data Loaded");
        }
#pragma warning disable 0168
        catch (FileNotFoundException e)
        {
            Data = new GameData();
        }
#pragma warning disable 0168
        finally
        {
            if (file != null)
            {
                file.Close();
                PersistData();
            }
        }
    }

    public void EnableAudio()
    {
        if (!Data.AudioEnabled)
        {
            Data.AudioEnabled = true;
            AudioController.instance.StartBackgroundMusic();
            PersistData();
        }
    }

    public void DisableAudio()
    {
        if (Data.AudioEnabled)
        {
            Data.AudioEnabled = false;
            AudioController.instance.StopBackgroundMusic();
            PersistData();
        }
    }
}

[Serializable]
public class GameData
{
    public bool FirstRun { get; set; }
    public bool AudioEnabled { get; set; }
    public int SelectedPlayer { get; set; }

    public int MoneyTotal { get; set; }
    public int deathCount { get; set; }
    public int GameLevel { get; set; }
    public bool[] BoughtItems { get; set; }

    public GameData()
    {
        FirstRun = true;
        AudioEnabled = true;
        SelectedPlayer = 1;
        MoneyTotal = 0;
        deathCount = 0;
        GameLevel = 1;
        BoughtItems = new bool[10];
    }
}
