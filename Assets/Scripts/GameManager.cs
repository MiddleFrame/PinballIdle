using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Text pointSum;
    public GameObject panelLeft;
    public GameObject panelRight;
    static public int Point = 0;
    static public long PointSum;
    static public bool automod=false;
    [Serializable]
    class SaveData
    {
        public long PointSum;
    }
    private void Start()
    {
        LoadGame();
        
    }
    private void Awake()
    {
        LoadGame();

    }

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/Save.dat");
        SaveData data = new SaveData
        {
            PointSum = PointSum
        };
        bf.Serialize(file, data);
        file.Close();

    }

    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/Save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/Save.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            PointSum = data.PointSum;
            pointSum.text = LetsScript.NormalSum();

        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
        else
            LoadGame();
    }

    public void Button6()
    {
        panelLeft.SetActive(automod);
        panelRight.SetActive(automod);

        automod = !automod;
    }
}