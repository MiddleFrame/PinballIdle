using System;
using System.Globalization;
using System.Linq;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class FreeShopController : MonoBehaviour
{
    public static DateTime LastReceiveDiamond
    {
        get => DateTime.Parse(
            PlayerPrefs.GetString("LastReceiveDiamond", DateTime.MinValue.ToString(CultureInfo.InvariantCulture)),
            CultureInfo.InvariantCulture);
        private set => PlayerPrefs.SetString("LastReceiveDiamond", value.ToString(CultureInfo.InvariantCulture));
    }

    public static DateTime LastReceiveGold
    {
        get => DateTime.Parse(
            PlayerPrefs.GetString("LastReceiveGold", DateTime.MinValue.ToString(CultureInfo.InvariantCulture)),
            CultureInfo.InvariantCulture);
        private set => PlayerPrefs.SetString("LastReceiveGold", value.ToString(CultureInfo.InvariantCulture));
    }

    [SerializeField]
    private Text _coin;

    private void OnEnable()
    {
        _coin.text = ((PlayerDataController.LevelSum +FieldManager.fields.isOpen.Sum(x=>x?1:0))* 5000).ToString();
    }

    public static void OnAdReceivedRewardDiamond()
    {
        PlayerDataController.Gems += 10;
        LastReceiveDiamond = DateTime.Now;
        FindFirstObjectByType<DonateShopController>().OnEnable();
    }

    public static void OnAdReceivedRewardCoin()
    {
        PlayerDataController.PointSum += (PlayerDataController.LevelSum+FieldManager.fields.isOpen.Sum(x=>x?1:0)) * 5000;
        LastReceiveGold = DateTime.Now;
        FindFirstObjectByType<DonateShopController>().OnEnable();
    }

    public static void BuyCoins()
    {
        if (PlayerDataController.Gems < 50) return;
        AnalyticManager.BuyCoinForDiamond();
        PlayerDataController.PointSum += 1000000;
        PlayerDataController.Gems -= 50;
    }
}