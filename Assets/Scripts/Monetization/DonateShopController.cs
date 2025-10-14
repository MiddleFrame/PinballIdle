using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject _notInitError;
    // Start is called before the first frame update

    private async void Start()
    {
        await Init();
    }

    private async Task Init()
    { 
        _notInitError.SetActive(true);
        while (!InAppPurchase.IsIapInitialized())
        {
            await Task.Delay(100);
        }
        _notInitError.SetActive(false);
        _diamondPacks[0].text = InAppPurchase.StoreController.GetProductById(InAppPurchase.LITTLE_PACK)
            ?.metadata.localizedPriceString;
        _fakePrice.text = (InAppPurchase.StoreController.GetProductById(InAppPurchase.MEDIUM_PACK)
            ?.metadata.localizedPrice * 2).ToString();
        _diamondPacks[1].text = InAppPurchase.StoreController.GetProductById(InAppPurchase.MEDIUM_PACK)
            ?.metadata.localizedPriceString;
        _diamondPacks[2].text = InAppPurchase.StoreController.GetProductById(InAppPurchase.BIG_PACK)
            ?.metadata.localizedPriceString;
        _coffeeCost.text = InAppPurchase.StoreController.GetProductById(InAppPurchase.COFFEE)
            ?.metadata.localizedPriceString;
        _removeAds.text = InAppPurchase.StoreController.GetProductById(InAppPurchase.REMOVE_ADS)
            ?.metadata.localizedPriceString;
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

        _rewardDiamond.SetActive(FreeShopController.LastReceiveDiamond.AddHours(12) < DateTime.Now);
        _rewardGold.SetActive(FreeShopController.LastReceiveGold.AddHours(12) < DateTime.Now);
        _timerDiamond = StartCoroutine(Timer(_rewardDiamondTimer, FreeShopController.LastReceiveDiamond.AddHours(12) - DateTime.Now));
        _timerGold =StartCoroutine(Timer(_rewardGoldTimer, FreeShopController.LastReceiveGold.AddHours(12) - DateTime.Now));
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