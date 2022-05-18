using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.Purchasing;

public class IaPurchase : IStoreListener
{
    public const string RemoveAds = "remove_ads";
    public const string Coffee = "coffee";
    public const string BigPack = "get_big_pack";
    public const string LittlePack = "get_little_pack_daimonds";
    public const string MediumPack = "pack_medium_diamond";
    private static IStoreController _storeController;
    private static IExtensionProvider _storeExtensionProvider;

    public void IapInitialize()
    {
        if (IsIapInitialized())
            return;
        var _builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        _builder.AddProduct(RemoveAds, ProductType.NonConsumable);
        _builder.AddProduct(BigPack, ProductType.Consumable);
        _builder.AddProduct(LittlePack, ProductType.Consumable);
        _builder.AddProduct(MediumPack, ProductType.Consumable);
        _builder.AddProduct(Coffee, ProductType.Consumable);
        UnityPurchasing.Initialize(this, _builder);
    }

    private static bool IsIapInitialized()
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
        if (_storeController.products.WithID(RemoveAds).hasReceipt)
        {
            AdsAndIAP.isRemoveAds = true;
            AdsAndIAP.instance.HideAds();
        }

        AdsAndIAP.isRemoveAds = false;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        switch (purchaseEvent.purchasedProduct.definition.id)
        {
            case RemoveAds:
                
                AdsAndIAP.isRemoveAds = true;
                AdsAndIAP.instance.HideAds();
                Debug.Log("buy: " + (purchaseEvent.purchasedProduct.definition.id));
                break;
            case LittlePack:
                PlayerDataController.Gems += 1000;
                break;
            case MediumPack:
                PlayerDataController.Gems += 10000;
                break;
            case BigPack:
                PlayerDataController.Gems += 20000;
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