using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        IaPurchase _iAPurchase = new IaPurchase();
        _iAPurchase.IapInitialize();
        StartCoroutine(IaPurchase.CheckSubscription());
        StartCoroutine(IaPurchase.CheckX2());
        if (IaPurchase.IsIapInitialized())
            _donateShopController.Init();
        else
        {
            StartCoroutine(InitShop());
        }
    }

    private IEnumerator InitShop()
    {
        while (!IaPurchase.IsIapInitialized())
            yield return new WaitForSeconds(1f);

        _donateShopController.Init();
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
}