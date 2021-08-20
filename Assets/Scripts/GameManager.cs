using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    //
    static public int maximumPoint=0;
    static public bool ExNum = false;
    public Text pointSum;
    public GameObject Attention;
    public Text point;
    public Text StandartCost;
    public Text StandartAfkCost;
    public Text AfkPoints;
    public Text AfkPointsLast;
    public Text AfkPointText;
    public Text AfkPointTime;
    public Text Auto_flipper_text;
    public Text PointBuffText;
    public Text PointBuffAfkText;
    public Text CostResetText;
   // public Text PointAfkText;
    public TimeSpan date;
    Color mycolor;
   public GameObject buttonBuyAutomod;
    public GameObject BackPanel;
    public HingeJoint2D hj1;
    public GameObject textError;
    public GameObject textError2;
    public GameObject Automod_slider;
    public HingeJoint2D hj2;
    public GameObject panelLeft;
    public GameObject panelRight;
    public GameObject Shop4;
    public GameObject Shop3;
    public GameObject Shop2;
    public GameObject Shop1;
    public GameObject button4;
    public GameObject button3;
    public GameObject button2;
    public GameObject button1;
    public GameObject Shop6;
    public GameObject button6;
    public GameObject NumberBut;
    public int CostReset = 20000;
    static public int Point = 0;
    static public long PointSum;
    static public bool automod=false;
    public GameObject AfkMenu;
    [Serializable]
    class SaveData
    {
        public long PointSum;
        public bool isBuyAutomod;
        public int CostOfGrade;
        public int CostOfAfk;
        public int PointOnBit;
        public int PointOnAfk;
        public int CountBall;
        public int MaximumPoint;
    }

    class SaveQuest
    {
        public bool[] QuestCompleate;
    }
    private void Start()
    {
        if (Advertisement.isSupported)
            Advertisement.Initialize("4265503", false);

     
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        mycolor = button2.GetComponent<Image>().color;
        button2.GetComponent<Image>().color = Color.white;
        PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
        PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
    }

    void SaveGame()
    {
        PlayerPrefs.SetString("LeaveDate", DateTime.Now.ToString());
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/Save.dat");
        FileStream file1 = File.Create(Application.persistentDataPath
          + "/Quest.dat");
        SaveData data = new SaveData
        {
            PointSum = PointSum,
            isBuyAutomod = Automod_slider.activeSelf,
            CostOfGrade = StandartBuff.CostOnGrade,
            CostOfAfk = AfkBuff.CostOnGrade,
            PointOnBit = StandartBuff.pointOnBit,
            PointOnAfk = AfkBuff.pointOnBit,
            CountBall = Teleport.i,
            MaximumPoint=maximumPoint
        };
        SaveQuest data1 = new SaveQuest
        {
            QuestCompleate = new bool[10]
        };
        for (int j=0; j < 10; j++)
        {
            data1.QuestCompleate[j] = QuestManager.QuestsCompleate[j];
        }
        bf.Serialize(file, data);
        bf.Serialize(file1, data1);
        file.Close();
        file1.Close();

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
            FileStream file1 =
             File.Open(Application.persistentDataPath
             + "/Quest.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            SaveQuest data1 = (SaveQuest)bf.Deserialize(file);
            file.Close();
            file1.Close();
            PointSum = data.PointSum;
            StandartBuff.CostOnGrade = data.CostOfGrade;
            AfkBuff.CostOnGrade = data.CostOfAfk;
            StandartBuff.pointOnBit = data.PointOnBit;
            AfkBuff.pointOnBit = data.PointOnAfk;
            Teleport.i = data.CountBall;
            maximumPoint = data.MaximumPoint;
            CostReset = 20000 + Teleport.i * 50000;
            CostResetText.text = LetsScript.NormalSum(CostReset);
            for (int j = 0; j < 10; j++)
            {
                QuestManager.QuestsCompleate[j] = data1.QuestCompleate[j];
            }
            if (Teleport.i == 5)
            {
                CostReset = int.MaxValue;
                CostResetText.text = "Max";
            }
       
            Automod_slider.SetActive(data.isBuyAutomod);
            buttonBuyAutomod.SetActive(!data.isBuyAutomod);
            if(data.isBuyAutomod)
                Auto_flipper_text.text = "Auto-flippers";
            

        }
        if (PlayerPrefs.HasKey("LeaveDate"))
        {
           date = DateTime.Now- DateTime.Parse(PlayerPrefs.GetString("LeaveDate"));
        }

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) {
            SaveGame();
        }
        else
        {
            LoadGame();
            ScorePoint();
            pointSum.text = LetsScript.NormalSum(GameManager.PointSum);
            StandartCost.text = LetsScript.NormalSum(StandartBuff.CostOnGrade);
            StandartAfkCost.text = LetsScript.NormalSum(AfkBuff.CostOnGrade);
            PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
            PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        }
    }

    public void Automod(GameObject image)
    {
        
        panelLeft.SetActive(automod);
        panelRight.SetActive(automod);
        hj1.useMotor = false;
        hj2.useMotor = false;
        automod = !automod;

        if (automod)
        {
            image.transform.localScale = new Vector3(1, image.transform.localScale.y, image.transform.localScale.z);
            image.GetComponent<Image>().color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
        }
        else
        {
            image.transform.localScale = new Vector3(-1, image.transform.localScale.y, image.transform.localScale.z);
            image.GetComponent<Image>().color = Color.red;
        }
    }
    public void StandartBuff1()
    {
        if (PointSum >= StandartBuff.CostOnGrade)
        {
            PointSum -= StandartBuff.CostOnGrade;
            pointSum.text = LetsScript.NormalSum(GameManager.PointSum);
            StandartBuff.pointOnBit += 1;
            StandartBuff.CostOnGrade = (int)(StandartBuff.CostOnGrade * 1.2f);
            StandartCost.text =LetsScript.NormalSum(StandartBuff.CostOnGrade);
            PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }


    public void AfkBuffBuy()
    {
        if (PointSum >= AfkBuff.CostOnGrade)
        {
            
            PointSum -= AfkBuff.CostOnGrade;
            pointSum.text = LetsScript.NormalSum(GameManager.PointSum);
            AfkBuff.pointOnBit += 1;
            AfkBuff.CostOnGrade = (int)(AfkBuff.CostOnGrade * 1.2f);
            StandartAfkCost.text = LetsScript.NormalSum(AfkBuff.CostOnGrade);
            PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }
    }
    public void BuyAutomod()
    {
        if (PointSum >= 100000)
        {
            PointSum -= 100000;
            pointSum.text = LetsScript.NormalSum(GameManager.PointSum);
            Auto_flipper_text.text = "Auto-flippers";
            Automod_slider.SetActive(true);
            buttonBuyAutomod.SetActive(false);
        }
        else
        {
            textError.SetActive(true);
           StartCoroutine(ViewText(textError));
           StopCoroutine(ViewText(textError));
        }
    }

    public IEnumerator ViewText(GameObject text)
    {
        yield return new WaitForSeconds(1f);
        text.SetActive(false);
    }

    public void Button1()
    {
        if (Shop6.activeSelf)
            Button6();
        else if (Shop3.activeSelf)
            Button3();
        else if (Shop2.activeSelf)
            Button2();
        else if (Shop4.activeSelf)
            Button4();
        if (!Shop1.activeSelf)
        {
            button1.GetComponent<Image>().color = mycolor;

        }
        else
        {
            button1.GetComponent<Image>().color = Color.white;
        }
        if (Shop1.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop1.SetActive(!Shop1.activeSelf);

    }
    public void Button2()
    {
        if (Shop6.activeSelf)
            Button6();
        else if (Shop3.activeSelf)
            Button3();
        else if (Shop1.activeSelf)
            Button1();
        else if (Shop4.activeSelf)
            Button4();
        if (!Shop2.activeSelf)
        {
         button2.GetComponent<Image>().color = mycolor;
            
        }
        else
        {
            button2.GetComponent<Image>().color = Color.white;
        }
        if (Shop2.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop2.SetActive(!Shop2.activeSelf);

    }
    public void Button3()
    {
        if (Shop6.activeSelf)
            Button6();
        else if (Shop2.activeSelf)
            Button2();
        else if (Shop1.activeSelf)
            Button1();
        else if (Shop4.activeSelf)
            Button4();
        if (!Shop3.activeSelf)
        {
            button3.GetComponent<Image>().color = mycolor;

        }
        else
        {
            button3.GetComponent<Image>().color = Color.white;
        }
        if (Shop3.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop3.SetActive(!Shop3.activeSelf);

    }
    public void Button4()
    {
        if (Shop6.activeSelf)
            Button6();
        else if (Shop2.activeSelf)
            Button2();
        else if (Shop1.activeSelf)
            Button1();
        else if (Shop3.activeSelf)
            Button3();
        if (!Shop4.activeSelf)
        {
            button4.GetComponent<Image>().color = mycolor;

        }
        else
        {
            button4.GetComponent<Image>().color = Color.white;
        }
        if (Shop4.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop4.SetActive(!Shop4.activeSelf);

    }
    public void BackPanelClick()
    {
        if (Shop6.activeSelf)
            Button6();
        else if (Shop2.activeSelf)
            Button2();
        else if (Shop3.activeSelf)
            Button3();
        else if (Shop1.activeSelf)
            Button1();
        else if (Shop4.activeSelf)
            Button4();
    }

    public void Button6()
    {
        if (Shop2.activeSelf)
            Button2();
        else if (Shop3.activeSelf)
            Button3();
        else if (Shop1.activeSelf)
            Button1();
        else if (Shop4.activeSelf)
            Button4();
        if (!Shop6.activeSelf)
        {
            button6.GetComponent<Image>().color = mycolor;
        }
        else
        {
            button6.GetComponent<Image>().color = Color.white;
        }
        if (Shop6.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop6.SetActive(!Shop6.activeSelf);

    }

    public void ExNum1(Image image)
    {
        if (!ExNum)
        {
            image.color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
           
        }
        else
        {
            image.color = Color.red;
        }
        NumberBut.GetComponent<RectTransform>().localScale = new Vector2(-NumberBut.GetComponent<RectTransform>().localScale.x, NumberBut.GetComponent<RectTransform>().localScale.y);
        ExNum = !ExNum;
    }


    public void ScorePoint()
    {
        if ((long)Math.Floor((date).TotalMinutes) > 0)
        {
            PointSum += (long)Math.Floor((date).TotalMinutes) *AfkBuff.pointOnBit; //+ (long)(Math.Floor((date).TotalMinutes) / 10) + AfkBuff.pointOnBit;
            string days = date.Days != 0? date.Days.ToString()+"days" : "" ;
            string hours = date.Hours != 0? date.Hours.ToString()+":" : "" ;
            AfkPointText.text = "While you were leavig:"; 
            AfkPointTime.text = $"Time {days} {hours}{date.Minutes}:{date.Seconds}";
            AfkPoints.text = $" {(long)Math.Floor((date).TotalMinutes)* AfkBuff.pointOnBit/* + (long)(Math.Floor((date).TotalMinutes) / 10) + AfkBuff.pointOnBit*/}";
            AfkPointsLast.text =    "points";
            AfkMenu.SetActive(true);
            
        }
    }
   public void ThxForWatching()
    {
        if (Advertisement.IsReady())
            Advertisement.Show("Rewarded_Android");
        PointSum += (long)Math.Floor((date).TotalMinutes)* AfkBuff.pointOnBit;
       // PointSum += (long)(Math.Floor((date).TotalMinutes)/10) + AfkBuff.pointOnBit;

        AfkPointText.text = "Thanks for watching!";
        AfkPointTime.text = "You got:";
        AfkPoints.text = $" {(long)(2*(Math.Floor((date).TotalMinutes)* AfkBuff.pointOnBit/* + (long)(Math.Floor((date).TotalMinutes) / 10) + AfkBuff.pointOnBit*/))}";
        AfkPointsLast.text = "points";
    }
    public void ScoreTest()
    {
        PointSum += 1000;
        pointSum.text = LetsScript.NormalSum(GameManager.PointSum);
    }

    public void ResetAll()
    {
        if (Teleport.i < 5 && PointSum >= CostReset && QuestManager.QuestsCompleate[0] && QuestManager.QuestsCompleate[1] && QuestManager.QuestsCompleate[2]
            && QuestManager.QuestsCompleate[3] && QuestManager.QuestsCompleate[4] && QuestManager.QuestsCompleate[5] && QuestManager.QuestsCompleate[6]
            && QuestManager.QuestsCompleate[7] && QuestManager.QuestsCompleate[8] && QuestManager.QuestsCompleate[9])
        {
            if (Teleport.i < 4)
            {
                CostReset += 50000;
                CostResetText.text = LetsScript.NormalSum(CostReset);
            }
            else
            {
                CostReset = int.MaxValue;
                CostResetText.text = "Max";

            }

            Teleport.i++;
            for (int j = 0; j < 2 * Teleport.i; j++)
            {
                QuestManager.QuestsCompleate[j] = false;
            }
            //Teleport.Ressed();
            StandartBuff.pointOnBit = 1;
            AfkBuff.pointOnBit = 1;
            StandartBuff.CostOnGrade = 100;
            AfkBuff.CostOnGrade = 100;
            StandartCost.text = LetsScript.NormalSum(StandartBuff.CostOnGrade);
            StandartAfkCost.text = LetsScript.NormalSum(AfkBuff.CostOnGrade);
            Point = 0;
            PointSum = 0;
            pointSum.text = LetsScript.NormalSum(GameManager.PointSum);
            point.text = "" + 0;
            Auto_flipper_text.text = "Buy auto-flippers";
            Automod_slider.SetActive(false);
            buttonBuyAutomod.SetActive(true);
            automod = false;
            PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
            PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        }
        else if (PointSum < CostReset)
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }
        else if (!(QuestManager.QuestsCompleate[0] && QuestManager.QuestsCompleate[1] && QuestManager.QuestsCompleate[2]
            && QuestManager.QuestsCompleate[3] && QuestManager.QuestsCompleate[4] && QuestManager.QuestsCompleate[5] && QuestManager.QuestsCompleate[6]
            && QuestManager.QuestsCompleate[7] && QuestManager.QuestsCompleate[8] && QuestManager.QuestsCompleate[9]))
        {
            textError2.SetActive(true);
            StartCoroutine(ViewText(textError2));
            StopCoroutine(ViewText(textError2));
        }
    }

    public void NewGame()
    {
        Attention.SetActive(true);
    }

    public void AcceptNewGame()
    {
        Teleport.i = 0;
        PointSum = CostReset;
        ResetAll();
        Teleport.i = 0;
        for (int j = 1; j < 6; j++)
            Teleport.mainballsstatic[j].SetActive(false);
    }
}