using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsAndIAP : MonoBehaviour
{
    public static bool isRemoveADS;
    
    public static AdsAndIAP instance;

    [SerializeField]
    GameObject[] hided;

    private void Awake()
    {
        instance = this;
       
    }

    private void Start()
    {
        IAPurchase iAPurchase = new IAPurchase();
        iAPurchase.IapInitializate();
        StartCoroutine(IAPurchase.CheckSubscription());
    }

    public void HideAds()
    {
        foreach(var hide in hided)
        {
            hide.SetActive(false);
        }
    }


    public void BuyRemoveADS()
    {
        IAPurchase.BuyProductID(IAPurchase._removeADS);
    }
}
