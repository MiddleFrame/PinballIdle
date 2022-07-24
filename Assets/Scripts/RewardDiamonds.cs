using System;
using System.Globalization;
using Controllers;
using Managers;
using UnityEngine;

public class RewardDiamonds : MonoBehaviour
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

    public static void OnAdReceivedRewardDiamond()
    {
        PlayerDataController.Gems += 10;
        LastReceiveDiamond = DateTime.Now;
        FindObjectOfType<DonateShopController>().OnEnable();
    }

    public static void OnAdReceivedRewardCoin()
    {
        PlayerDataController.PointSum += 100000;
        LastReceiveGold = DateTime.Now;
        FindObjectOfType<DonateShopController>().OnEnable();
    }

    public static void BuyCoins()
    {
        if (PlayerDataController.Gems < 50) return;
        AnalyticManager.BuyCoinForDiamond();
        PlayerDataController.PointSum += 1000000;
        PlayerDataController.Gems -= 50;
    }
}