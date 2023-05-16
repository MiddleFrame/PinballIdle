using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System.Windows;

public class DonateShopController : MonoBehaviour
{
    [SerializeField]
    private Text[] _diamondPacks;

    [SerializeField]
    private Text _coffeeCost;

    [SerializeField]
    private Text _fakePrice;

    [SerializeField]
    private Text _removeAds;

    [SerializeField]
    private GameObject _rewardDiamond;
    [SerializeField]
    private GameObject _rewardGold;
    [SerializeField]
    private Text _rewardDiamondTimer;
    [SerializeField]
    private Text _rewardGoldTimer;
    // Start is called before the first frame update
    public async Task Init()
    { 
        while (!IaPurchase.IsIapInitialized())
        {
            await Task.Delay(100);
        }
        _diamondPacks[0].text = IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.LITTLE_PACK)
            .metadata.localizedPriceString;
        _fakePrice.text = (IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.MEDIUM_PACK).metadata
            .localizedPrice * 2).ToString();
        _diamondPacks[1].text = IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.MEDIUM_PACK)
            .metadata.localizedPriceString;
        _diamondPacks[2].text = IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.BIG_PACK).metadata
            .localizedPriceString;
        _coffeeCost.text = IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.COFFEE).metadata
            .localizedPriceString;
        _removeAds.text = IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.REMOVE_ADS).metadata
            .localizedPriceString;
    }

    private Coroutine _timerDiamond;
    private Coroutine _timerGold;
    public void OnEnable()
    {
        if (_timerGold != null)
        {
            StopCoroutine(_timerGold);
            _timerGold = null;
        }

        if (_timerDiamond != null)
        {
            StopCoroutine(_timerDiamond);
            _timerDiamond = null;
        }

        _rewardDiamond.SetActive(RewardDiamonds.LastReceiveDiamond.AddHours(12) < DateTime.Now);
        _rewardGold.SetActive(RewardDiamonds.LastReceiveGold.AddHours(12) < DateTime.Now);
        _timerDiamond = StartCoroutine(Timer(_rewardDiamondTimer, RewardDiamonds.LastReceiveDiamond.AddHours(12) - DateTime.Now));
        _timerGold =StartCoroutine(Timer(_rewardGoldTimer, RewardDiamonds.LastReceiveGold.AddHours(12) - DateTime.Now));
    }

    private IEnumerator Timer(Text timer, TimeSpan time)
    {
        while (true)
        {
            time = time.Add(new TimeSpan(0,0,-1));
            timer.text = time.ToString(@"hh\:mm\:ss");
            yield return new WaitForSeconds(1f);
        }
    }
}