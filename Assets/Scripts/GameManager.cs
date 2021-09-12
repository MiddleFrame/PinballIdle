using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEditor;
public class GameManager : MonoBehaviour, IUnityAdsListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    //\
    public GameObject[] BottonSkin;
    public SpriteRenderer spawnPoint;
    public Sprite spawnPointDefault;
    public Sprite spawnPointDark;
    public Text[] AllText;
    public GameObject Splitters;
    public GameObject[] AllPanel;
    public GameObject[] Stoppers;
    public GameObject[] Balls = new GameObject[6];
    public GameObject Phone;
    public GameObject ProgressBarBackground;
    public Image DarkThemeImage;
    public LetsScript let;
    public static bool DarkTheme = false;
    static public int lvl=1;
    public Text StopperTextMain;
    public Text lvlbuff;
    public Image lvlImage;
    public Image ProgressBar;
    private float speed = 3f;
    public long POintSpent = 0;
    public Camera MainCamera;
    public Sprite DefaultLock;
    public Sprite CompleatedImage;
    public Sprite CompleatedImageLock;
    public GameObject[] Lock;
    public GameObject QuestCompeate;
    static public int timeQuest= 30;
    public Text TimeQuest2;
    public GameObject spawnPointLine;
    public GameObject QuestBlock;
    public GameObject BlockCanvas;
    public GameObject[] PanelQuest = new GameObject[5];
    public GameObject[] ButtonQuests = new GameObject[5];
    public GameObject[] BuyBallButtons = new GameObject[5];
    public Text PointQuest1;
    static public int quest1point=0;
    public GameObject StopButton;
    public GameObject QuestPanel;
    public GameObject BottonPanel;
    public GameObject StartPointBottonPanel;
    public GameObject EndPointBottonPanel;
    public Text QuestsText;
    private int CostBall = 40000;
    public GameObject ButtonPick;
    public GameObject Buttonx3Pick;
    public GameObject ButtonBigPick;
    public GameObject RewardError;
    public Image SoundImage;
    public Image TextNum;
    public GameObject Checkers;
    public GameObject stopperPanel;
    public GameObject stopperPanelCooldown;
    public GameObject StoppersButton;
    static public bool isQuestStarted = false;
    static public int maximumPoint=0;
    static public bool ExNum = false;
    static public int NumberQuest;
    public Text pointSum;
    public GameObject Attention;
    public Text MaxPoint;
    public Text SumSent;
    public Text NumberOfBall;
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
    Color mycolor = new Color(0.776f, 0.776f, 0.776f, 1.000f);
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
    public GameObject Shop5;
    public GameObject Shop4;
    public GameObject Shop3;
    public GameObject Shop2;
    public GameObject Shop1;
    public GameObject button5;
    public GameObject button4;
    public GameObject button3;
    public GameObject button2;
    public GameObject button1;
    public GameObject Shop6;
    public GameObject button6;
    public GameObject plaingField;
    public GameObject NumberBut;
    public int CostReset = 20000;
    static public int Point = 0;
    static public long PointSum;
    static public bool automod=false;
    public GameObject AfkMenu;
    public int price=0;
    public GameObject lvlTextPanel;
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


    enum reward {x2reward, x3reward }
    reward MyReward;
    class SaveQuest
    {
        public bool[] QuestCompleate;
    }

   private void Awake()
    {
        
        if (Advertisement.isSupported)
            Advertisement.Initialize("4265503", false);
    }
    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Load("4265503", this);
        ScorePoint();
        pointSum.text = NormalSum(PointSum);
        StandartCost.text = NormalSum(StandartBuff.CostOnGrade);
        StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
        StopperCost.text = NormalSum(Checker.costOnGrade);
        StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        colorB = button1.GetComponent<Button>().colors;
        PointBuffText.text = StandartBuff.pointOnBit + "→" + (StandartBuff.pointOnBit + 1);
        StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers+0.75);
        StopperCooldownText.text = Checker.coldownStopper-3 + "→" + (Checker.coldownStopper -3 - 0.75);
        PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        if (AudioListener.volume == 0)
        {

            if (SoundImage.color != Color.red)
            {
                SoundImage.color = Color.red;
                
            }
        }
        if (!ExNum)
        {

            TextNum.color = Color.red;
          
        }

    }

    void SaveGame()
    {
        PlayerPrefs.SetString("LeaveDate", DateTime.Now.ToString());
        PlayerPrefs.SetString("DarkTheme", DarkTheme.ToString());
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
        PlayerPrefs.SetString("PointSent", POintSpent.ToString());
        PlayerPrefs.SetInt("HitNeeded",LetsScript.hitneeded);
        PlayerPrefs.SetInt("lvl",lvl);
        PlayerPrefs.SetString("TextView", lvlTextPanel.activeSelf.ToString());
        for (int j=0; j < 5; j++)
        {
            PlayerPrefs.SetString($"QuestNewCompleate[{j}]", QuestManager.QuestsCompleate[j].ToString());
        }
        for (int j = 0; j < 5; j++)
        {
            PlayerPrefs.SetString($"BuyBallButtons[{j}]", BuyBallButtons[j].activeSelf.ToString());
        }
       
        if (PlayerPrefs.HasKey("reward"))
        {
            if(bool.Parse(PlayerPrefs.GetString("reward")))
            PlayerPrefs.SetInt("RewardPoints", (int)Math.Floor((date).TotalMinutes));
        }
    }
    private void Update()
    {
       
        if (isQuestStarted)
            BottonPanel.transform.position = Vector3.MoveTowards(BottonPanel.transform.position, EndPointBottonPanel.transform.position, speed * Time.deltaTime);
        else if (!isQuestStarted)
            BottonPanel.transform.position = Vector3.MoveTowards(BottonPanel.transform.position, StartPointBottonPanel.transform.position, speed * Time.deltaTime);

    }
    void LoadGame()
    {

        if (PlayerPrefs.HasKey("DarkTheme"))
        {
            if (bool.Parse(PlayerPrefs.GetString("DarkTheme")))
            {
               //PlayerSettings.SplashScreen.unityLogoStyle = PlayerSettings.SplashScreen.UnityLogoStyle.LightOnDark;
                EnableDisableDark();
            }
        }
        if (PlayerPrefs.HasKey("reward"))
        {
            Reward = bool.Parse(PlayerPrefs.GetString("reward"));
            AfkPoints.text = PlayerPrefs.GetInt("RewardPoints")+"";
        }
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
            if(AudioListener.volume < 0.9f)
            {
                SoundImage.color = Color.red;
                SoundImage.GetComponent<RectTransform>().localScale = new Vector2(-SoundImage.GetComponent<RectTransform>().localScale.x, SoundImage.GetComponent<RectTransform>().localScale.y);
            }
        }
        if (PlayerPrefs.HasKey("LeaveDate") )
        {
           if (ButtonPick.activeSelf)
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
            if (Teleport.i > 0)
            {
                if (Teleport.i > 5)
                {
                    Teleport.i = 5;
                }
                NumberOfBall.text = $"Number of extra ball  {Teleport.i}\\5";
            }
            else
                NumberOfBall.text = $"Number of extra ball  0\\5";
        }
        if (PlayerPrefs.HasKey("MaximumPoint"))
        {
            maximumPoint = PlayerPrefs.GetInt("MaximumPoint");
            MaxPoint.text = $"Record:   {maximumPoint}";
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
        if (PlayerPrefs.HasKey("PointSent"))
        {
            POintSpent = long.Parse(PlayerPrefs.GetString("PointSent"));
            SumSent.text = $"Total points spent:     {POintSpent}";
        }
        else
            SumSent.text = $"Total points spent:     0";
        if (PlayerPrefs.HasKey("costColdownStopper"))
        {
           
           Checker.costOnCooldown=long.Parse(PlayerPrefs.GetString("costColdownStopper"));
        }
        if (PlayerPrefs.HasKey("QuestNewCompleate[0]"))
        {
            for (int j = 0; j < 5; j++)
            {
                QuestManager.QuestsCompleate[j] = bool.Parse(PlayerPrefs.GetString($"QuestNewCompleate[{j}]"));
                if (QuestManager.QuestsCompleate[j])
                {
                    PanelQuest[j].SetActive(false);
                    Lock[j].GetComponent<Image>().sprite = CompleatedImageLock;
                }
            }
           
        }
        if (PlayerPrefs.HasKey("BuyBallButtons[0]"))
        {
            for (int j = 0; j < 5; j++)
            {
               BuyBallButtons[j].SetActive(bool.Parse( PlayerPrefs.GetString($"BuyBallButtons[{j}]")));
            }
        }  
        if (PlayerPrefs.HasKey("lvl"))
        {
            lvl = PlayerPrefs.GetInt("lvl");
            let.lvltext.text = $" level - {GameManager.lvl}";
            let.lvlbuff.text = $"x{GameManager.lvl}";
        }
        if (PlayerPrefs.HasKey("TextView"))
        {
            bool flag = bool.Parse(PlayerPrefs.GetString("TextView"));
            if (!flag)
            {
               
                LvltextView();
            }
        }
        if (PlayerPrefs.HasKey("HitNeeded"))
        {
            LetsScript.hitneeded = PlayerPrefs.GetInt("HitNeeded");
            let.ProgressBar.fillAmount = (500f * GameManager.lvl - LetsScript.hitneeded) / 500f / GameManager.lvl;
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
            PlayerPrefs.SetString("Reward", false.ToString());
        }
    }
    public void QuestNumber(int i)
    {
        NumberQuest = i;
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
            POintSpent += StandartBuff.CostOnGrade;
            SumSent.text = $"Total points spent:     {POintSpent}";
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
            POintSpent += Checker.costOnGrade;
            SumSent.text = $"Total points spent:     {POintSpent}";
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
            POintSpent += Checker.costOnCooldown;
            SumSent.text = $"Total points spent:     {POintSpent}";
            pointSum.text = NormalSum(GameManager.PointSum);
            Checker.coldownStopper -= 0.75f;
            Checker.costOnCooldown = (long)(Checker.costOnCooldown * 1.2f);
            StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
            StopperCooldownText.text = Checker.coldownStopper + "→" + (Checker.coldownStopper - 0.75);
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
            POintSpent += AfkBuff.CostOnGrade;
            SumSent.text = $"Total points spent:     {POintSpent}";
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
            POintSpent += 100000;
            SumSent.text = $"Total points spent:     {POintSpent}";
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
        if (!isQuestStarted)
        {
            if (Shop6.activeSelf)
                Button6();
            else if (Shop3.activeSelf)
                Button3();
            else if (Shop2.activeSelf)
                Button2();
            else if (Shop4.activeSelf)
                Button4();
            else if (Shop5.activeSelf)
                Button5();
        }
            if (!Shop1.activeSelf)
            {
            if (DarkTheme)
            {
                button1.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
                button1.GetComponent<Image>().color = mycolor;

            }
            else
            {
            if (DarkTheme)
            {
                button1.GetComponent<Image>().color = Color.white;
            }
            else
                button1.GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF); ;
            }
            if (Shop1.activeSelf)
                BackPanel.SetActive(false);
            else
                BackPanel.SetActive(true);
        
        Shop1.SetActive(!Shop1.activeSelf);

    }
    public void Button2()
    {
        if (!isQuestStarted)
        {
            if (Shop6.activeSelf)
                Button6();
            else if (Shop3.activeSelf)
                Button3();
            else if (Shop1.activeSelf)
                Button1();
            else if (Shop4.activeSelf)
                Button4();
            else if (Shop5.activeSelf)
                Button5();
        }
        if (!Shop2.activeSelf)
            {
            if (DarkTheme)
            {
                button2.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
                button2.GetComponent<Image>().color = mycolor;

            }
            else
            {
            if (DarkTheme)
            {
                button2.GetComponent<Image>().color = Color.white;
            }
            else
                button2.GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF);
        }
        
            if (Shop2.activeSelf)
                BackPanel.SetActive(false);
            else
                BackPanel.SetActive(true);
        
        Shop2.SetActive(!Shop2.activeSelf);

    }
    public void Button3()
    {
        if (!isQuestStarted)
        {
            if (Shop6.activeSelf)
                Button6();
            else if (Shop2.activeSelf)
                Button2();
            else if (Shop1.activeSelf)
                Button1();
            else if (Shop4.activeSelf)
                Button4();
            else if (Shop5.activeSelf)
                Button5();
        }
        if (!Shop3.activeSelf)
        {
            if (DarkTheme)
            {
                button3.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
                button3.GetComponent<Image>().color = mycolor;

        }
        else
        {
            if (DarkTheme)
            {
                button3.GetComponent<Image>().color = Color.white;
            }
            else
                button3.GetComponent<Image>().color = new Color(0xE6,0xE6,0xE6,0xFF);
        }
        if (Shop3.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop3.SetActive(!Shop3.activeSelf);

    }
    public void Button4()
    {
        if (!isQuestStarted)
        {
            if (Shop6.activeSelf)
                Button6();
            else if (Shop2.activeSelf)
                Button2();
            else if (Shop1.activeSelf)
                Button1();
            else if (Shop3.activeSelf)
                Button3();
            else if (Shop5.activeSelf)
                Button5();
        }
        if (!Shop4.activeSelf)
        {
            if (DarkTheme)
            {
                button4.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
                button4.GetComponent<Image>().color = mycolor;

        }
        else
        {
            if (DarkTheme)
            {
                button4.GetComponent<Image>().color = Color.white;
            }
            else
                button4.GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF); ;
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
        else if (Shop5.activeSelf)
            Button5();
    }

    public void Button6()
    {
        if (!isQuestStarted)
        {
            if (Shop2.activeSelf)
                Button2();
            else if (Shop3.activeSelf)
                Button3();
            else if (Shop1.activeSelf)
                Button1();
            else if (Shop4.activeSelf)
                Button4();
            else if (Shop5.activeSelf)
                Button5();
        }
        if (!Shop6.activeSelf)
        {
            if (DarkTheme)
            {
                button6.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
                button6.GetComponent<Image>().color = mycolor;
        }
        else
        {
            if (DarkTheme)
            {
                button6.GetComponent<Image>().color = Color.white;
            }
            else
                button6.GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF); ;
        }
        if (Shop6.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop6.SetActive(!Shop6.activeSelf);

    }
    public void Button5()
    {
        if (!isQuestStarted)
        {
            if (Shop2.activeSelf)
                Button2();
            else if (Shop3.activeSelf)
                Button3();
            else if (Shop1.activeSelf)
                Button1();
            else if (Shop4.activeSelf)
                Button4();
            else if (Shop6.activeSelf)
                Button6();
        }
        if (!Shop5.activeSelf)
        {
            if (DarkTheme)
            {
                button5.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
            button5.GetComponent<Image>().color = mycolor;
        }
        else
        {
            if (DarkTheme)
            {
                button5.GetComponent<Image>().color = Color.white;
            }
            else
                button5.GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF); ;
        }
        if (Shop5.activeSelf)
            BackPanel.SetActive(false);
        else
            BackPanel.SetActive(true);
        Shop5.SetActive(!Shop5.activeSelf);

    }
    public void ExNum1(Image image)
    {
        if (!ExNum)
        {
            image.color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
            if(image.GetComponent<RectTransform>().localScale.x<0)
            image.GetComponent<RectTransform>().localScale = new Vector2(-image.GetComponent<RectTransform>().localScale.x, image.GetComponent<RectTransform>().localScale.y);
        }
        else
        {
            image.color = Color.red;
            if (image.GetComponent<RectTransform>().localScale.x > 0)
                image.GetComponent<RectTransform>().localScale = new Vector2(-image.GetComponent<RectTransform>().localScale.x, image.GetComponent<RectTransform>().localScale.y);
        }
       
        ExNum = !ExNum;
    }


    public void ScorePoint()
    {
        // Debug.Log(date.TotalMinutes.ToString());
        Debug.Log(Math.Floor(date.TotalMinutes).ToString());
        Debug.Log(Reward.ToString());
        if ((long)Math.Floor((date).TotalMinutes) > 0 && !Reward)
        {
            PointSum += (long)Math.Floor((date).TotalMinutes) *AfkBuff.pointOnBit; //+ (long)(Math.Floor((date).TotalMinutes) / 10) + AfkBuff.pointOnBit;
            string days = date.Days != 0? date.Days.ToString()+" days" : "" ;
            string hours = date.Hours != 0? date.Hours.ToString()+" h" : "" ;
            AfkPointText.text = "While you were leavig:"; 
            AfkPointTime.text = $"{days} {hours} {date.Minutes} min";
            AfkPoints.text = $" {(long)Math.Floor((date).TotalMinutes)* AfkBuff.pointOnBit}";
            AfkPointsLast.text =    "points";
            AfkMenu.SetActive(true);
            
        }
    }
   public void ThxForWatching()
    {
        if (Advertisement.IsReady())
        {
            MyReward = reward.x3reward;
            Advertisement.Show("Rewarded_Android");
        }
        else
        {
            RewardError.SetActive(true);
            StartCoroutine(ViewText(RewardError));
            StopCoroutine(ViewText(RewardError));
        }
    }
    public void ScoreTest()
    {
        PointSum += 1000;
        pointSum.text = NormalSum(GameManager.PointSum);
    }

    public void ResetAll()
    {
        if (Teleport.i < 5 && PointSum >= CostReset && QuestManager.QuestsCompleate[0] && QuestManager.QuestsCompleate[1] && QuestManager.QuestsCompleate[2]
            && QuestManager.QuestsCompleate[3] && QuestManager.QuestsCompleate[4])
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
            for (int j = 0; j <  Teleport.i; j++)
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
        maximumPoint = 0;
        MaxPoint.text = $"Record:    0";
        POintSpent = 0;
        SumSent.text = $"Total points spent:     {POintSpent}";
        StandartBuff.CostOnGrade = 100;
        AfkBuff.CostOnGrade = 100;
        QuestManager.Quest0Challenge = 35;
        NumberOfBall.text = $"Number of extra ball  0\\5";
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
        for (int j = 0; j < 5; j++)
        {
            QuestManager.QuestsCompleate[j] = false;
            PanelQuest[j].SetActive(true);
            BuyBallButtons[j].SetActive(false);
            Lock[j].GetComponent<Image>().sprite = DefaultLock;
        }
    }
    

    public void StartQuest(string textQuest)
    {
        QuestPanel.SetActive(true);
        QuestsText.text = textQuest;
        Button3();
        x2bonus.SetActive(false);
      
        StartCoroutine(ShowButton());
    }
      public void BuyStopper()
    {
        if (PointSum >= 20000)
        {
            PointSum -= 20000;
            POintSpent += 20000;
            SumSent.text = $"Total points spent:     {POintSpent}";
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
        StopperTextMain.text = "Stoppers";
        StoppersButton.SetActive(false);
        stopperPanel.SetActive(true);
        stopperPanelCooldown.SetActive(true);
        Checkers.SetActive(true);
    }

    public void Doreward()
    {
        if (Advertisement.IsReady())
        {

            MyReward = reward.x2reward;
            Advertisement.Show("Rewarded_Android");
        }
        
    }
    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(60f);
        int i = 1;
        while (i > 0)
        {
            if (!isQuestStarted)
            {
                x2bonus.SetActive(true);
                i--;
            }
            else
            {
                yield return new WaitForSeconds(60f);
            }
        }
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
        lvlbuff.text = $"x{lvl}";
        if (!DarkTheme)
            lvlbuff.color = Color.black;
        else
            lvlbuff.color = Color.white;
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
    public void BuyBall(int i)
    {
        if (PointSum >= CostBall)
        {
            PointSum -= CostBall;
            POintSpent += CostBall;
            SumSent.text = $"Total points spent:     {POintSpent}";
            pointSum.text = NormalSum(GameManager.PointSum);
            Teleport.i++;
            NumberOfBall.text = $"Number of extra ball  {Teleport.i}\\5";
            BuyBallButtons[i].SetActive(false);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }

    public void CompleatedQuest()
    {
        BlockCanvas.SetActive(true);
        Shop3.SetActive(true);
        QuestManager.QuestsCompleate[NumberQuest] = true;
        ButtonQuests[NumberQuest].GetComponent<Image>().sprite = CompleatedImage;
        StartCoroutine(BackScreen(PanelQuest[NumberQuest]));
        

    }

    IEnumerator BackScreen(GameObject Go)
    {
        Lock[NumberQuest].GetComponent<Image>().sprite=CompleatedImageLock;
        while (Go.transform.position.x<QuestBlock.transform.position.x)
        {
            Go.transform.position = new Vector2(Go.transform.position.x+ 0.8f * Time.deltaTime,Go.transform.position.y);
            yield return new WaitForFixedUpdate();
        }
        BlockCanvas.SetActive(false);
        isQuestStarted = false;
        BuyBallButtons[NumberQuest].SetActive(true);
        Go.SetActive(false);
        Shop3.SetActive(false);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
           
    }

    public void OnUnityAdsReady(string placementId)
    {
       
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new NotImplementedException();
    }
    static public bool Reward = false;
    public void OnUnityAdsDidStart(string placementId)
    {
        if (MyReward == reward.x3reward && placementId != "Interstitial_Android")
        {
            AfkPoints.text = $"{3 *(long)(Math.Floor((date).TotalMinutes)) * AfkBuff.pointOnBit}";
            PointSum += (long)(Math.Floor((date).TotalMinutes)) * 3 * AfkBuff.pointOnBit;
            Reward = true;
            PlayerPrefs.SetString("reward", Reward.ToString());
        }
    }
    public void Stop()
    {
        isQuestStarted = false;
        point.gameObject.SetActive(true);
        lvlTextPanel.SetActive(true);
        pointSum.gameObject.SetActive(true);
        StopButton.SetActive(false);
        ProgressBar.gameObject.SetActive(true);
        if (NumberQuest == 0)
            PointQuest1.gameObject.SetActive(false);
        else if (NumberQuest == 1)
        {
            TimeQuest2.gameObject.SetActive(false);
            StopCoroutine(Quest2());
          
        }
        else if(NumberQuest == 2)
        {
            MainCamera.orthographicSize = 5;
            EndPointBottonPanel.transform.position = new Vector2(EndPointBottonPanel.transform.position.x, EndPointBottonPanel.transform.position.y + 50f);
            PointQuest1.gameObject.SetActive(false);
        }
        else if(NumberQuest == 3)
        {
            PointQuest1.gameObject.SetActive(false);
            StopCoroutine(CameraRotation());
            MainCamera.transform.rotation = new Quaternion(MainCamera.transform.rotation.x, MainCamera.transform.rotation.y, 0, 0);
         
            BottonPanel.SetActive(true);
           
        }
        else if(NumberQuest == 4)
        {
            PointQuest1.gameObject.SetActive(false);
            MainCamera.orthographicSize = 5;
            MainCamera.transform.position = new Vector3(0f, 0f, MainCamera.transform.position.z);
           
        }
    }
    IEnumerator ShowButtonStopAndRestart()
    {
        yield return new WaitForSeconds(1f);
        StopButton.SetActive(true);
        
    }
    public void StartQuest()
    {
        isQuestStarted = true;
        lvlTextPanel.SetActive(false);
        StartCoroutine(ShowButtonStopAndRestart());
        quest1point = 0;
        ProgressBar.gameObject.SetActive(false);
        switch (NumberQuest)
        {
            case 0:
                PointQuest1.text = "0\\30";
                speed = 3f;
                PointQuest1.gameObject.SetActive(true);
                break;
            case 1:
                TimeQuest2.gameObject.SetActive(true);
                speed = 3f;
                StartCoroutine(Quest2());
                break;
            case 2:
                PointQuest1.text = "0\\20";
                PointQuest1.gameObject.SetActive(true);
                MainCamera.orthographicSize = 25;
                speed = 3f;
                EndPointBottonPanel.transform.position = new Vector2(EndPointBottonPanel.transform.position.x , EndPointBottonPanel.transform.position.y - 50f);
                break;
            case 3:
 
                PointQuest1.text = "0\\20";
                PointQuest1.gameObject.SetActive(true);
                BottonPanel.SetActive(false);
                StartCoroutine(CameraRotation());
                speed = 3f;
                break;
            case 4:
                PointQuest1.text = "0\\20";
                PointQuest1.gameObject.SetActive(true);
                speed = 30f;
                MainCamera.orthographicSize = 1;
                break;
            default:
                break;

        }
    }
    public void ReklamaOnStop()
    {
        Advertisement.Show("Interstitial_Android");
    }
    IEnumerator CameraRotation()
    {
        quest1point = 0;
        while (quest1point<20 && !BottonPanel.activeSelf)
        {
            MainCamera.transform.Rotate(new Vector3(0, 0, 1), Space.World);
           
            // MainCamera.transform.rotation =Quaternion.AngleAxis(90, new Vector3( 0,0,1));
            yield return new WaitForFixedUpdate();
        }
       
        if (quest1point >= 20)
        {
            Stop();
            QuestManager.QuestsCompleate[3] = true;
            QuestCompeate.SetActive(true);
            
        }
        else
        {
            MainCamera.transform.rotation = new Quaternion(MainCamera.transform.rotation.x, MainCamera.transform.rotation.y, 0, 0);
            BottonPanel.transform.position = new Vector2(0, -7);
        }
    }
    IEnumerator Quest2()
    {
        timeQuest = 30;
        while (timeQuest > 0 && TimeQuest2.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1f);
            timeQuest--;
            TimeQuest2.text = $"{timeQuest}";
        }
        Stop();
        if (timeQuest <= 0)
        {
            QuestManager.QuestsCompleate[0] = true;
            QuestCompeate.SetActive(true);
        }
       
    }

    public void LvltextView() {

        if (!lvlTextPanel.activeSelf)
        {
            lvlTextPanel.SetActive(true);
            lvlImage.color = new Color32(0x39, 0xB5, 0x4A, 0xFF);

        }
        else
        {
            lvlTextPanel.SetActive(false);
            lvlImage.color = Color.red;
        }
        lvlImage.GetComponent<RectTransform>().localScale = new Vector2(-lvlImage.GetComponent<RectTransform>().localScale.x, lvlImage.GetComponent<RectTransform>().localScale.y);
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log(showResult);
        if (showResult == ShowResult.Finished && placementId!= "Interstitial_Android")
        {
            if (MyReward == reward.x3reward)
            {
               // PointSum += price * 3 * AfkBuff.pointOnBit;
                // PointSum += (long)(Math.Floor((date).TotalMinutes)/10) + AfkBuff.pointOnBit;
                ButtonPick.SetActive(false);
                Buttonx3Pick.SetActive(false);
                ButtonBigPick.SetActive(true);
                AfkPointText.text = "Thanks for watching!";
                AfkPointTime.text = "You got:";
               
                //AfkPointsLast.text = $"{(date).TotalMinutes}";
                AfkPointsLast.text = "points";
            }
            else if (MyReward == reward.x2reward)
            {
                Reward = false;
                lvlbuff.text = $"x{lvl*2}";
                lvlbuff.color = Color.yellow;
                x2bonus.SetActive(false);
                int variabled = StandartBuff.pointOnBit;
                StandartBuff.pointOnBit *= 2;
                StartCoroutine(TimeBuff(variabled));
                StartCoroutine(ShowButton());
                PlayerPrefs.SetString("reward", Reward.ToString());
            }
        }
        else if (showResult == ShowResult.Skipped && placementId != "Interstitial_Android")
        {
            PointSum -= (long)(Math.Floor((date).TotalMinutes)) * 3 * AfkBuff.pointOnBit;
        }
        else if(placementId== "Interstitial_Android")
        {

        }
        else
        throw new NotImplementedException();
        Reward = false;
    }

    public void EnableDisableDark()
    {
        if (!DarkTheme)
        {
            DarkThemeImage.color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
            
        }
        else
        {
            DarkThemeImage.color = Color.red;
        }
        DarkThemeImage.GetComponent<RectTransform>().localScale = new Vector2(-DarkThemeImage.GetComponent<RectTransform>().localScale.x, DarkThemeImage.GetComponent<RectTransform>().localScale.y);
        DarkTheme = !DarkTheme;
        if (DarkTheme)
        {
            spawnPoint.sprite = spawnPointDark;
            ColorBlock cb = button1.GetComponent<Button>().colors;
            cb.normalColor = new Color32(0x16, 0x71, 0x99, 0xFF);
            cb.pressedColor = new Color32(0x0E, 0x43, 0x60, 0xFF);
            cb.selectedColor = new Color32(0x16, 0x71, 0x99, 0xFF);
            cb.highlightedColor = new Color32(0x16, 0x71, 0x99, 0xFF);
            cb.disabledColor = new Color32(0x16, 0x71, 0x99, 0xFF);
            button6.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            button1.GetComponent<Button>().colors = cb;
            button2.GetComponent<Button>().colors = cb;
            button3.GetComponent<Button>().colors = cb;
            button4.GetComponent<Button>().colors = cb;
            button5.GetComponent<Button>().colors = cb;
            button6.GetComponent<Button>().colors = cb;
        }
        else
        {
            spawnPoint.sprite = spawnPointDefault;
            button6.GetComponent<Image>().color = mycolor;
            button1.GetComponent<Button>().colors = colorB;
            button2.GetComponent<Button>().colors = colorB;
            button3.GetComponent<Button>().colors = colorB;
            button4.GetComponent<Button>().colors = colorB;
            button5.GetComponent<Button>().colors = colorB;
            button6.GetComponent<Button>().colors = colorB;
      }
        ProgressBarBackground.GetComponent<Image>().color = ChangeColor(ProgressBarBackground.GetComponent<Image>().color);
       
      
        Shop1.GetComponent<Image>().color=ChangeColor(Shop1.GetComponent<Image>().color, "Shop");
        Shop2.GetComponent<Image>().color=ChangeColor(Shop2.GetComponent<Image>().color, "Shop");
        Shop3.GetComponent<Image>().color=ChangeColor(Shop3.GetComponent<Image>().color, "Shop");
        Shop4.GetComponent<Image>().color=ChangeColor(Shop4.GetComponent<Image>().color, "Shop");
        Shop5.GetComponent<Image>().color= ChangeColor(Shop5.GetComponent<Image>().color, "Shop");
        Shop6.GetComponent<Image>().color= ChangeColor(Shop6.GetComponent<Image>().color, "Shop");
        spawnPointLine.GetComponent<SpriteRenderer>().color = ChangeColor(plaingField.GetComponent<SpriteRenderer>().color, "pointline");
        MainCamera.backgroundColor = ChangeColor(MainCamera.backgroundColor);
        Splitters.GetComponent<Image>().color = ChangeColor(Splitters.GetComponent<Image>().color, "808080");
        plaingField.GetComponent<SpriteRenderer>().color= ChangeColor(plaingField.GetComponent<SpriteRenderer>().color);
        Phone.GetComponent<SpriteRenderer>().color = ChangeColor(Phone.GetComponent<SpriteRenderer>().color);
        for(int h = 0; h<6; h++)
            Balls[h].GetComponent<SpriteRenderer>().color = ChangeColor(Balls[h].GetComponent<SpriteRenderer>().color);
        for(int h =0; h<2;h++)
            Stoppers[h].GetComponent<SpriteRenderer>().color = ChangeColor(Stoppers[h].GetComponent<SpriteRenderer>().color);
        for(int h=0; h < AllPanel.Length; h++)
        {
            AllPanel[h].GetComponent<Image>().color = ChangeColor(AllPanel[h].GetComponent<Image>().color, "Panel");
        }
        for (int h = 0; h <BottonSkin.Length; h++)
        {
            BottonSkin[h].GetComponent<Image>().color = ChangeColor(BottonSkin[h].GetComponent<Image>().color, "Panel");
        }
        for (int h = 0; h < AllText.Length; h++)
        {
            AllText[h].color = ChangeColor(AllText[h].color);
        }
    }
    ColorBlock colorB;
    public Color32 ChangeColor(Color gr, string tag = "")
    {
        if (DarkTheme)
        { 
            if (gr == Color.black)
                return Color.white;
            else if (gr == new Color32(0x80, 0x80, 0x80, 0xFF) || gr == new Color32(0xCC, 0xCC, 0xCC, 0xFF) || gr == Color.white)
            {
                return new Color32(0x0E, 0x43, 0x60, 0xFF);
            }
            else if (gr == new Color32(0xE6, 0xE6, 0xE6, 0xFF) || gr == new Color32(0xB3, 0xB3, 0xB3, 0xFF))
            {
                return new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
            {
                return Color.white;
            }
        }
        else
        {
            if (gr == Color.white)
                return Color.black;
            else if (gr == new Color32(0x0E, 0x43, 0x60, 0xFF))
            {
                if (tag == "808080")
                    return new Color32(0x80, 0x80, 0x80, 0xFF);
                else if (tag == "pointline")
                    return new Color32(0xCC, 0xCC, 0xCC, 0xFF);
                else
                    return Color.white;
            }
            else if (gr == new Color32(0x16, 0x71, 0x99, 0xFF))
            {
                if (tag == "Panel")
                    return new Color32(0xB3, 0xB3, 0xB3, 0xFF);
                else if (tag == "Shop")
                    return new Color32(0xB3, 0xB3, 0xB3, 0xFF);
                else 
                    return new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            }
            else
                return Color.white;
        }
    }
    
}

