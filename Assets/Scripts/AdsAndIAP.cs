using System.Threading.Tasks;
using Managers;
using UnityEngine;
using Unity.Services.Core.Environments;
using Unity.Services.Core;

public class AdsAndIAP : MonoBehaviour
{
    public static bool isRemoveAds;

    public static AdsAndIAP instance;

    [SerializeField]
    private GameObject[] hided;

    [SerializeField]
    private DonateShopController _donateShopController;

    private void Awake()
    {
        instance = this;
    }

    private async void Start()
    {
        while (!AnalyticManager.isRemoteInit)
            await Task.Yield();
        var options = new InitializationOptions()
            .SetEnvironmentName("production");
 
        await UnityServices.InitializeAsync(options);
        IaPurchase _iAPurchase = new IaPurchase();
        _iAPurchase.IapInitialize();
        StartCoroutine(IaPurchase.CheckSubscription());
        StartCoroutine(IaPurchase.CheckX2());
        if (IaPurchase.IsIapInitialized())
            await _donateShopController.Init();
        else
        {
            InitShop();
        }
    }

    private async void InitShop()
    {
        while (!IaPurchase.IsIapInitialized())
            await Task.Delay(1000);

        await _donateShopController.Init();
    }

    public void HideAds()
    {
        foreach (var _hide in hided)
        {
            _hide.SetActive(false);
        }
    }


    public void BuyRemoveAds()
    {
        IaPurchase.BuyProductID(IaPurchase.REMOVE_ADS);
    }

    public void BuyCoffee()
    {
        IaPurchase.BuyProductID(IaPurchase.COFFEE);
    }

    public void BuyDiamond(int pack)
    {
        switch (pack)
        {
            case 0:
                IaPurchase.BuyProductID(IaPurchase.LITTLE_PACK);
                break;
            case 1:
                IaPurchase.BuyProductID(IaPurchase.MEDIUM_PACK);
                break;
            case 2:
                IaPurchase.BuyProductID(IaPurchase.BIG_PACK);
                break;
        }
    }
    public void BuyKey(int pack)
    {
        switch (pack)
        {
            case 0:
                IaPurchase.BuyProductID(IaPurchase.KEY_LITTLE_PACK);
                break;
            case 1:
                IaPurchase.BuyProductID(IaPurchase.KEY_MEDIUM_PACK);
                break;
            case 2:
                IaPurchase.BuyProductID(IaPurchase.KEY_BIG_PACK);
                break;
        }
    }
}