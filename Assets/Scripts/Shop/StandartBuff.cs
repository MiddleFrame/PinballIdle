using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandartBuff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text[] _standartCost;
    [SerializeField]
    private Text[] _standartCostAutoflipper;
    [SerializeField]
    private Text[] _pointBuffText;
    private bool _isOpen = true;
    private bool _isOpenAutoFlipper = true;
    public static CostAndGrade grade;
    private int[] costOnGrade = new int[] { 100 };
    private int[] costAutoflipper = new int[] { 10000 };
    [SerializeField]
    private Text[] _autoflipperText;
    [SerializeField]
    private Image[] _buttonImage;
    [SerializeField]
    private Image[] _buttonAutoflipperImage;
    [SerializeField]
    private GameObject[] _automodSlider;
    public static bool[] automod;
    private void Start()
    {
        automod = new bool[grade.autoflippers.Length];
        for (int i = 0; i < grade.autoflippers.Length; i++)
        {
            StandartBuffHit(i, grade.pointOnBit[i] - 1);
            if (grade.autoflippers[i])
            {
                OpenAutomod(i);
            }
        }
    }

    private void Update()
    {
        if (MenuController.currentMenu == 0) {
            if (_isOpen && PlayerDataController.PointSum < costOnGrade[FieldManager.currentField])
            {
                _isOpen = false;
                _buttonImage[FieldManager.currentField].raycastTarget = false;
                _buttonImage[FieldManager.currentField].sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_standartCost[FieldManager.currentField].gameObject);

            }
            else if (!_isOpen && PlayerDataController.PointSum >= costOnGrade[FieldManager.currentField])
            {
                _isOpen = true;
                _buttonImage[FieldManager.currentField].sprite = GameManager.instance._unlockedSprite;
                _buttonImage[FieldManager.currentField].raycastTarget = true;
                GameManager.instance.TextUp(_standartCost[FieldManager.currentField].gameObject);

            } 
        }
        if (MenuController.currentMenu == 1) {
            if (_isOpenAutoFlipper && !grade.autoflippers[FieldManager.currentField] && PlayerDataController.PointSum < costAutoflipper[FieldManager.currentField])
            {
                _isOpenAutoFlipper = false;

                _buttonAutoflipperImage[FieldManager.currentField].raycastTarget = false;
                _buttonAutoflipperImage[FieldManager.currentField].sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_standartCostAutoflipper[FieldManager.currentField].gameObject);

            }
            else if (!_isOpenAutoFlipper && !grade.autoflippers[FieldManager.currentField] && PlayerDataController.PointSum >= costAutoflipper[FieldManager.currentField])
            {
                _isOpenAutoFlipper = true;

                _buttonAutoflipperImage[FieldManager.currentField].sprite = GameManager.instance._unlockedSprite;
                _buttonAutoflipperImage[FieldManager.currentField].raycastTarget = true;
                GameManager.instance.TextUp(_standartCostAutoflipper[FieldManager.currentField].gameObject);

            } 
        }
    }

    private void StandartBuffHit(int field = 0, int countGrades = 1)
    {
        for (int i = 0; i < countGrades; i++)
        {
            costOnGrade[field] = (int)(costOnGrade[field] * 1.1f);
        }
        _standartCost[field].text = GameManager.NormalSum(costOnGrade[field]);
        _pointBuffText[field].text = grade.pointOnBit[field] + "→" + (grade.pointOnBit[field] + 1);
    }


    public void BuyStandartBuffHit()
    {
        if (PlayerDataController.PointSum >= costOnGrade[FieldManager.currentField])
        {
            PlayerDataController.PointSum -= costOnGrade[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += costOnGrade[FieldManager.currentField];

            grade.pointOnBit[FieldManager.currentField] += 1;
            StandartBuffHit(FieldManager.currentField, 1);
        }
    }


    public void BuyAutomod()
    {
        if (PlayerDataController.PointSum >= costAutoflipper[FieldManager.currentField])
        {
            PlayerDataController.PointSum -= costAutoflipper[FieldManager.currentField];
            Statistics.stats.pointSpent += costAutoflipper[FieldManager.currentField];
            grade.autoflippers[FieldManager.currentField] = true;
            OpenAutomod(FieldManager.currentField);
        }
    }

    private void OpenAutomod(int i)
    {

        _autoflipperText[i].text = "Auto-flippers";
        _automodSlider[i].SetActive(true);
        _buttonAutoflipperImage[i].gameObject.SetActive(false);

    }




    public void Automod(int i)
    {
        automod[i] = !automod[i];

        if (automod[i])
        {
            _automodSlider[i].transform.localScale = new Vector3(1, _automodSlider[i].transform.localScale.y, _automodSlider[i].transform.localScale.z);
            _automodSlider[i].GetComponent<Image>().color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
        }
        else
        {
            _automodSlider[i].transform.localScale = new Vector3(-1, _automodSlider[i].transform.localScale.y, _automodSlider[i].transform.localScale.z);
            _automodSlider[i].GetComponent<Image>().color = Color.red;
        }
    }
}

[Serializable]
public class CostAndGrade
{
    public int[] pointOnBit;
    public bool[] autoflippers;
    public CostAndGrade()
    {
        pointOnBit = new int[] { 1 };
        autoflippers = new bool[] { false };
    }
}
