using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : MonoBehaviour
{
    private String fileName = "/pixeljumper.data";
    public static DataController instance;

    private GameData data;

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
        finally
        {
            if (file != null) file.Close();
        }
    }
}

[Serializable]
class GameData
{
    bool firstRun;
    bool musicEnabled;
    int selectedPlayer;

    int moneyTotal;
    int deathCount;
    int gameLevel;
    bool[] boughtItems;
}
