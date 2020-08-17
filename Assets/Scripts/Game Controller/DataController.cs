using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : MonoBehaviour
{
    private String fileName = "/pixeljumper.data";
    public static DataController instance;

    public GameData data;

    void Awake()
    {
        MakeSingleton();
        instance.Load();
    }

    private void MakeSingleton()
    {
        if (instance != null) return;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        FileStream file = null;
        try
        {
            file = File.Create(Application.persistentDataPath + fileName);

            BinaryFormatter bf = new BinaryFormatter();
            if (data != null) bf.Serialize(file, data);
            Debug.Log("Data Saved");
        }
        finally
        {
            if (file != null) file.Close();
        }
    }

    private void Load()
    {
        FileStream file = null;
        try
        {
            file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();
            data = (GameData)bf.Deserialize(file);
            Debug.Log("Data Loaded");
        }
#pragma warning disable 0168
        catch (FileNotFoundException e)
        {
            data = new GameData();
        }
#pragma warning disable 0168
        finally
        {
            if (file != null) file.Close();
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
