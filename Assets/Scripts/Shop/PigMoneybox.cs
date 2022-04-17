using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;
public class PigMoneybox : MonoBehaviour
{
    public static AfkGrade grades;

    [SerializeField]
    private Text _standartCost;
    [SerializeField]
    private Text _pointBuffText;
    [SerializeField]
    private Image _buttonImage;
    private bool _isOpen = true;
    private int costOnGrade = 100;
    [SerializeField]
    private GameObject _signal;

    private int points = 0;
    private int _maxPoints = 1000;
    public static DateTime nextClaim
    {
        get { return DateTime.Parse(PlayerPrefs.GetString("NextClaim", DateTime.MinValue.ToString())); }
        set { PlayerPrefs.SetString("NextClaim", value.ToString()); }
    }
    [Header("Panel moneybox")]
    [SerializeField]
    private GameObject _moneyboxPanel;
    [SerializeField]
    private Text _buffText;
    [SerializeField]
    private Text _maxText;
    [SerializeField]
    private Text _getText;
    [SerializeField]
    private Text _getx2Text;

    private void Start()
    {
        if (nextClaim != DateTime.MinValue)
        {
            if (nextClaim < DateTime.Now)
            {
                points = _maxPoints;
                openMoneybox();
            }
            else
            {
                points = (int)(_maxPoints - (nextClaim - DateTime.Now).TotalMinutes * (grades.countGrades + 1)*10);
            }
        }
        else
        {
            nextClaim = DateTime.Now.AddMinutes((_maxPoints - points) / ((grades.countGrades + 1) * 10));
        }
        StandartBuffHit(grades.countGrades);
    }
    private void Update()
    {
        if (MenuController.currentMenu == 0)
        {
            if (_isOpen && PlayerDataController.PointSum < costOnGrade)
            {
                _isOpen = false;
                _buttonImage.raycastTarget = false;
                _buttonImage.sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_standartCost.gameObject);

            }
            else if (!_isOpen && PlayerDataController.PointSum >= costOnGrade)
            {
                _isOpen = true;
                _buttonImage.sprite = GameManager.instance._unlockedSprite;
                _buttonImage.raycastTarget = true;
                GameManager.instance.TextUp(_standartCost.gameObject);

            }
        }
    }

    IEnumerator checkSignal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(_maxPoints==points && !_signal.activeSelf)
            {
                _signal.SetActive(true);
            }
            else if(_maxPoints != points && _signal.activeSelf)
            {
                _signal.SetActive(false);
            }
        }
    }

    private void StandartBuffHit(int countGrades = 1)
    {
        for (int i = 0; i < countGrades; i++)
        {
            costOnGrade = (int)(costOnGrade * 1.1f);
        }
        _standartCost.text = GameManager.NormalSum(costOnGrade);
        _pointBuffText.text = ((grades.countGrades + 1) * 10).ToString() + "→" + (grades.countGrades + 2) * 10;
    }


    public void BuyStandartBuffHit()
    {
        if (PlayerDataController.PointSum >= costOnGrade)
        {
            PlayerDataController.PointSum -= costOnGrade;
            Statistics.stats.pointSpent += costOnGrade;
            grades.countGrades += 1;
            StandartBuffHit(1);
            nextClaim = DateTime.Now.AddMinutes((_maxPoints-points) / ((grades.countGrades + 1) * 10));
        }
    }

    public void OpenMoneybox()
    {
        countPoints();
        openMoneybox();
    }
    private void openMoneybox()
    {
        Debug.Log("Total minutes: "+ (nextClaim - DateTime.Now).TotalMinutes);
        _moneyboxPanel.SetActive(true);/*
        _buffText.text = ((grades.countGrades + 1) * 10).ToString();
        _maxText.text = _maxPoints.ToString();*/
        _getText.text = points.ToString();
        _getx2Text.text = (3 * points).ToString();
        _buffText.text = $"You get: {(grades.countGrades+1)*10} coin every min";
        _maxText.text = $"Maximum: {_maxPoints} coins";
    }

    private void countPoints()
    {
        points = (int)(_maxPoints - Math.Ceiling((nextClaim - DateTime.Now).TotalMinutes * (grades.countGrades + 1) * 10));
    }

    public void Claim()
    {
        _moneyboxPanel.SetActive(false);
       
        nextClaim = DateTime.Now.AddMinutes((float)_maxPoints / ((grades.countGrades + 1) * 10));
        PlayerDataController.PointSum += points;
        points = 0;

    }
    public void Claimx3()
    {
        _moneyboxPanel.SetActive(false);
        InterstitialAndReward.timeOutReward = InterstitialAndReward.timeOutRewardMax;
         nextClaim = DateTime.Now.AddMinutes((float)_maxPoints / ((grades.countGrades + 1) * 10));
        PlayerDataController.PointSum += 3*points;
        points = 0;

    }

}

public class AfkGrade
{
    public int countGrades = 0;
}
