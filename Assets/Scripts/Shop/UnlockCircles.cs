using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockCircles : MonoBehaviour
{
    public int field=0;
    public GameObject[] circles;
    private bool _isOpen = true;
    private bool _isMax = false;
    public UpgradeCircle upgrade;
    [SerializeField]
    private Text _cost;  
    [SerializeField]
    private Text _count;
    [SerializeField]
    private Image _imageCost;
    [SerializeField]
    private int _startCost;
    [SerializeField]
    private int _multiCost;
    [SerializeField]
    private Sprite _maxSprite;

    private void Start()
    {
       
        for(int i=0; i < upgrade.Upgrades; i++)
        {
            OpenCircle(i);
        }
        UpdateText();
    }

    private void Update()
    {
        if (MenuController.currentMenu == 1)
        {
            if (_isMax)
                return;
            if (_isOpen && PlayerDataController.PointSum < cost)
            {
                _isOpen = false;
                _imageCost.raycastTarget = false;
                _imageCost.sprite = GameManager.instance._lockedSprite;
                GameManager.instance.TextDown(_cost.gameObject);

            }
            else if (!_isOpen && PlayerDataController.PointSum >= cost)
            {
                _isOpen = true;
                _imageCost.sprite = GameManager.instance._unlockedSprite;
                _imageCost.raycastTarget = true;
                GameManager.instance.TextUp(_cost.gameObject);
            }
        }
    }

    private void OpenCircle(int i)
    {
        if(i<circles.Length)
        circles[i].SetActive(true);
    }
    int cost = 10;
  
    public void BuyCircle()
    {
        cost = _startCost + _multiCost * upgrade.Upgrades;
        if (PlayerDataController.PointSum >= cost)
        {
            PlayerDataController.PointSum -= cost;
            Statistics.stats.pointSpent += cost;
            
            OpenCircle(upgrade.Upgrades);
            upgrade.Upgrades++;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        if (_isMax)
            return;
        if(upgrade.Upgrades == upgrade.MaxUpgrade)
        {
            _isMax = true;
           GameManager.instance.TextDown(_cost.gameObject);
            _cost.text = "MAX";
            _imageCost.sprite = _maxSprite;
            _count.text = (upgrade.MaxUpgrade+1).ToString();
            _imageCost.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            cost = _startCost + _multiCost * upgrade.Upgrades;
            _cost.text = (cost).ToString();
            _count.text = (upgrade.Upgrades+1).ToString() + "→" + (upgrade.Upgrades+2).ToString();
        }
    }
}



public class UpgradeCircle
{
    public int MaxUpgrade;
    public int Upgrades = 0;
    public UpgradeCircle(int MaxCircle) { this.MaxUpgrade = MaxCircle; }
}
