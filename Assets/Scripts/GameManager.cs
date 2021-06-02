using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Text pointSum;
    static public int Point = 0;
    static public int PointSum;

    [Serializable]
    class SaveData
    {
        public int PointSum;
    }
    private void Start()
    {
        LoadGame();
        
    }

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.PointSum = PointSum;
        bf.Serialize(file, data);
        file.Close();

    }

    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            PointSum = data.PointSum;
            pointSum.text = ""+PointSum;

        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
        else
            LoadGame();
    }
}