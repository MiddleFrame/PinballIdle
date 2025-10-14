using System.Threading.Tasks;
using Controllers;
using Managers;
using UnityEngine;
using Unity.Services.Core.Environments;
using Unity.Services.Core;
using Zenject;

public class AdsAndIAP : MonoBehaviour
{
    public static bool isRemoveAds;

    public static AdsAndIAP instance;

    [SerializeField]
    private GameObject[] hided;

    [SerializeField]
    private DonateShopController _donateShopController;

    [Inject]
    private PopupsController _popupsController;
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
        InAppPurchase _iAPurchase = new InAppPurchase();
        await _iAPurchase.IapInitialize();
        _popupsController.RegisterActionToOpenPopup<ShopPanel>(
            ()=>GameManager.instance.StartCoroutine( SkinShopController.instance.InitSkins()));
    }

  

    public void HideAds()
    {
        foreach (var _hide in hided)
        {
            _hide.SetActive(false);
        }
    }


    public static void BuyRemoveAds()
    {
        InAppPurchase.BuyProductID(InAppPurchase.REMOVE_ADS);
    }

    public static void BuyCoffee()
    {
        InAppPurchase.BuyProductID(InAppPurchase.COFFEE);
    }

    public void BuyDiamond(int pack)
    {
        switch (pack)
        {
            case 0:
                InAppPurchase.BuyProductID(InAppPurchase.LITTLE_PACK);
                break;
            case 1:
                InAppPurchase.BuyProductID(InAppPurchase.MEDIUM_PACK);
                break;
            case 2:
                InAppPurchase.BuyProductID(InAppPurchase.BIG_PACK);
                break;
        }
    }
    public void BuyKey(int pack)
    {
        switch (pack)
        {
            case 0:
                InAppPurchase.BuyProductID(InAppPurchase.KEY_LITTLE_PACK);
                break;
            case 1:
                InAppPurchase.BuyProductID(InAppPurchase.KEY_MEDIUM_PACK);
                break;
            case 2:
                InAppPurchase.BuyProductID(InAppPurchase.KEY_BIG_PACK);
                break;
        }
    }
}