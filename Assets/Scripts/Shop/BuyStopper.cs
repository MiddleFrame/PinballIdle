using UnityEngine;
using UnityEngine.UI;

public class BuyStopper : MonoBehaviour
{
    
    public static StopperGrades grades;

    private int _costStoppers = 40000;
    private long _costBuffStoppers = 1000;
    private long _costCooldownStoppers = 1000;
    [SerializeField]
    private Image[] _buyStoppers;
    [SerializeField]
    private GameObject[] _upgradeTimeStopper;
    [SerializeField]
    private GameObject[] _upgradeCooldownStopper;
    [SerializeField]
    private GameObject[] _checkers;
    [SerializeField]
    private Text[] _costStoppersText;
    [SerializeField]
    private Text[] _buyStoppersText;
    /*[SerializeField]
    private Text[] _costBuffStoppersText;
    [SerializeField]
    private Text[] _countBuffStoppersText;
    [SerializeField]
    private Image[] _costBuffStoppersImage;
    [SerializeField]
    private Text[] _costCooldownStoppersText;
    [SerializeField]
    private Text[] _countCooldownStoppersText;
    [SerializeField]
    private Image[] _costCooldownStoppersImage;*/

    private bool _isStopperOpen=true;
   // private bool _isStopperBuffOpen=true;
   // private bool _isStopperCooldownOpen=true;
   //private bool _stopperMax = true;
   // private bool _stopperCooldownMax=true;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _buyStoppers.Length; i++)
        {
            if(grades.isStopper[i])
            buyStoppers(i);
       // stopperBuff(grades.buffTimeStoppers[i],i);
        //    stopperBuffCooldown(grades.buffCooldownStoppers[i],i);
        }
    }

    private void Update()
    {
        if (!grades.isStopper[FieldManager.currentField] && MenuController.currentMenu == 1)
        {
            if (_isStopperOpen && PlayerDataController.PointSum < _costStoppers)
            {
                _isStopperOpen = false;
                _buyStoppers[FieldManager.currentField].raycastTarget = false;
                _buyStoppers[FieldManager.currentField].sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_costStoppersText[FieldManager.currentField].gameObject);

            }
            else if (!_isStopperOpen && PlayerDataController.PointSum >= _costStoppers)
            {
                _isStopperOpen = true;
                _buyStoppers[FieldManager.currentField].sprite = GameManager.instance._unlockedSprite;
                _buyStoppers[FieldManager.currentField].raycastTarget = true;
                GameManager.instance.TextUp(_costStoppersText[FieldManager.currentField].gameObject);
            }
        }/*
        else if (grades.isStopper[FieldManager.currentField] && MenuController.currentMenu == 0)
        {
            if (!_stopperMax && _isStopperBuffOpen && PlayerDataController.PointSum < _costBuffStoppers)
            {
                _isStopperBuffOpen = false;
                _costBuffStoppersImage[FieldManager.currentField].raycastTarget = false;
                _costBuffStoppersImage[FieldManager.currentField].sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_costBuffStoppersText[FieldManager.currentField].gameObject);

            }
            else if (!_stopperMax && !_isStopperBuffOpen && PlayerDataController.PointSum >= _costBuffStoppers)
            {
                _isStopperBuffOpen = true;
                _costBuffStoppersImage[FieldManager.currentField].sprite = GameManager.instance._unlockedSprite;
                _costBuffStoppersImage[FieldManager.currentField].raycastTarget = true;
                GameManager.instance.TextUp(_costBuffStoppersText[FieldManager.currentField].gameObject);
            }   
            if (!_stopperCooldownMax && _isStopperCooldownOpen && PlayerDataController.PointSum < _costCooldownStoppers)
            {
                _isStopperCooldownOpen = false;
                _costCooldownStoppersImage[FieldManager.currentField].raycastTarget = false;
                _costCooldownStoppersImage[FieldManager.currentField].sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_costCooldownStoppersText[FieldManager.currentField].gameObject);

            }
            else if (!_stopperCooldownMax && !_isStopperCooldownOpen && PlayerDataController.PointSum >= _costCooldownStoppers)
            {
                _isStopperCooldownOpen = true;
                _costCooldownStoppersImage[FieldManager.currentField].sprite = GameManager.instance._unlockedSprite;
                _costCooldownStoppersImage[FieldManager.currentField].raycastTarget = true;
                GameManager.instance.TextUp(_costCooldownStoppersText[FieldManager.currentField].gameObject);
            }
        }*/
    }
    /*
    public void StopperBuff()
    {
        if (PlayerDataController.PointSum >= _costBuffStoppers)
        {
            if (Checker.TimeStoppers < 5)
            {
                PlayerDataController.PointSum -= _costBuffStoppers;
                Statistics.stats.pointSpent += _costBuffStoppers;
                // SumSent.text = $"Total points spent:     {PointSpent}";
                stopperBuff(1, FieldManager.currentField);
            }
        }
    }
    
    private void stopperBuff(int i,int g)
    {
        for (int j = 0; j < i; j++)
        {
            Checker.TimeStoppers += 0.75f ;
            _costBuffStoppers = (long)(_costBuffStoppers * 1.1f);
        }
        _costBuffStoppersText[g].text = GameManager.NormalSum(_costBuffStoppers);
        _countBuffStoppersText[g].text = Checker.TimeStoppers + "→" + (Checker.TimeStoppers + 0.75);
        if (Checker.TimeStoppers >= 5)
        {
            _stopperMax = true;
            GameManager.instance.TextDown(_costBuffStoppersText[g].gameObject);
            _costCooldownStoppersImage[g].raycastTarget = false;
            _costCooldownStoppersImage[g].sprite = GameManager.instance._lockedSprite ;
            _countBuffStoppersText[g].text = Checker.TimeStoppers + "";
            _costBuffStoppersText[g].text = "MAX";
        }
    }

    public void StopperBuffCooldown()
    {
        if (PlayerDataController.PointSum >= _costCooldownStoppers)
        {
            if (Checker.coldownStopper > 6)
            {
                PlayerDataController.PointSum -= _costCooldownStoppers;
                Statistics.stats.pointSpent += _costCooldownStoppers;
                stopperBuffCooldown(1, FieldManager.currentField);
           }
        }

    }

    private void stopperBuffCooldown(int i, int j)
    {
        for (int g= 0; g < i; g++)
        {
            Checker.coldownStopper -= 0.75f;
            _costCooldownStoppers = (long)(_costCooldownStoppers * 1.1f);
        }
        _costCooldownStoppersText[j].text = GameManager.NormalSum(_costCooldownStoppers);
        _countCooldownStoppersText[j].text = Checker.coldownStopper + "→" + (Checker.coldownStopper - 0.75);
        
        if (Checker.coldownStopper <= 6)
        {
            _stopperCooldownMax = true;
            GameManager.instance.TextDown(_costCooldownStoppersText[j].gameObject);
            _costCooldownStoppersImage[j].raycastTarget = false;
            _costCooldownStoppersImage[j].sprite = GameManager.instance._lockedSprite;
            _countCooldownStoppersText[j].text = Checker.coldownStopper + "";
            _costCooldownStoppersText[j].text = "MAX";
        }
    }*/

    public void BuyStoppers()
    {
        if (PlayerDataController.PointSum >= _costStoppers)
        {
            PlayerDataController.PointSum -= _costStoppers;
            Statistics.stats.pointSpent += _costStoppers;
            
            buyStoppers(FieldManager.currentField);
        }
    }

    private void buyStoppers(int i)
    {
        _buyStoppersText[i].text = "Stoppers";
        _buyStoppers[i].gameObject.SetActive(false);
        _upgradeTimeStopper[i].SetActive(true);
        _upgradeCooldownStopper[i].SetActive(true);
        _checkers[i].SetActive(true);
    }
}


public class StopperGrades
{
    public bool[] isStopper;
   // public int[] buffTimeStoppers;
   // public int[] buffCooldownStoppers;
    public StopperGrades(int length)
    {
        isStopper = new bool[length];
       // buffTimeStoppers = new int[length];
        //buffCooldownStoppers = new int[length];
    }
}