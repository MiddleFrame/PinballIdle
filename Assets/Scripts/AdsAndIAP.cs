using UnityEngine;

public class AdsAndIAP : MonoBehaviour
{
    public static bool isRemoveAds;

    public static AdsAndIAP instance;

    [SerializeField]
    private GameObject[] hided;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        IaPurchase _iAPurchase = new IaPurchase();
        _iAPurchase.IapInitialize();
        StartCoroutine(IaPurchase.CheckSubscription());
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
        IaPurchase.BuyProductID(IaPurchase.RemoveAds);
    }
    
    public void BuyCoffee()
    {
        IaPurchase.BuyProductID(IaPurchase.Coffee);
    }

    public void BuyDiamond(int pack)
    {
        switch (pack)
        {
            case 0:
                IaPurchase.BuyProductID(IaPurchase.LittlePack);
                break;
            case 1:
                IaPurchase.BuyProductID(IaPurchase.MediumPack);
                break;
            case 2:
                IaPurchase.BuyProductID(IaPurchase.BigPack);
                break;
        }
    }
}