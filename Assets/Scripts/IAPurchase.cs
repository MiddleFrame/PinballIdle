using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.Purchasing;

public class IaPurchase : IStoreListener
{
    public const string REMOVE_ADS = "remove_ads";
    public const string COFFEE = "coffee";
    public const string BIG_PACK = "get_big_pack";
    public const string LITTLE_PACK = "get_little_pack_daimonds";
    public const string MEDIUM_PACK = "pack_medium_diamond";
    public static IStoreController _storeController;
    private static IExtensionProvider _storeExtensionProvider;
    public void IapInitialize()
    {
        if (IsIapInitialized())
            return;
        var _builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        _builder.AddProduct(REMOVE_ADS, ProductType.NonConsumable);
        _builder.AddProduct(BIG_PACK, ProductType.Consumable);
        _builder.AddProduct(LITTLE_PACK, ProductType.Consumable);
        _builder.AddProduct(MEDIUM_PACK, ProductType.Consumable);
        _builder.AddProduct(COFFEE, ProductType.Consumable);
        UnityPurchasing.Initialize(this, _builder);
    }

    public static bool IsIapInitialized()
    {
        return _storeController != null && _storeExtensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
    }

    public static IEnumerator CheckSubscription()
    {
        while (!IsIapInitialized())
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (_storeController?.products == null) yield break;
        AdsAndIAP.isRemoveAds = false;
        if (_storeController.products.WithID(REMOVE_ADS).hasReceipt)
        {
            AdsAndIAP.isRemoveAds = true;
            AdsAndIAP.instance.HideAds();
        }

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        switch (purchaseEvent.purchasedProduct.definition.id)
        {
            case REMOVE_ADS:
                
                AdsAndIAP.isRemoveAds = true;
                AdsAndIAP.instance.HideAds();
                Debug.Log("buy: " + (purchaseEvent.purchasedProduct.definition.id));
                break;
            case LITTLE_PACK:
                PlayerDataController.Gems += 300;
                break;
            case MEDIUM_PACK:
                PlayerDataController.Gems += 2000;
                break;
            case BIG_PACK:
                PlayerDataController.Gems += 10000;
                break;
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
    }

    /// <summary>
    /// Проверить, куплен ли товар.
    /// </summary>
    /// <param name="id">Индекс товара в списке.</param>
    /// <returns></returns>
    public static bool CheckBuyState(string id)
    {
        Product _product = _storeController.products.WithID(id);
        return _product.hasReceipt;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        _storeExtensionProvider = extensions;
        
    }

    public static void BuyProductID(string productId)
    {
        if (!IsIapInitialized()) return;
        Product _product = _storeController.products.WithID(productId);
        Debug.Log("Try to buy: " + productId);
        Debug.Log(_product);
        Debug.Log(_product.availableToPurchase);
        if (!_product.availableToPurchase) return;
        Debug.Log("Start buy: " + productId);
        _storeController.InitiatePurchase(_product);
    }
}