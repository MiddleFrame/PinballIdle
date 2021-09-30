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
    public GameObject[] PanelTriggerBalls;
    public static int choosenBall = 0;
    public Color32[] colorsBall = new Color32[] {Color.black, new Color32(0xB3,0xB3,0xB3,0xFF), new Color32(0xFF,0x89,0x12,0xFF), new Color32(0xFC,0xDE,0x3A,0xFF) };
    public Text timeExpReward;
    public GameObject x2expbonus;
    public Sprite ButtonBuy;
    public ScrollRect[] Scroll;
    public GameObject CircleBuffPanel;
    public GameObject x2areasBuyButton;
    public Text x2AreasCostText;
    int[] x2AreasCost = new int[] { 50000, 75000, 90000 };
    public GameObject[] x2Areas;
    public Text CircleBuffText;
    public GameObject CircleBuffButton;
    public Text CircleTextBuffCost;
    public FieldManager Fm;
    public GameObject StoppersBuffCooldownButton;
    public GameObject StoppersBuffButton;
    public LetsScript Romb;
    public Text RombCostBuff;
    public Text PointBuffTextRomb;
    public GameObject RombBaff;
    public Text RombBuy;
    public GameObject RombButton;
    public GameObject StrongRomb;
    public Text CircleF2;
    public GameObject    buttonBuyCirlce;
    public GameObject[] ContentShop1;
    public GameObject[] ContentShop2;
    public GameObject[] ContentShop3;
    public Text lvlBuff;
    public Text GemGet;
    public Image[] MiniField;
    public Sprite[] MiniFieldDark;
    public Sprite[] MiniFieldLight;
    public GameObject[] Arrows;
    public GameObject[] PanelBackGround;
    public GameObject[] Handlles;
    int lvlNeeded = 3;
    public GameObject ButtonRelive;
    public GameObject LevelNeedToReliveText;
    public Text ReliveText;
    public Text Gems;
    public Sprite MaxBuff;
    public Image StopperCooldown;
    public Image StopperTime;
    public Sprite NumberOn;
    public Sprite NumberOff;
    public Image Number;
    public Sprite DarkOn;
    public Sprite DarkOff;
    public Image Dark;
    public Sprite LvlTextOn;
    public Sprite LvlTextOff;
    public Image LvlText;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public Image Sound;
    public GameObject ProgressBarGM;
    public GameObject[] BottonSkin;
    public SpriteRenderer spawnPoint;
    public SpriteRenderer spawnPointF2;
    public Sprite spawnPointDefault;
    public Sprite spawnPointDark;
    public Text[] AllText;
    public GameObject Splitters;
    public GameObject[] AllPanel;
    public GameObject[] Stoppers;
    public GameObject[] Balls = new GameObject[6];
    public GameObject[] BallsF2;
    public GameObject Phone;
    public GameObject PhoneF2;
    public GameObject ProgressBarBackground;
    public Image DarkThemeImage;
    public Text GemText;
    public LetsScript let;
    public static bool DarkTheme = false;
    static public int lvl=1;
    public Text StopperTextMain;
    public Text lvlbuff;
    public Image lvlImage;
    public Image ProgressBar;
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
    public GameObject spawnPointLineF2;
    public GameObject QuestBlock;
    public GameObject BlockCanvas;
    public GameObject[] PanelQuest = new GameObject[5];
    public GameObject[] ButtonQuests = new GameObject[5];
    public GameObject[] BuyBallButtons = new GameObject[5];
    public Text PointQuest1;
    static public int quest1point=0;
    public static int gems=0;
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
    public Text pointField2;
    public Text StandartCost;
    public Text StandartCostF2;
    public Text StopperCost;
    public Text StandartAfkCost;
    public Text StopperCooldownCost;
    public Text AfkPoints;
    public Text AfkPointsLast;
    public Text AfkPointText;
    public Text StopperText;
    public Text StopperCooldownText;
    public Text AfkPointTime;
    public Text[] Auto_flipper_text;
    public Text PointBuffText;
    public Text PointBuffTextF2;
    public Text PointBuffAfkText;
    public Text CostResetText;
   // public Text PointAfkText;
    public TimeSpan date;
    public Text time;
    Color mycolor = new Color(0.776f, 0.776f, 0.776f, 1.000f);
   public GameObject[] buttonBuyAutomod;
    public GameObject x2bonus;
    public GameObject BackPanel;
    public HingeJoint2D hj1;
    public GameObject textError;
    public GameObject textError2;
    public GameObject[] Automod_slider;
    public HingeJoint2D hj2;
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
    static public int PointField2 = 0;
    static public long PointSum;
    static public long[] PointsNow = new long[] { 0,0};
    static public bool[] automod= new bool[] { false, false };
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


    enum reward {x2reward, x3reward, x2expreward}
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
        StandartCost.text = NormalSum(StandartBuff.CostOnGrade[0]);
        StandartCostF2.text = NormalSum(StandartBuff.CostOnGrade[1]);
        StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
        if(Checker.costOnGrade!=0)
        StopperCost.text = NormalSum(Checker.costOnGrade);
        if(Checker.costOnCooldown!=0)
        StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        colorB = button1.GetComponent<Button>().colors;
        PointBuffText.text = StandartBuff.pointOnBit[0] + "→" + (StandartBuff.pointOnBit[0] + 1);
        PointBuffTextF2.text = StandartBuff.pointOnBit[1] + "→" + (StandartBuff.pointOnBit[1] + 1);
        if (Checker.costOnGrade != 0)
            StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers + 0.75);
        else
            StopperText.text = Checker.TimeStoppers + "";
        if (Checker.costOnCooldown != 0)
            StopperCooldownText.text = Checker.coldownStopper - 3 + "→" + (Checker.coldownStopper - 3 - 0.75);
        else
            StopperCooldownText.text = Checker.coldownStopper - 3 + "";
        PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        PointBuffTextRomb.text = Romb.PointLet + "→" + (Romb.PointLet + 1);
        isQuestStarted = false;
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("Gems", gems);
        PlayerPrefs.SetString("LeaveDate", DateTime.Now.ToString());
        PlayerPrefs.SetString("DarkTheme", DarkTheme.ToString());
        PlayerPrefs.SetString("PointSum", PointSum.ToString());
        PlayerPrefs.SetString("isBuyAutomod", Automod_slider[0].activeSelf.ToString());
        PlayerPrefs.SetString("isBuyAutomodF2", Automod_slider[1].activeSelf.ToString());
        PlayerPrefs.SetInt("CostOfGrade", StandartBuff.CostOnGrade[0]);
        PlayerPrefs.SetInt("CostOfAfk", AfkBuff.CostOnGrade);
        PlayerPrefs.SetInt("PointOnBit", StandartBuff.pointOnBit[0]);
        PlayerPrefs.SetInt("PointOnAfk", AfkBuff.pointOnBit);
        PlayerPrefs.SetInt("CountBall", Teleport.i[0]);
        PlayerPrefs.SetInt("CountBallF2", Teleport.i[1]);
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
        PlayerPrefs.SetInt("LostBall", BallsManager.ballsLost);
        PlayerPrefs.SetInt("questCompleted", BallsManager.questCompleted);
        PlayerPrefs.SetString("TextView", lvlTextPanel.activeSelf.ToString());
        for (int j = 0; j <4; j++)
        {
            PlayerPrefs.SetString($"BallsOpen[{j}]", BallsManager.isOpenBall[j].ToString());
        }
        for (int j=0; j < 10; j++)
        {
            PlayerPrefs.SetString($"QuestNewCompleate[{j}]", QuestManager.QuestsCompleate[j].ToString());
        }
        for (int j = 0; j < 10; j++)
        {
            PlayerPrefs.SetString($"BuyBallButtons[{j}]", BuyBallButtons[j].activeSelf.ToString());
        }
       
        if (PlayerPrefs.HasKey("Reward"))
        {
            if(bool.Parse(PlayerPrefs.GetString("Reward")))
            PlayerPrefs.SetInt("RewardPoints", (int)Math.Floor((date).TotalMinutes));
        }
        for (int j = 0; j < FieldManager.Fields.Length; j++)
        {
            PlayerPrefs.SetString($"FieldOpen[{j}]", FieldManager.Fields[j].ToString());
        }
        PlayerPrefs.SetInt("CostOfGradeF2", StandartBuff.CostOnGrade[1]);
        PlayerPrefs.SetInt("PointOnBitF2", StandartBuff.pointOnBit[1]);
        PlayerPrefs.SetString("Romb", StrongRomb.activeSelf.ToString());
        PlayerPrefs.SetInt("RombBuff", Romb.PointLet);
        PlayerPrefs.SetInt("RombCostBuff", RombCost);
        PlayerPrefs.SetInt("CircleBuff", Cirlce.MaxPointNeed);
        PlayerPrefs.SetInt("CircleCostBuff", CirleBuffCost);
        PlayerPrefs.SetString("Circle", (!buttonBuyCirlce.activeSelf).ToString());
        for (int j = 0; j < x2Areas.Length; j++)
        {
            PlayerPrefs.SetString($"x2Areas[{j}]", x2Areas[j].activeSelf.ToString());
        }

    }
    private void Update()
    {
       /*
        if (isQuestStarted)
            BottonPanel.transform.position = Vector3.MoveTowards(BottonPanel.transform.position, EndPointBottonPanel.transform.position, speed * Time.deltaTime);
        else if (!isQuestStarted)
            BottonPanel.transform.position = Vector3.MoveTowards(BottonPanel.transform.position, StartPointBottonPanel.transform.position, speed * Time.deltaTime);
            */
    }
    void LoadGame()
    {
        if (PlayerPrefs.HasKey("Gems"))
        {
            gems = PlayerPrefs.GetInt("Gems");
           
            Gems.text = $"{gems} ";
            LvlUpGems();
        }
        if (PlayerPrefs.HasKey("DarkTheme"))
        {
            if (bool.Parse(PlayerPrefs.GetString("DarkTheme")) && !DarkTheme)
            {
               //PlayerSettings.SplashScreen.unityLogoStyle = PlayerSettings.SplashScreen.UnityLogoStyle.LightOnDark;
                EnableDisableDark();
            }
        }
        if (PlayerPrefs.HasKey("Reward"))
        {
            Reward = bool.Parse(PlayerPrefs.GetString("Reward"));
            if(Reward)
            AfkPoints.text = PlayerPrefs.GetInt("RewardPoints")+"";
        }
        if (PlayerPrefs.HasKey("ExNum"))
        {
            if (bool.Parse(PlayerPrefs.GetString("ExNum"))&& !ExNum)
            {   
                ExNum1();
            }
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
           if(float.Parse(PlayerPrefs.GetString("Sound"))<1 && AudioListener.volume>0.9)
            Sounds();
        }
        if (PlayerPrefs.HasKey("LeaveDate") )
        {
            if(!Reward)
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
                Auto_flipper_text[0].text = "Auto-flippers";
            Automod_slider[0].SetActive(flag);
            buttonBuyAutomod[0].SetActive(!flag);
        }
        if (PlayerPrefs.HasKey("isBuyAutomodF2"))
        {

            bool flag = bool.Parse(PlayerPrefs.GetString("isBuyAutomodF2"));
            if (flag)
                Auto_flipper_text[1].text = "Auto-flippers";
            Automod_slider[1].SetActive(flag);
            buttonBuyAutomod[1].SetActive(!flag);
        }
        if (PlayerPrefs.HasKey("isBuyAutomodF2"))
        {
            
           bool flag =  bool.Parse(PlayerPrefs.GetString("isBuyAutomodF2"));
            if (flag)
                Auto_flipper_text[1].text = "Auto-flippers";
            Automod_slider[1].SetActive(flag);
            buttonBuyAutomod[1].SetActive(!flag);
        }
        if (PlayerPrefs.HasKey("CostOfGrade"))
        {
            StandartBuff.CostOnGrade[0] = PlayerPrefs.GetInt("CostOfGrade");
        
        }
        if (PlayerPrefs.HasKey("CostOfAfk"))
        {
            AfkBuff.CostOnGrade = PlayerPrefs.GetInt("CostOfAfk");
           
        }
        if (PlayerPrefs.HasKey("PointOnBit"))
        {
            StandartBuff.pointOnBit[0]= PlayerPrefs.GetInt("PointOnBit");
        }
        if (PlayerPrefs.HasKey("PointOnAfk"))
        {
            AfkBuff.pointOnBit = PlayerPrefs.GetInt("PointOnAfk");
        }
        if (PlayerPrefs.HasKey("CountBall"))
        {
            Teleport.i[0] = PlayerPrefs.GetInt("CountBall");
            if (Teleport.i[0] > 0)
            {
                if (Teleport.i[0] > 5)
                {
                    Teleport.i[0] = 5;
                }
                NumberOfBall.text = $"Number of extra ball  {Teleport.i[0]+Teleport.i[1]}\\10";
            }
            else
                NumberOfBall.text = $"Number of extra ball  0\\10";
        }
        if (PlayerPrefs.HasKey("CountBallF2"))
        {
            Teleport.i[1] = PlayerPrefs.GetInt("CountBallF2");
            if (Teleport.i[1] > 0)
            {
                if (Teleport.i[1] > 5)
                {
                    Teleport.i[1] = 5;
                }
                NumberOfBall.text = $"Number of extra ball  {Teleport.i[0]+Teleport.i[1]}\\10";
            }
        }
        if (PlayerPrefs.HasKey("MaximumPoint"))
        {
            maximumPoint = PlayerPrefs.GetInt("MaximumPoint");
            MaxPoint.text = $"Record:   {maximumPoint}";
        }
        if (PlayerPrefs.HasKey("TimeStoppers"))
        {
          
                Checker.TimeStoppers = PlayerPrefs.GetFloat("TimeStoppers");
            if (Checker.TimeStoppers >= 5)
            {
                Checker.costOnGrade = 0;
                StopperBuff();
            }
        }
        if(PlayerPrefs.HasKey("coldownStopper"))
        {
            Checker.coldownStopper = PlayerPrefs.GetFloat("coldownStopper");
            if (Checker.coldownStopper <= 6)
            {
                Checker.costOnCooldown = 0;
                StopperBuffCooldown();
            }
        }
        if(PlayerPrefs.HasKey("costStopper"))
        {
            if (Checker.TimeStoppers < 5)
                Checker.costOnGrade = long.Parse(PlayerPrefs.GetString("costStopper"));
            else
                Checker.costOnGrade = 0;
        }
        if (PlayerPrefs.HasKey("PointSent"))
        {
            POintSpent = long.Parse(PlayerPrefs.GetString("PointSent"));
            if (POintSpent > 1000000)
                UnlockFoughtBall();
            SumSent.text = $"Total points spent:     {POintSpent}";
        }
        else
            SumSent.text = $"Total points spent:     0";
        if (PlayerPrefs.HasKey("costColdownStopper"))
        {
            if (Checker.coldownStopper > 6)
                Checker.costOnCooldown = long.Parse(PlayerPrefs.GetString("costColdownStopper"));
            else
                Checker.costOnCooldown = 0;
        }
        if (PlayerPrefs.HasKey("QuestNewCompleate[0]"))
        {
            for (int j = 0; j < 10; j++)
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
            for (int j = 0; j < 10; j++)
            {
               BuyBallButtons[j].SetActive(bool.Parse( PlayerPrefs.GetString($"BuyBallButtons[{j}]")));
            }
        }  
        if (PlayerPrefs.HasKey("lvl"))
        {
            lvl = PlayerPrefs.GetInt("lvl");
            if (lvl == 0)
                lvl = 1;
            let.lvltext.text = $" level - {GameManager.lvl}";
            let.lvlbuff.text = $"x{GameManager.lvl}";
        }
        if (PlayerPrefs.HasKey("TextView"))
        {
           
            if (!bool.Parse(PlayerPrefs.GetString("TextView")))
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
        if (PlayerPrefs.HasKey("FieldOpen[0]"))
        {
            for (int j = 0; j < FieldManager.Fields.Length; j++)
            {
                FieldManager.Fields[j] = bool.Parse( PlayerPrefs.GetString($"FieldOpen[{j}]"));
                if (FieldManager.Fields[j] && j>0)
                    Fm._buyNewField(j-1);
                
            }
        }
        if (PlayerPrefs.HasKey("CostOfGradeF2"))
        {
            StandartBuff.CostOnGrade[1]= PlayerPrefs.GetInt("CostOfGradeF2");
            StandartCostF2.text = NormalSum(StandartBuff.CostOnGrade[1]);
        }
        if (PlayerPrefs.HasKey("PointOnBitF2"))
        {
            StandartBuff.pointOnBit[1] = PlayerPrefs.GetInt("PointOnBitF2");
            PointBuffTextF2.text = StandartBuff.pointOnBit[1] + "→" + (StandartBuff.pointOnBit[1] + 1);
        }
        if (PlayerPrefs.HasKey("Romb"))
        {
            bool flag = bool.Parse(PlayerPrefs.GetString("Romb"));
            if(flag)
            _buyStrongRombF2();
        }
        if (PlayerPrefs.HasKey("RombBuff"))
        {
            Romb.PointLet = PlayerPrefs.GetInt("RombBuff");
            PointBuffTextRomb.text = Romb.PointLet + "→" + (Romb.PointLet + 1);
        }
        if (PlayerPrefs.HasKey("RombCostBuff"))
        {
            RombCost = PlayerPrefs.GetInt("RombCostBuff");
            RombCostBuff.text = NormalSum(RombCost);
        }
        if (PlayerPrefs.HasKey("CircleBuff"))
        {
            Cirlce.MaxPointNeed = PlayerPrefs.GetInt("CircleBuff");
            CircleBuffText.text = Cirlce.MaxPointNeed + "→" + (Cirlce.MaxPointNeed - 1);
            if (Cirlce.MaxPointNeed < 4)
                StandartBuffCircle();
        }
        if (PlayerPrefs.HasKey("CircleCostBuff"))
        {
            CirleBuffCost = PlayerPrefs.GetInt("CircleCostBuff");
            if(CirleBuffCost>0)
            CircleTextBuffCost.text = NormalSum(CirleBuffCost);
        }
        if (PlayerPrefs.HasKey("Circle"))
        {
            bool flag = bool.Parse( PlayerPrefs.GetString("Circle"));
            if (flag)
                _buySafeCircleF2();
        }
        if(PlayerPrefs.HasKey("BallsOpen[0]"))
            for(int j=0; j < 4; j++)
            {
                BallsManager.isOpenBall[j] = bool.Parse(PlayerPrefs.GetString($"BallsOpen[{j}]"));
            }
        if (PlayerPrefs.HasKey("LostBall"))
        {
            BallsManager.ballsLost = PlayerPrefs.GetInt("LostBall");
            if (BallsManager.ballsLost > 1000)
                UnlockThirdBall();
        }

        if (PlayerPrefs.HasKey("questCompleted"))
        {
            BallsManager.questCompleted = PlayerPrefs.GetInt("questCompleted");
            if (BallsManager.questCompleted >= 10)
                UnlockSecondBall();
        }
        if (PlayerPrefs.HasKey("x2Areas[0]")) {
            for (int j = 0; j < x2Areas.Length; j++)
            {
                x2Areas[j].SetActive(bool.Parse(PlayerPrefs.GetString($"x2Areas[{j}]")));
                if (j!= x2Areas.Length-1 && x2Areas[j].activeSelf)
                {
                    x2AreasCostText.text = NormalSum(x2AreasCost[j + 1]);
                }
               
              }
            if (x2Areas[x2Areas.Length-1].activeSelf)
            {
                Buyx2Areas();
            }
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("reward", false.ToString());
    }
    private void OnApplicationFocus(bool focus)
    {
        OnApplicationPause(!focus);
        
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
            StandartCost.text = NormalSum(StandartBuff.CostOnGrade[0]);
            StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
            StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
            
            PointBuffText.text = StandartBuff.pointOnBit[0] + "→" + (StandartBuff.pointOnBit[0] + 1);
            PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
           
           
        }
    }
    public void QuestNumber(int i)
    {
        NumberQuest = i;
    }
    public void Automod(GameObject image)
    {
        hj1.useMotor = false;
        hj2.useMotor = false;
        automod[FieldManager.CorrectField] = !automod[FieldManager.CorrectField];

        if (automod[FieldManager.CorrectField])
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
        if (PointSum >= StandartBuff.CostOnGrade[0])
        {
            PointSum -= StandartBuff.CostOnGrade[0];
            POintSpent += StandartBuff.CostOnGrade[0];
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            StandartBuff.pointOnBit[0] += 1;
            StandartBuff.CostOnGrade[0] = (int)(StandartBuff.CostOnGrade[0] * 1.2f);
            StandartCost.text =NormalSum(StandartBuff.CostOnGrade[0]);
            PointBuffText.text = StandartBuff.pointOnBit[0] + "→" + (StandartBuff.pointOnBit[0] + 1);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }


    public void StandartBuff2()
    {
        if (PointSum >= StandartBuff.CostOnGrade[1])
        {
            PointSum -= StandartBuff.CostOnGrade[1];
            POintSpent += StandartBuff.CostOnGrade[1];
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            StandartBuff.pointOnBit[1] += 1;
            StandartBuff.CostOnGrade[1] = (int)(StandartBuff.CostOnGrade[1] * 1.2f);
            StandartCostF2.text = NormalSum(StandartBuff.CostOnGrade[1]);
            PointBuffTextF2.text = StandartBuff.pointOnBit[1] + "→" + (StandartBuff.pointOnBit[1] + 1);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }
    int RombCost = 1000;
    public void StandartBuffRomb()
    {
        if (PointSum >= RombCost)
        {
            PointSum -= RombCost;
            POintSpent += RombCost;
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            Romb.PointLet += 1;
            RombCost = (int)(RombCost * 1.2f);
            RombCostBuff.text = NormalSum(RombCost);
            PointBuffTextRomb.text = Romb.PointLet + "→" + (Romb.PointLet + 1);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }
   int CirleBuffCost = 5000;
    public void StandartBuffCircle()
    {
        if (PointSum >= CirleBuffCost)
        {
            if (Cirlce.MaxPointNeed > 3)
            {
                PointSum -= CirleBuffCost;
                POintSpent += CirleBuffCost;
                SumSent.text = $"Total points spent:     {POintSpent}";
                if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
                {
                    BallsManager.isOpenBall[3] = true;
                    UnlockFoughtBall();
                }
                pointSum.text = NormalSum(GameManager.PointSum);

                Cirlce.MaxPointNeed--;
                CirleBuffCost = (int)(CirleBuffCost * 1.2f);
                CircleTextBuffCost.text = NormalSum(CirleBuffCost);
                CircleBuffText.text = Cirlce.MaxPointNeed + "→" + (Cirlce.MaxPointNeed - 1);
            }
            if (Cirlce.MaxPointNeed <= 3)
            {
                TextDown(CircleTextBuffCost.gameObject);
                CircleBuffButton.GetComponent<Button>().enabled = false;
                CircleBuffButton.GetComponent<Image>().sprite = MaxBuff;
                CircleBuffText.text = Cirlce.MaxPointNeed + "";
                CirleBuffCost = 0;
                CircleTextBuffCost.text = "MAX";
            }
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }

    public void Sounds()
    {
        if (AudioListener.volume >0.9)
        {
            AudioListener.volume = 0;
            Sound.sprite = SoundOff;
        }
        else
        {

            Sound.sprite = SoundOn;
            AudioListener.volume = 1;
        }
        
    }

    public void StopperBuff()
    {
        if (PointSum >= Checker.costOnGrade)
        {
            if (Checker.TimeStoppers < 5)
            {
                PointSum -= Checker.costOnGrade;
                POintSpent += Checker.costOnGrade;
                SumSent.text = $"Total points spent:     {POintSpent}";
                if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
                {
                    BallsManager.isOpenBall[3] = true;
                    UnlockFoughtBall();
                }
                pointSum.text = NormalSum(GameManager.PointSum);
                Checker.TimeStoppers += 0.75f;
                Checker.costOnGrade = (long)(Checker.costOnGrade * 1.2f);
                StopperCost.text = NormalSum(Checker.costOnGrade);
                StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers + 0.75);
            }
            if (Checker.TimeStoppers >= 5)
            {
                
                TextDown(StopperCost.gameObject);
                StoppersBuffButton.GetComponent<Button>().enabled = false;
                StopperTime.sprite = MaxBuff;
                StopperText.text = Checker.TimeStoppers + "";
                StopperCost.text = "MAX";
            }
        }
        else
        {
            textError.SetActive(true);
            Checker.costOnGrade = 0;
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }

    }
    public void Buyx2Areas()
    {
        int g =0;
        int cost = 0;
        if (x2Areas[0].activeSelf)
            g = 1;
        if (x2Areas[1].activeSelf)
            g = 2;
        if (x2Areas[2].activeSelf)
            g = 3;
        if (g < 3) {
            cost = x2AreasCost[g];
            if (PointSum >= cost)
            {
                PointSum -= cost;
                POintSpent += cost;
                SumSent.text = $"Total points spent:     {POintSpent}";
                if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
                {
                    BallsManager.isOpenBall[3] = true;
                    UnlockFoughtBall();
                }
                pointSum.text = NormalSum(GameManager.PointSum);

                x2Areas[g].SetActive(true);
                if (g < 2)
                    x2AreasCostText.text = NormalSum(x2AreasCost[g + 1]);
                else
                {
                    Buyx2Areas();
                    x2AreasCostText.text = "MAX";
                }
            }
            else
            {
                textError.SetActive(true);
                StartCoroutine(ViewText(textError));
                StopCoroutine(ViewText(textError));
            }
        }
        else
        {
            TextDown(x2AreasCostText.gameObject);
            x2areasBuyButton.GetComponent<Button>().enabled = false;
            x2areasBuyButton.GetComponent<Image>().sprite = MaxBuff;
            x2AreasCostText.text = "MAX";
        }

}
    public void StopperBuffCooldown()
    {
        if (PointSum >= Checker.costOnCooldown)
        {
            if (Checker.coldownStopper > 6)
            {
                PointSum -= Checker.costOnCooldown;
                POintSpent += Checker.costOnCooldown;
                SumSent.text = $"Total points spent:     {POintSpent}";
                if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
                {
                    BallsManager.isOpenBall[3] = true;
                    UnlockFoughtBall();
                }
                pointSum.text = NormalSum(GameManager.PointSum);

                Checker.coldownStopper -= 0.75f;
                Checker.costOnCooldown = (long)(Checker.costOnCooldown * 1.2f);
                StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
                StopperCooldownText.text = Checker.coldownStopper + "→" + (Checker.coldownStopper - 0.75);
            }
            if (Checker.coldownStopper <= 6)
            {
                TextDown(StopperCooldownCost.gameObject);
                StoppersBuffCooldownButton.GetComponent<Button>().enabled = false;
                StopperCooldown.sprite = MaxBuff;
                StopperCooldownText.text = Checker.coldownStopper+"";
                Checker.costOnCooldown = 0;
                StopperCooldownCost.text = "MAX";
            }
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
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
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
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            Auto_flipper_text[0].text = "Auto-flippers";
            Automod_slider[0].SetActive(true);
            buttonBuyAutomod[0].SetActive(false);
        }
        else
        {
            textError.SetActive(true);
           StartCoroutine(ViewText(textError));
           StopCoroutine(ViewText(textError));
        }
    }

    public void BuyAutomodF2()
    {
        if (PointSum >= 100000)
        {
            PointSum -= 100000;
            POintSpent += 100000;
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            Auto_flipper_text[1].text = "Auto-flippers";
            Automod_slider[1].SetActive(true);
            buttonBuyAutomod[1].SetActive(false);
        }
        else
        {
            textError.SetActive(true);
           StartCoroutine(ViewText(textError));
           StopCoroutine(ViewText(textError));
        }
    }

    public void BuyStrongRombF2()
    {
        if (PointSum >= 20000)
        {
            PointSum -= 20000;
            POintSpent += 20000;
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            _buyStrongRombF2();
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }
    }

    void _buyStrongRombF2()
    {
        RombBuy.text = "Strong Romb";
        RombButton.SetActive(false);
        StrongRomb.SetActive(true);
        RombBaff.SetActive(true);
    }

    public void BuySafeCircleF2()
    {
        if (PointSum >= 20000)
        {
            PointSum -= 20000;
            POintSpent += 20000;
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
            pointSum.text = NormalSum(GameManager.PointSum);
            _buySafeCircleF2();
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(ViewText(textError));
            StopCoroutine(ViewText(textError));
        }
    }

    void _buySafeCircleF2()
    {
        CircleBuffPanel.SetActive(true);
        Cirlce.PointNeed = Cirlce.MaxPointNeed;
        CircleF2.text = "Safe Circle";
        buttonBuyCirlce.SetActive(false);
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
            ContentShop1[FieldManager.CorrectField].SetActive(true);
            for(int h = 0; h< ContentShop1.Length; h++)
            {
                if(h!=FieldManager.CorrectField)
                ContentShop1[h].SetActive(false);
            }
            if (DarkTheme)
            {
                button1.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else
                button1.GetComponent<Image>().color = mycolor;

            }
        else
            {
            if (DarkTheme)
            {
                button1.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
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
            ContentShop2[FieldManager.CorrectField].SetActive(true);
            for (int h = 0; h < ContentShop1.Length; h++)
            {
                if (h != FieldManager.CorrectField)
                    ContentShop2[h].SetActive(false);
            }
            if (DarkTheme)
            {
                button2.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else
                button2.GetComponent<Image>().color = mycolor;

            }
            else
            {
            if (DarkTheme)
            {
                button2.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
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
            Scroll[2].content = ContentShop3[FieldManager.CorrectField].GetComponent<RectTransform>();
            ContentShop3[FieldManager.CorrectField].SetActive(true);
            for (int h = 0; h < ContentShop1.Length; h++)
            {
                if (h != FieldManager.CorrectField)
                    ContentShop3[h].SetActive(false);
            }
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
                button3.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else
                button3.GetComponent<Image>().color = mycolor;

        }
        else
        {
            if (DarkTheme)
            {
                button3.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
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
                button4.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else
                button4.GetComponent<Image>().color = mycolor;

        }
        else
        {
            if (DarkTheme)
            {
                button4.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            else
                button4.GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF); 
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
                button6.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else
                button6.GetComponent<Image>().color = mycolor;
        }
        else
        {
            if (DarkTheme)
            {
                button6.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
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
                button5.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else
            button5.GetComponent<Image>().color = mycolor;
        }
        else
        {
            if (DarkTheme)
            {
                button5.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF); 
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
    public void ExNum1()
    {
        
       
        ExNum = !ExNum;
        if (ExNum)
            Number.sprite = NumberOn;
        else
            Number.sprite = NumberOff;

    }


    public void ScorePoint()
    {
        // Debug.Log(date.TotalMinutes.ToString());
        Debug.Log(Math.Floor(date.TotalMinutes).ToString());
        Debug.Log(Reward.ToString());
        if ((long)Math.Floor((date).TotalMinutes) > 0 && !Reward)
        {
            if (!Reward)
            {
                PointSum += (long)Math.Floor((date).TotalMinutes) * AfkBuff.pointOnBit; //+ (long)(Math.Floor((date).TotalMinutes) / 10) + AfkBuff.pointOnBit;
                string days = date.Days != 0 ? date.Days.ToString() + " days" : "";
                string hours = date.Hours != 0 ? date.Hours.ToString() + " h" : "";
                AfkPointText.text = "While you were leavig:";
                AfkPointTime.text = $"{days} {hours} {date.Minutes} min";
                AfkPoints.text = $" {(long)Math.Floor((date).TotalMinutes) * AfkBuff.pointOnBit}";
                AfkPointsLast.text = "points";
            }
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


    public void NewGame()
    {
        Attention.SetActive(true);
    }





    public void AcceptNewGame()
    {
        for (int h = 0; h < PointsNow.Length; h++)
        {
            PointsNow[h] = 0;
        }
        gems += lvl;
        Gems.text = $"{gems} ";
        lvl = 1;
        LetsScript.hitneeded = 500;
        LvlUpGems();
        lvlBuff.text = $" level - {lvl}";
        lvlbuff.text = $"x{lvl}";
        Teleport.i[0] = 0;
        Teleport.i[1] = 0;
        StandartBuff.pointOnBit[0] = 1;
        StandartBuff.pointOnBit[1] = 1;
        AfkBuff.pointOnBit = 1;
        maximumPoint = 0;
        MaxPoint.text = $"Record:    0";
       
        Checker.coldownStopper = 12;
        Checker.TimeStoppers = 3;
        Checker.costOnGrade = 1000;
        Checker.costOnCooldown = 1000;
        StandartBuff.CostOnGrade[0] = 100;
        StandartBuff.CostOnGrade[1] = 1000;

        StopperText.text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers + 0.75);
        StopperCooldownText.text = Checker.coldownStopper - 3 + "→" + (Checker.coldownStopper - 3 - 0.75);
        StopperCost.text = NormalSum(Checker.costOnGrade);
        StopperCooldownCost.text = NormalSum(Checker.costOnCooldown);
        StopperTextMain.text = "Buy \"Stoppers\"";
        StoppersButton.SetActive(true);
        stopperPanel.SetActive(false);
        stopperPanelCooldown.SetActive(false);
        Checkers.SetActive(false);
        AfkBuff.CostOnGrade = 100;
        NumberOfBall.text = $"Number of extra ball  0\\10";
        StandartCost.text = NormalSum(StandartBuff.CostOnGrade[0]);
        StandartCostF2.text = NormalSum(StandartBuff.CostOnGrade[1]);
        StandartAfkCost.text = NormalSum(AfkBuff.CostOnGrade);
        Point = 0;
        PointsNow[0] = 0;
        PointsNow[1] = 0;
        PointField2 = 0;
        PointSum = 0;
        pointSum.text = NormalSum(GameManager.PointSum);
        point.text = "" + 0;
        Auto_flipper_text[0].text = "Buy auto-flippers";
        Auto_flipper_text[1].text = "Buy auto-flippers";
        Automod_slider[0].SetActive(false);
        Automod_slider[1].SetActive(false);
        buttonBuyAutomod[0].SetActive(true);
        buttonBuyAutomod[1].SetActive(true);
        automod[0] = false;
        automod[1] = false;
        PointBuffText.text = StandartBuff.pointOnBit[0] + "→" + (StandartBuff.pointOnBit[0] + 1);
        PointBuffTextF2.text = StandartBuff.pointOnBit[1] + "→" + (StandartBuff.pointOnBit[1] + 1);
        PointBuffAfkText.text = AfkBuff.pointOnBit + "→" + (AfkBuff.pointOnBit + 1);
        for (int j = 1; j < 6; j++)
            Teleport.mainballsstatic[j].SetActive(false);
        for (int j = 0; j < 10; j++)
        {
            QuestManager.QuestsCompleate[j] = false;
            PanelQuest[j].SetActive(true);
            BuyBallButtons[j].SetActive(false);
            Lock[j].GetComponent<Image>().sprite = DefaultLock;
        }
        if (x2Areas[x2Areas.Length - 1].activeSelf)
        {
            TextUp(x2AreasCostText.gameObject);
            x2areasBuyButton.GetComponent<Button>().enabled = true;
            x2areasBuyButton.GetComponent<Image>().sprite = ButtonBuy;
        }
        for (int i = 0; i < x2Areas.Length; i++)
        {
            x2Areas[i].SetActive(false);
        }
        x2AreasCostText.text = NormalSum(x2AreasCost[0]);
        CircleBuffPanel.SetActive(false);
        Cirlce.PointNeed = Cirlce.MaxPointNeed = 10;
        CircleF2.text = "Buy Safe Circle";
        buttonBuyCirlce.SetActive(true);
        RombBuy.text = "Buy Strong Romb";
        RombButton.SetActive(true);
        StrongRomb.SetActive(false);
        RombBaff.SetActive(false);
        if (StopperCooldown.sprite == MaxBuff)
        {
            TextUp(StopperCooldownCost.gameObject);
            StopperCooldown.sprite = ButtonBuy;
        }
        if (StopperTime.sprite == MaxBuff)
        {
            TextUp(StopperCost.gameObject);
            StopperTime.sprite = ButtonBuy;
        }
        Romb.PointLet = 1;
        RombCost = 1000;
        RombCostBuff.text = NormalSum(RombCost);
        PointBuffTextRomb.text = Romb.PointLet + "→" + (Romb.PointLet + 1);
        if(CirleBuffCost == 0)
        {
            TextUp(CircleTextBuffCost.gameObject);
            CircleBuffButton.GetComponent<Button>().enabled = true;
            CircleBuffButton.GetComponent<Image>().sprite = ButtonBuy;
        }
        CirleBuffCost =5000;
        CircleTextBuffCost.text = NormalSum(CirleBuffCost);
        CircleBuffText.text = Cirlce.MaxPointNeed + "→" + (Cirlce.MaxPointNeed - 1);
    }
    

    public void StartQuest(string textQuest)
    {
        QuestPanel.SetActive(true);
        QuestsText.text = textQuest;
        Button3();
        pointField2.gameObject.SetActive(false);
        x2bonus.SetActive(false);
        x2expbonus.SetActive(false);
        if (Arrows.Length > FieldManager.CorrectField * 2)
        Arrows[FieldManager.CorrectField*2].SetActive(false);
        if (FieldManager.CorrectField != 0)
        Arrows[FieldManager.CorrectField*2-1].SetActive(false);
        StartCoroutine(ShowButton());
        if (FieldManager.CorrectField > 0)
            Checker.isAllChecked = false;
        let.PointsNow[FieldManager.CorrectField].gameObject.SetActive(false);
        for(int h = 0; h < Fm.FieldsAll.Length; h++)
        {
            if (h != FieldManager.CorrectField)
            {
                Fm.FieldsAll[h].SetActive(false);
            }
        }
    }
      public void BuyStopper()
    {
        if (PointSum >= 20000)
        {
            PointSum -= 20000;
            POintSpent += 20000;
            SumSent.text = $"Total points spent:     {POintSpent}";
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }
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
    public void DorewardExp()
    {
        if (Advertisement.IsReady())
        {

            MyReward = reward.x2expreward;
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
    IEnumerator TimeBuff(int[] were)
    {

        for (int i = 30; i > 0; i--)
        {
            (time).text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        time.text = "";
        StandartBuff.pointOnBit[0] -= were[0];
        StandartBuff.pointOnBit[1] -= were[1];
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
            if (POintSpent >= 1000000 && !BallsManager.isOpenBall[3])
            {
                BallsManager.isOpenBall[3] = true;
                UnlockFoughtBall();
            }               
            pointSum.text = NormalSum(GameManager.PointSum);
            Teleport.i[FieldManager.CorrectField]++;
            NumberOfBall.text = $"Number of extra ball  {Teleport.i[0]+Teleport.i[1]}\\10";
            BuyBallButtons[i+FieldManager.CorrectField*5].SetActive(false);
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
        BallsManager.questCompleted++;
        if (BallsManager.questCompleted == 10 && !BallsManager.isOpenBall[1])
        {
            BallsManager.isOpenBall[1] = true;
            UnlockSecondBall();
        }
        BlockCanvas.SetActive(true);
        Button3();
        QuestManager.QuestsCompleate[NumberQuest + FieldManager.CorrectField * 5] = true;
        ButtonQuests[NumberQuest+FieldManager.CorrectField*5].GetComponent<Image>().sprite = CompleatedImage;
        StartCoroutine(BackScreen(PanelQuest[NumberQuest + FieldManager.CorrectField * 5]));
        

    }

    IEnumerator BackScreen(GameObject Go)
    {
        Lock[NumberQuest + FieldManager.CorrectField * 5].GetComponent<Image>().sprite=CompleatedImageLock;
        while (Go.transform.position.x<QuestBlock.transform.position.x)
        {
            Go.transform.position = new Vector2(Go.transform.position.x+ 1f * Time.deltaTime,Go.transform.position.y);
            yield return new WaitForFixedUpdate();
        }
        BlockCanvas.SetActive(false);
        isQuestStarted = false;
        BuyBallButtons[NumberQuest + FieldManager.CorrectField * 5].SetActive(true);
        Go.SetActive(false);
      
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
        if (MyReward == reward.x3reward && placementId != "Interstitial_Android")
        {
            AfkPoints.text = $"{3 * (long)(Math.Floor((date).TotalMinutes)) * AfkBuff.pointOnBit}";
            PointSum += (long)(Math.Floor((date).TotalMinutes)) * 3 * AfkBuff.pointOnBit;
            Reward = true;
            PlayerPrefs.SetString("Reward", Reward.ToString());
        }
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
            PlayerPrefs.SetString("Reward", Reward.ToString());
        }
    }
    public void Stop()
    {
        let.PointsNow[FieldManager.CorrectField].gameObject.SetActive(true);
        isQuestStarted = false;
        if(FieldManager.CorrectField==0)
        point.gameObject.SetActive(true);
        lvlTextPanel.SetActive(true);
        if (FieldManager.CorrectField == 1)
            pointField2.gameObject.SetActive(true);
        pointSum.gameObject.SetActive(true);
        BottonPanel.SetActive(true);
        ProgressBarGM.SetActive(true);
        StopButton.SetActive(false);
        ProgressBar.gameObject.SetActive(true);
        NumberQuest = -1;
            if (Arrows.Length > FieldManager.CorrectField * 2 && FieldManager.Fields[FieldManager.CorrectField+1])
                Arrows[FieldManager.CorrectField * 2].SetActive(true);
            if (FieldManager.CorrectField != 0)
                Arrows[FieldManager.CorrectField * 2 - 1].SetActive(true);
        
        for (int h = 0; h < Fm.FieldsAll.Length; h++)
        {
            if (h != FieldManager.CorrectField && FieldManager.Fields[h])
            {
                Fm.FieldsAll[h].SetActive(true);
            }
        }
        if (NumberQuest == 0)
            PointQuest1.gameObject.SetActive(false);
        else if (NumberQuest == 1)
        {
            TimeQuest2.gameObject.SetActive(false);
            StopCoroutine(Quest2());
          
        }
        else if(NumberQuest == 2)
        {
            Fm.FieldsAll[FieldManager.CorrectField].transform.localScale *= 5;
            MainCamera.orthographicSize = 5;
            PointQuest1.gameObject.SetActive(false);
        }
        else if(NumberQuest == 3)
        {
            PointQuest1.gameObject.SetActive(false);
            StopCoroutine(CameraRotation());
            MainCamera.transform.rotation = new Quaternion(MainCamera.transform.rotation.x, MainCamera.transform.rotation.y, 0, 0);
            Fm.FieldsAll[FieldManager.CorrectField].transform.rotation = new Quaternion(Fm.FieldsAll[FieldManager.CorrectField].transform.rotation.x, Fm.FieldsAll[FieldManager.CorrectField].transform.rotation.y, 0, 0);


        }
        else if(NumberQuest == 4)
        {

            PointQuest1.gameObject.SetActive(false);
            MainCamera.orthographicSize = 5;
            Fm.FieldsAll[FieldManager.CorrectField].transform.localScale /= 5;
            Fm.FieldsAll[FieldManager.CorrectField].transform.localPosition = new Vector3(0, 4.2f, 1);
            MainCamera.transform.position = new Vector3(0f, 0f, MainCamera.transform.position.z);
           
        }
    }
   
    public void StartQuest()
    {
        BottonPanel.SetActive(false);
        isQuestStarted = true;
        lvlTextPanel.SetActive(false);
        ProgressBarGM.SetActive(false);
        StopButton.SetActive(true);
        quest1point = 0;
        ProgressBar.gameObject.SetActive(false);
        StartCoroutine(Expx2());
        switch (NumberQuest)
        {
            case 0:
                PointQuest1.text = "0\\30";
                PointQuest1.gameObject.SetActive(true);
                break;
            case 1:
                TimeQuest2.gameObject.SetActive(true);
                StartCoroutine(Quest2());
                break;
            case 2:
                PointQuest1.text = "0\\20";
                PointQuest1.gameObject.SetActive(true);
                MainCamera.orthographicSize = 25;
                Fm.FieldsAll[FieldManager.CorrectField].transform.localScale /= 5;
                
                break;
            case 3:
                PointQuest1.text = "0\\20";
                PointQuest1.gameObject.SetActive(true);
                StartCoroutine(CameraRotation());
               
                break;
            case 4:
                PointQuest1.text = "0\\20";
                PointQuest1.gameObject.SetActive(true);
                Fm.FieldsAll[FieldManager.CorrectField].transform.localScale *= 5;
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
            Fm.FieldsAll[FieldManager.CorrectField].transform.Rotate(new Vector3(0, 0, -1), Space.Self);
            // MainCamera.transform.rotation =Quaternion.AngleAxis(90, new Vector3( 0,0,1));
            yield return new WaitForFixedUpdate();
        }
       
        if (quest1point >= 20)
        {
            Stop();
            QuestManager.QuestsCompleate[3+FieldManager.CorrectField*5] = true;
            QuestCompeate.SetActive(true);
            
        }
        else
        {
            Fm.FieldsAll[FieldManager.CorrectField].transform.rotation = new Quaternion(Fm.FieldsAll[FieldManager.CorrectField].transform.rotation.x, Fm.FieldsAll[FieldManager.CorrectField].transform.rotation.y, 0, 0);
            MainCamera.transform.rotation = new Quaternion(MainCamera.transform.rotation.x, MainCamera.transform.rotation.y, 0, 0);
            //BottonPanel.transform.position = new Vector2(0, -7);
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
            QuestManager.QuestsCompleate[0+FieldManager.CorrectField*5] = true;
            QuestCompeate.SetActive(true);
        }
       
    }

    public void LvltextView() {

        if (!lvlTextPanel.activeSelf)
        {
            lvlTextPanel.SetActive(true);
            LvlText.sprite = LvlTextOn;

        }
        else
        {
            lvlTextPanel.SetActive(false);
            LvlText.sprite = LvlTextOff;
        }
        
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
                int[] variabled =new int[] { StandartBuff.pointOnBit[0], StandartBuff.pointOnBit[1] };
                StandartBuff.pointOnBit[0] *= 2;
                StandartBuff.pointOnBit[1] *= 2;
                StartCoroutine(TimeBuff(variabled));
                StartCoroutine(ShowButton());
                PlayerPrefs.SetString("Reward", Reward.ToString());
            }
            else if (MyReward == reward.x2expreward)
            {
                
                Reward = false;
                lvlBuff.color = Color.yellow;
                x2expbonus.SetActive(false);
                LetsScript.exp = 2;
                StartCoroutine(Expx2());
                PlayerPrefs.SetString("Reward", Reward.ToString());
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
        PlayerPrefs.SetString("Reward", Reward.ToString());
    }
    IEnumerator Expx2()
    {
        if(!isQuestStarted)
        for(int i = 60; i>0; i--)
        {
            timeExpReward.text = i + "";
            yield return new WaitForSeconds(1f);
        }
        LetsScript.exp = 1;
        timeExpReward.text = "";
        lvlBuff.color = Color.white;
        int g = 1;
        while(g>0)
        if (!isQuestStarted)
        {
            x2expbonus.SetActive(true);
            g--;
        }
        else
        {
            yield return new WaitForSeconds(60f);
        }

    }
    public void EnableDisableDark()
    {
        DarkTheme = !DarkTheme;
        if (DarkTheme)
        {
            Dark.sprite = DarkOn;  
        }
        else
        {
            Dark.sprite = DarkOff;
        }
       
        if (DarkTheme)
        {
            MiniField[0].sprite = MiniFieldDark[0];
            spawnPoint.sprite = spawnPointDark;
            spawnPointF2.sprite = spawnPointDark;
            ColorBlock cb = button1.GetComponent<Button>().colors;
            cb.normalColor = Color.white;// new Color32(0x16, 0x71, 0x99, 0xFF);
            cb.pressedColor = new Color32(0x0E, 0x43, 0x60, 0xFF);
            cb.selectedColor = Color.white; //new Color32(0x16, 0x71, 0x99, 0xFF);
            cb.highlightedColor = Color.white;// new Color32(0x16, 0x71, 0x99, 0xFF);
            cb.disabledColor = Color.white; //new Color32(0x16, 0x71, 0x99, 0xFF);
            
            button1.GetComponent<Button>().colors = cb;
            button2.GetComponent<Button>().colors = cb;
            button3.GetComponent<Button>().colors = cb;
            button4.GetComponent<Button>().colors = cb;
            button5.GetComponent<Button>().colors = cb;
            button6.GetComponent<Button>().colors = cb;
            button1.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF); 
            button2.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF); 
            button3.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF); 
            button4.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF); 
            button5.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF); 
            button6.GetComponent<Image>().color = new Color32(0x16, 0x71, 0x99, 0xFF);
            if (Shop6.activeSelf)
                button6.GetComponent<Image>().color = new Color32(0x20, 0x93, 0xBC, 0xFF);
        }
        else
        {
            spawnPoint.sprite = spawnPointDefault;
            spawnPointF2.sprite = spawnPointDefault;
            MiniField[0].sprite = MiniFieldLight[0];
            button1.GetComponent<Button>().colors = colorB;
            button2.GetComponent<Button>().colors = colorB;
            button3.GetComponent<Button>().colors = colorB;
            button4.GetComponent<Button>().colors = colorB;
            button5.GetComponent<Button>().colors = colorB;
            button6.GetComponent<Button>().colors = colorB;
            button1.GetComponent<Image>().color  =Color.white;
            button2.GetComponent<Image>().color = Color.white;//new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            button3.GetComponent<Image>().color = Color.white; //new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            button4.GetComponent<Image>().color = Color.white; //new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            button5.GetComponent<Image>().color = Color.white; //new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            button6.GetComponent<Image>().color = Color.white;// new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            if (Shop6.activeSelf)
                button6.GetComponent<Image>().color = mycolor;
        }
        ProgressBarBackground.GetComponent<Image>().color = ChangeColor(ProgressBarBackground.GetComponent<Image>().color);
       
      
        Shop1.GetComponent<Image>().color=ChangeColor(Shop1.GetComponent<Image>().color, "Shop");
        Shop2.GetComponent<Image>().color=ChangeColor(Shop2.GetComponent<Image>().color, "Shop");
        Shop3.GetComponent<Image>().color=ChangeColor(Shop3.GetComponent<Image>().color, "Shop");
        Shop4.GetComponent<Image>().color=ChangeColor(Shop4.GetComponent<Image>().color, "Shop");
        Shop5.GetComponent<Image>().color= ChangeColor(Shop5.GetComponent<Image>().color, "Shop");
        Shop6.GetComponent<Image>().color= ChangeColor(Shop6.GetComponent<Image>().color, "Shop");
        spawnPointLine.GetComponent<SpriteRenderer>().color = ChangeColor(plaingField.GetComponent<SpriteRenderer>().color, "pointline");
        spawnPointLineF2.GetComponent<SpriteRenderer>().color = ChangeColor(spawnPointLineF2.GetComponent<SpriteRenderer>().color, "pointline");
        MainCamera.backgroundColor = ChangeColor(MainCamera.backgroundColor);
        Splitters.GetComponent<Image>().color = ChangeColor(Splitters.GetComponent<Image>().color, "808080");
        plaingField.GetComponent<SpriteRenderer>().color= ChangeColor(plaingField.GetComponent<SpriteRenderer>().color);
        Fm.FieldsAll[1].GetComponent<SpriteRenderer>().color= ChangeColor(Fm.FieldsAll[1].GetComponent<SpriteRenderer>().color);
        Phone.GetComponent<SpriteRenderer>().color = ChangeColor(Phone.GetComponent<SpriteRenderer>().color);
        PhoneF2.GetComponent<SpriteRenderer>().color = ChangeColor(PhoneF2.GetComponent<SpriteRenderer>().color);
        for (int h = 0; h < 6; h++)
        {
            if (h == 0 && choosenBall != 0)
                continue;
            Balls[h].GetComponent<SpriteRenderer>().color = ChangeColor(Balls[h].GetComponent<SpriteRenderer>().color);
            BallsF2[h].GetComponent<SpriteRenderer>().color = ChangeColor(BallsF2[h].GetComponent<SpriteRenderer>().color);
        }
        for(int h = 6; h < BallsF2.Length; h++)
        {
            BallsF2[h].GetComponent<Image>().color = ChangeColor(BallsF2[h].GetComponent<Image>().color);
        }
        for(int h =0; h<2;h++)
            Stoppers[h].GetComponent<SpriteRenderer>().color = ChangeColor(Stoppers[h].GetComponent<SpriteRenderer>().color);
        for(int h=0; h < AllPanel.Length; h++)
        {
            AllPanel[h].GetComponent<Image>().color = ChangeColor(AllPanel[h].GetComponent<Image>().color, "Panel");
        }
        for (int h = 0; h <BottonSkin.Length; h++)
        {
            BottonSkin[h].GetComponent<Image>().color = ChangeColor(BottonSkin[h].GetComponent<Image>().color, "AnotherPanel");
        }
        for (int h = 0; h < AllText.Length; h++)
        {
            AllText[h].color = ChangeColor(AllText[h].color);
        }
        for (int h = 0; h < Lock.Length; h++)
        {
            Lock[h].GetComponent<Image>().color = ChangeColor(Lock[h].GetComponent<Image>().color, "Lock");
        }
        for (int h = 0; h < PanelQuest.Length; h++)
        {
            PanelQuest[h].GetComponent<Image>().color = ChangeColor(PanelQuest[h].GetComponent<Image>().color, "8080");
        }
        for (int h = 0; h < PanelBackGround.Length; h++)
        {
            if(h<10)
            PanelBackGround[h].GetComponent<Image>().color = ChangeColor(PanelBackGround[h].GetComponent<Image>().color, "Background");
           else
                PanelBackGround[h].GetComponent<Image>().color = ChangeColor(PanelBackGround[h].GetComponent<Image>().color, "white");
        }
        for (int h = 0; h < Handlles.Length; h++)
        {
            Handlles[h].GetComponent<Image>().color = ChangeColor(Handlles[h].GetComponent<Image>().color, "808080");
        }
        for (int h = 0; h < Arrows.Length; h++)
        {
            Arrows[h].GetComponent<Image>().color = ChangeColor(Arrows[h].GetComponent<Image>().color);
        }
    }
    ColorBlock colorB;
    public Color32 ChangeColor(Color gr, string tag = "")
    {
        if (DarkTheme)
        {
            if(tag == "Panel")
            {
                return new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else if (tag == "Shop")
            {
                return new Color32(0x20, 0x93, 0xBC, 0xFF);
            }
            else if(tag == "8080")
            {
                return new Color32(0x16, 0x71, 0x99, 0xFF);
            }
            
            if (gr == Color.black)
                return Color.white;
            else if (gr == new Color32(0x80, 0x80, 0x80, 0xFF) || gr == new Color32(0xCC, 0xCC, 0xCC, 0xFF) || gr == Color.white|| gr == new Color32(0x99, 0x99, 0x99, 0xFF))
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
            if (tag == "Panel")
                return new Color32(0xB3, 0xB3, 0xB3, 0xFF);
            else if (tag == "Shop")
                return new Color32(0xB3, 0xB3, 0xB3, 0xFF);
            else if (tag == "Lock")
                return new Color32(0x4D, 0x4D, 0x4D, 0xFF);
            else if (tag == "Background")
                return new Color32(0x99, 0x99, 0x99, 0xFF);
            else if (tag == "8080")
                return new Color32(0x80, 0x80, 0x80, 0xFF);
            else if (tag == "white")
                return Color.white;
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
                    return new Color32(0xE6, 0xE6, 0xE6, 0xFF);
            }
            else
                return Color.white;
        }
    }
    
    public void UnlockSecondBall()
    {
        PanelTriggerBalls[1].SetActive(true);
        Lock[12].SetActive(false);
    }
    public void UnlockThirdBall()
    {
        PanelTriggerBalls[2].SetActive(true);
        Lock[11].SetActive(false);
    }
    public void UnlockFoughtBall()
    {
        PanelTriggerBalls[3].SetActive(true);
        Lock[10].SetActive(false);
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


    public void LvlUpGems()
    {
        if (lvl >= lvlNeeded)
        {
            ButtonRelive.SetActive(true);
            LevelNeedToReliveText.SetActive(false);
        }
        else
        {
            ButtonRelive.SetActive(false);
            LevelNeedToReliveText.SetActive(true) ;
        }
        ReliveText.text = $"Reset all progress and get {lvl} gems";
        GemGet.text = $"{lvl} ";
    }
}

