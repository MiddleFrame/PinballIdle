using System;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rating : MonoBehaviour
{
    [SerializeField]
    private GameObject _dots;

    [SerializeField]
    private GameObject _rules;

    [SerializeField]
    private GameObject _rating;

    [SerializeField]
    private GameObject _winPanel;

    [SerializeField]
    private Text[] _nicknames;

    [SerializeField]
    private Text _point;

    [SerializeField]
    private Text _placeText;

    [SerializeField]
    private Text _prise;

    [SerializeField]
    private Text[] _points;

    [SerializeField]
    private Text _cost;

    [SerializeField]
    private GameObject _bottomPanel;

    private static int _dayOfWeek;

    [SerializeField]
    private Image _nextButton;

    private static int LastDayOfWeek
    {
        get => PlayerPrefs.GetInt("LastDayOfWeek", -1);
        set => PlayerPrefs.SetInt("LastDayOfWeek", value);
    }

    private readonly string[] _nicknamesList = new[]
    {
        "DarK_Knigt", "Lemonk", "Crtal", "lou_Tep", "MrZadrot", "_LegenDa_", "Cemea", "Zigzag", "SmoKKeR",
        "DIVERSANT_", "K_I_N_G", "SkILLzAr", "MrGameFun", "leha2019",
        "Enigma", "Black_Wolf", "Good_Joker", "ReversAlpha", "GADZILO", "Cherry_Pie", "ByRaShKa", "Vito_Scaletta",
        "MaxTheGamer", "DELIRIOUSPLAY", "NyanCat", "RedHulk", "FireKitty", "Klaimmor", "EzzzBOX",
        "GhOsTiK", "MonAmour", "SkyDeaD", "DeMpDeeZ", "Enroxes", "SpeedBeast", "Howfaralice", "FlooMeer",
        "Mist(er)ror", "HouruSinkara", "BaRsIk", "RaVeNCroW", "DantASS", "Artcross", "DarkWolfMaster",
        "BallisticAmmo", "SkOrPiOnUs",
        "JopaTekilla", "NoNameForever", "EziosTaNKprrak", "DEAD__Е_Y_E", "DEADShotGunDEAD", "Mr.Bean", "Fox121",
        "lena6", "dontcry", "253255", "dady.cat", "565jsadfhhfd", "NoName", "Alex234", "NoGameNoLife", "Killer333"
    };

    public static int points
    {
        get => PlayerPrefs.GetInt("PointCompetitive", 0);
        set => PlayerPrefs.SetInt("PointCompetitive", value);
    }

    private static int tries
    {
        get => PlayerPrefs.GetInt("Try", 0);
        set => PlayerPrefs.SetInt("Try", value);
    }

    private static List<int> playerPoints;

    private static string[] _weeksPlayer;

    private static string[] weeksPlayer
    {
        get => PlayerPrefs.GetString("weeksPlayer").Split(' ');
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            string _nicks = "";
            _nicks += _weeksPlayer[0] + " ";
            _nicks += _weeksPlayer[1] + " ";
            _nicks += _weeksPlayer[2] + " ";
            _nicks += _weeksPlayer[3] + " ";
            PlayerPrefs.SetString("weeksPlayer", _nicks);
        }
    }

    private void Awake()
    {
        MenuController.openMenu[MenuController.Shops.Versus] += UpdateRating;
        MenuController.openMenu[MenuController.Shops.Versus] += () =>
        {
            if (PlayerDataController.PointSum < tries * 1000000)
            {
                _nextButton.sprite = GameManager.instance._lockedSprite;
                _nextButton.raycastTarget = false;
            }
            else
            {
                _nextButton.sprite = GameManager.instance._unlockedSprite;
                _nextButton.raycastTarget = true;
            }
        };
        MenuController.openMenu[MenuController.Shops.Versus] += () =>
        {
            _cost.text = tries > 0 ? tries + " million" : "free";
        };
    }

    private void Start()
    {
        _dayOfWeek = (int) DateTime.Now.DayOfWeek;
        if (LastDayOfWeek != _dayOfWeek)
        {
            if (LastDayOfWeek == -1 || LastDayOfWeek > _dayOfWeek)
            {
                if (LastDayOfWeek > _dayOfWeek)
                {
                    OpenWinPanel(points / 3);
                }

                SetNewPlayers();
            }
            else
            {
                _weeksPlayer = weeksPlayer;
            }

            LastDayOfWeek = _dayOfWeek;
        }
        else
        {
            SetOldPlayers();
        }

        SetNewPoint();
    }


    private void SetNewPlayers()
    {
        _weeksPlayer = new string[4];
        for (int _i = 0; _i < 4; _i++)
        {
            _weeksPlayer[_i] = _nicknamesList[Random.Range(0, _nicknamesList.Length)];
        }

        weeksPlayer = null;
    } private void SetOldPlayers()
    {
        _weeksPlayer = new string[4];
        for (int _i = 0; _i < 4; _i++)
        {
            _weeksPlayer[_i] = weeksPlayer[_i];
        }
    }

    private void SetNewPoint()
    {
        playerPoints = new List<int>(new int[5])
        {
            [0] = points
        };
        for (int _i = 1; _i < 5; _i++)
        {
            playerPoints[_i] =2 * _dayOfWeek+_i;
        }
    }

    private void UpdateRating()
    {
        playerPoints[0] = points;
        var _sortList = new List<int>(playerPoints);
        _sortList.Sort();
        _sortList.Reverse();
        for (int _i = 0; _i < 5; _i++)
        {
            var _a = playerPoints
                .IndexOf(playerPoints.Find(x => x == _sortList[_i]));
            if (_a == 0)
            {
                _dots.SetActive(_i == 4);
            }

            _nicknames[_i].text = _a == 0 ? "You" : _weeksPlayer[_a - 1];
            _points[_i].text = _sortList[_i].ToString();
        }
    }

    private int _place;

    private void OpenWinPanel(int point)
    {
        tries = 0;
        _winPanel.SetActive(true);
        _point.text = point.ToString();
        _place = point switch
        {
            0 => 6,
            -1 => 6,
            -2 => 6,
            4 => 2,
            3 => 3,
            2 => 4,
            1 => 5,
            _ => 1
        };
        _placeText.text = _place.ToString();
        _prise.text = _place switch
        {
            1 => "500",
            2 => "300",
            3 => "200",
            _ => "0"
        };
    }

    public void GetReward()
    {
        PlayerDataController.Gems += _place switch
        {
            1 => 500,
            2 => 300,
            3 => 200,
            _ => 0
        };
        points = 0;
        SetNewPoint();
        UpdateRating();
    }

    public void NextWindow()
    {
        if (PlayerDataController.PointSum < tries * 1000000) return;
        PlayerDataController.PointSum -= tries * 1000000;
        tries++;
        _bottomPanel.SetActive(false);
        _rating.SetActive(false);
        _rules.SetActive(true);
    }
}