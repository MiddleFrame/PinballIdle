using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEditor;
using Yodo1.MAS;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public static int choosenBall = 0;
    public Color32[] colorsBall = new Color32[] {Color.black, new Color32(0xB3,0xB3,0xB3,0xFF), new Color32(0xFF,0x89,0x12,0xFF) };

    
    public Sprite _lockedSprite;
    
    public Sprite _unlockedSprite;
    [SerializeField]
    private GameObject text;
    public static GameObject Text { get { return instance.text; } set { instance.text = value; } }

    Color mycolor = new Color(0.776f, 0.776f, 0.776f, 1.000f);


    private void Awake()
    {
        instance = this;
        Vibration.Init();
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
       
    }



    
    

    
    static public string NormalSum(long p)
    {
        string res;
        if (p + 1 >= 1000 && p < 10000)
            res = Math.Round((p) / 1000.0, 2, MidpointRounding.AwayFromZero) + "K";
        else if (p + 1 >= 10000 && p < 100000)
            res = Math.Round((p) / 1000.0, 1, MidpointRounding.AwayFromZero) + "K";
        else if (p + 1 >= 100000 && p < 1000000)
            res = (int)(p / 1000.0) + "K";
        else if (p + 1 >= 1000000 && p < 10000000)
            res = Math.Round(p / 1000000.0, 2, MidpointRounding.AwayFromZero) + "M";
        else if (p + 1 >= 10000000 && p < 100000000)
            res = Math.Round(p / 1000000.0, 1, MidpointRounding.AwayFromZero) + "M";
        else if (p + 1 >= 100000000 && p < 1000000000)
            res = Math.Round(p / 1000000.0) + "M";
        else if (p + 1 >= 1000000000 && p < 10000000000)
            res = Math.Round(p / 1000000000.0, 2, MidpointRounding.AwayFromZero) + "B";
        else if (p + 1 >= 10000000000 && p < 100000000000)
            res = Math.Round(p / 1000000000.0, 1, MidpointRounding.AwayFromZero) + "B";
        else if (p + 1 >= 100000000000)
            res = Math.Round(p / 1000000000.0) + "B";
        else
            res = "" + (p);
        return res;
    }



   
    public void TextDown(GameObject tx)
    {
        if (tx.GetComponent<Text>() == null || tx.GetComponent<Text>().text != "MAX")
        {
            tx.transform.position -= new Vector3(0, 0.08f, 0);
        }
    }

    public void TextUp(GameObject tx)
    {
        if(tx.GetComponent<Text>()==null || tx.GetComponent<Text>().text!="MAX")
        tx.transform.position += new Vector3(0, 0.08f, 0);
    }


}

