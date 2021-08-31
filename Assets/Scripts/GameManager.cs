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
    public Image SoundImage;
    public Image TextNum;
    public GameObject Checkers;
    public GameObject stopperPanel;
    public GameObject stopperPanelCooldown;
    public GameObject StoppersButton;
    static public int maximumPoint=0;
    static public bool ExNum = false;
    public Text pointSum;
    public GameObject Attention;
    public Text point;
    public Text StandartCost;
    public Text StopperCost;
    public Text StandartAfkCost;
    public Text StopperCooldownCost;
    public Text AfkPoints;
    public Text AfkPointsLast;
    public Text AfkPointText;
    public Text StopperText;
    public Text StopperCooldownText;
    public Text AfkPointTime;
    public Text Auto_flipper_text;
    public Text PointBuffText;
    public Text PointBuffAfkText;
    public Text CostResetText;
   // public Text PointAfkText;
    public TimeSpan date;
    public Text time;
    Color mycolor;
   public GameObject buttonBuyAutomod;
    public GameObject x2bonus;
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
        ScorePoint();
        pointSum.text = NormalSum(PointSum);
        StandartCost.text = NormalSum(StandartBuff.CostOnGrade);
        StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
        StopperCost.text = NormalSum(Checker.costOnGrade);
        StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        mycolor = button2.GetComponent<Image>().color;
        button2.GetComponent<Image>().color = Color.white;
        PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
        StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers+0.75);
        StopperCooldownText.text = Checker.coldownStopper-3 + "→" + (Checker.coldownStopper -3 - 0.75);
        PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
    }

    void SaveGame()
    {
        PlayerPrefs.SetString("LeaveDate", DateTime.Now.ToString());
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath
         // + "/Save.dat");
       // FileStream file1 = File.Create(Application.persistentDataPath
       //   + "/Quest.dat");
        //SaveData data = new SaveData
        //{
        //    PointSum = PointSum,
        //    isBuyAutomod = Automod_slider.activeSelf,
        //    CostOfGrade = StandartBuff.CostOnGrade,
        //    CostOfAfk = AfkBuff.CostOnGrade,
        //    PointOnBit = StandartBuff.pointOnBit,
        //    PointOnAfk = AfkBuff.pointOnBit,
        //    CountBall = Teleport.i,
        //    MaximumPoint=maximumPoint
        //};
        PlayerPrefs.SetString("PointSum", PointSum.ToString());
        PlayerPrefs.SetString("isBuyAutomod", Automod_slider.activeSelf.ToString());
        PlayerPrefs.SetInt("CostOfGrade", StandartBuff.CostOnGrade);
        PlayerPrefs.SetInt("CostOfAfk", AfkBuff.CostOnGrade);
        PlayerPrefs.SetInt("PointOnBit", StandartBuff.pointOnBit);
        PlayerPrefs.SetInt("PointOnAfk", AfkBuff.pointOnBit);
        PlayerPrefs.SetInt("CountBall", Teleport.i);
        PlayerPrefs.SetInt("MaximumPoint", maximumPoint);
        PlayerPrefs.SetString("Stoppers", Checkers.activeSelf.ToString());
        PlayerPrefs.SetFloat("TimeStoppers", Checker.TimeStoppers);
        PlayerPrefs.SetFloat("coldownStopper", Checker.coldownStopper);
        PlayerPrefs.SetString("costStopper", Checker.costOnGrade.ToString());
        PlayerPrefs.SetString("costColdownStopper", Checker.costOnCooldown.ToString());
        PlayerPrefs.SetString("ExNum", ExNum.ToString());
        PlayerPrefs.SetString("Sound", AudioListener.volume.ToString());
    //SaveQuest data1 = new SaveQuest
    //    {
    //        QuestCompleate = new bool[10]
    //    };
        for (int j=0; j < 10; j++)
        {
            PlayerPrefs.SetString($"QuestCompleate[{j}]", QuestManager.QuestsCompleate[j].ToString());
        }
        //bf.Serialize(file, data);
        //bf.Serialize(file1, data1);
        //file.Close();
        //file1.Close();

    }

    void LoadGame()
    {
        // if (File.Exists(Application.persistentDataPath
        //   + "/Save.dat"))
        // {
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file =
        //  File.Open(Application.persistentDataPath
        //  + "/Save.dat", FileMode.Open);
        //FileStream file1 =
        // File.Open(Application.persistentDataPath
        // + "/Quest.dat", FileMode.Open);
        //SaveData data = (SaveData)bf.Deserialize(file);
        //SaveQuest data1 = (SaveQuest)bf.Deserialize(file);
        //file.Close();
        //file1.Close();
        //PointSum = data.PointSum;
        //StandartBuff.CostOnGrade = data.CostOfGrade;
        //AfkBuff.CostOnGrade = data.CostOfAfk;
        //StandartBuff.pointOnBit = data.PointOnBit;
        //AfkBuff.pointOnBit = data.PointOnAfk;
        //Teleport.i = data.CountBall;
        //maximumPoint = data.MaximumPoint;





        //if(data.isBuyAutomod)
        //    Auto_flipper_text.text = "Auto-flippers";


        // }
        if (PlayerPrefs.HasKey("ExNum"))
        {
            ExNum = bool.Parse(PlayerPrefs.GetString("ExNum"));
            if (ExNum)
            {
                ExNum = false;
                ExNum1(TextNum);
               
            }
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            AudioListener.volume = float.Parse(PlayerPrefs.GetString("Sound"));
            if(AudioListener.volume == 0)
            {
                SoundImage.color = Color.red;
                SoundImage.GetComponent<RectTransform>().localScale = new Vector2(-SoundImage.GetComponent<RectTransform>().localScale.x, SoundImage.GetComponent<RectTransform>().localScale.y);
            }
        }
        if (PlayerPrefs.HasKey("LeaveDate"))
        {
           date = DateTime.Now- DateTime.Parse(PlayerPrefs.GetString("LeaveDate"));
        }
        if (PlayerPrefs.HasKey("PointSum"))
        {
            PointSum = long.Parse(PlayerPrefs.GetString("PointSum"));

        }
        if (PlayerPrefs.HasKey("isBuyAutomod"))
        {
            
           bool flag =  bool.Parse(PlayerPrefs.GetString("isBuyAutomod"));
            if (flag)
                Auto_flipper_text.text = "Auto-flippers";
            Automod_slider.SetActive(flag);
            buttonBuyAutomod.SetActive(!flag);
        }
        if (PlayerPrefs.HasKey("CostOfGrade"))
        {
            StandartBuff.CostOnGrade = PlayerPrefs.GetInt("CostOfGrade");
        
        }
        if (PlayerPrefs.HasKey("CostOfAfk"))
        {
            AfkBuff.CostOnGrade = PlayerPrefs.GetInt("CostOfAfk");
           
        }
        if (PlayerPrefs.HasKey("PointOnBit"))
        {
            StandartBuff.pointOnBit= PlayerPrefs.GetInt("PointOnBit");
        }
        if (PlayerPrefs.HasKey("PointOnAfk"))
        {
            AfkBuff.pointOnBit = PlayerPrefs.GetInt("PointOnAfk");
        }
        if (PlayerPrefs.HasKey("CountBall"))
        {
            Teleport.i = PlayerPrefs.GetInt("CountBall");
        }
        if (PlayerPrefs.HasKey("MaximumPoint"))
        {
            maximumPoint = PlayerPrefs.GetInt("MaximumPoint");
        }
        if (PlayerPrefs.HasKey("TimeStoppers"))
        {
            Checker.TimeStoppers = PlayerPrefs.GetFloat("TimeStoppers");
        }
        if(PlayerPrefs.HasKey("coldownStopper"))
        {
            Checker.coldownStopper = PlayerPrefs.GetFloat("coldownStopper");
        }
        if(PlayerPrefs.HasKey("costStopper"))
        {
            Checker.costOnGrade=long.Parse( PlayerPrefs.GetString("costStopper" ));
        }
        if (PlayerPrefs.HasKey("costColdownStopper"))
        {
           
           Checker.costOnCooldown=long.Parse(PlayerPrefs.GetString("costColdownStopper"));
        }
        if (PlayerPrefs.HasKey("QuestCompleate[0]"))
        {
            for (int j = 0; j < 10; j++)
            {
                QuestManager.QuestsCompleate[j] = bool.Parse(PlayerPrefs.GetString($"QuestCompleate[{j}]"));
            }
            QuestManager.Updates();
            if (QuestManager.QuestsCompleate[1])
            {
                QuestManager.Quest2();
            }
        }
        CostReset = 20000 + Teleport.i * 50000;
        CostResetText.text = NormalSum(CostReset);

        if (Teleport.i == 5)
        {
            CostReset = int.MaxValue;
            CostResetText.text = "Max";
        }
        if (PlayerPrefs.HasKey("Stoppers"))
            if (bool.Parse(PlayerPrefs.GetString("Stoppers")))
                _buyStopper();
     
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
            pointSum.text = NormalSum(PointSum);
            StandartCost.text = NormalSum(StandartBuff.CostOnGrade);
            StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
            StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
            StopperCost.text = NormalSum(Checker.costOnGrade);
            PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
            PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
            StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers + 0.75);
            StopperCooldownText.text = Checker.coldownStopper - 3 + "→" + (Checker.coldownStopper - 3 - 0.75);
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
            pointSum.text = NormalSum(GameManager.PointSum);
            StandartBuff.pointOnBit += 1;
            StandartBuff.CostOnGrade = (int)(StandartBuff.CostOnGrade * 1.2f);
            StandartCost.text =NormalSum(StandartBuff.CostOnGrade);
            PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }

    public void Sounds(Image image)
    {
        if (AudioListener.volume == 1)
        {
            image.color = Color.red;
            AudioListener.volume = 0;
        }
        else
        {

            image.color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
            AudioListener.volume = 1;
        }
        image.GetComponent<RectTransform>().localScale = new Vector2(-image.GetComponent<RectTransform>().localScale.x, image.GetComponent<RectTransform>().localScale.y);
    }

    public void StopperBuff()
    {
        if (PointSum >= Checker.costOnGrade)
        {
            PointSum -= Checker.costOnGrade;
            pointSum.text = NormalSum(GameManager.PointSum);
            Checker.TimeStoppers += 0.75f;
            Checker.costOnGrade = (long)(Checker.costOnGrade * 1.2f);
            StopperCost.text = NormalSum(Checker.costOnGrade);
            StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers + 0.75);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }

    public void StopperBuffCooldown()
    {
        if (PointSum >= Checker.costOnCooldown)
        {
            PointSum -= Checker.costOnCooldown;
            pointSum.text = NormalSum(GameManager.PointSum);
            Checker.coldownStopper -= 0.75f;
            Checker.costOnCooldown = (long)(Checker.costOnCooldown * 1.2f);
            StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
            StopperCooldownCost.text = Checker.coldownStopper + "→" + (Checker.coldownStopper - 0.75);
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
            pointSum.text = NormalSum(GameManager.PointSum);
            AfkBuff.pointOnBit += 1;
            AfkBuff.CostOnGrade = (int)(AfkBuff.CostOnGrade * 1.2f);
            StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
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
            pointSum.text = NormalSum(GameManager.PointSum);
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
        image.GetComponent<RectTransform>().localScale = new Vector2(-NumberBut.GetComponent<RectTransform>().localScale.x, NumberBut.GetComponent<RectTransform>().localScale.y);
        ExNum = !ExNum;
    }


    public void ScorePoint()
    {
       // Debug.Log(date.TotalMinutes.ToString());
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
        PointSum += (long)Math.Floor((date).TotalMinutes)*2* AfkBuff.pointOnBit;
       // PointSum += (long)(Math.Floor((date).TotalMinutes)/10) + AfkBuff.pointOnBit;

        AfkPointText.text = "Thanks for watching!";
        AfkPointTime.text = "You got:";
        AfkPoints.text = $" {(long)(3*(Math.Floor((date).TotalMinutes)* AfkBuff.pointOnBit/* + (long)(Math.Floor((date).TotalMinutes) / 10) + AfkBuff.pointOnBit*/))}";
        AfkPointsLast.text = "points";
    }
    public void ScoreTest()
    {
        PointSum += 1000;
        pointSum.text = NormalSum(GameManager.PointSum);
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
                CostResetText.text = NormalSum(CostReset);
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
            StandartCost.text = NormalSum(StandartBuff.CostOnGrade);
            StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
            Point = 0;
            PointSum = 0;
            pointSum.text = NormalSum(GameManager.PointSum);
            point.text = "" + 0;
            QuestManager.Quest0Challenge += 25;
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
        CostReset = 20000;
        CostResetText.text = NormalSum(CostReset);
        Teleport.i = 0;
        StandartBuff.pointOnBit = 1;
        AfkBuff.pointOnBit = 1;
        StandartBuff.CostOnGrade = 100;
        AfkBuff.CostOnGrade = 100;
        QuestManager.Quest0Challenge = 35;
        StandartCost.text = NormalSum(StandartBuff.CostOnGrade);
        StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
        Point = 0;
        PointSum = 0;
        pointSum.text = NormalSum(GameManager.PointSum);
        point.text = "" + 0;
        Auto_flipper_text.text = "Buy auto-flippers";
        Automod_slider.SetActive(false);
        buttonBuyAutomod.SetActive(true);
        automod = false;
        PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
        PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        for (int j = 1; j < 6; j++)
            Teleport.mainballsstatic[j].SetActive(false);
        for (int j = 0; j < QuestManager.QuestsCompleate.Length; j++)
        {
            if(j<2)
                QuestManager.QuestsCompleate[j] = false;
            else
                QuestManager.QuestsCompleate[j] = true;
        }
    }
      public void BuyStopper()
    {
        if (PointSum >= 20000)
        {
            PointSum -= 20000;
            pointSum.text = NormalSum(GameManager.PointSum);
            _buyStopper();
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }
    }

    public void _buyStopper()
    {
        StoppersButton.SetActive(false);
        stopperPanel.SetActive(true);
        stopperPanelCooldown.SetActive(true);
        Checkers.SetActive(true);
    }

    public void Doreward()
    {
        if (Advertisement.IsReady())
        {
            x2bonus.SetActive(false);

            Advertisement.Show("Rewarded_Android");
            int variabled = StandartBuff.pointOnBit;
            StandartBuff.pointOnBit *= 2;
            StartCoroutine(TimeBuff(variabled));
            StartCoroutine(ShowButton());
            

        }
    }
    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(60f);
        x2bonus.SetActive(true);
    }
    IEnumerator TimeBuff(int were)
    {

        for (int i = 30; i > 0; i--)
        {
            (time).text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        time.text = "";
        StandartBuff.pointOnBit -= were;
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
}

