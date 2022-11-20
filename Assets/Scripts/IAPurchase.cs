using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.Purchasing;

public class IaPurchase : IStoreListener
{
    public static string REMOVE_ADS = "remove_ads";
    public static string COFFEE = "coffee";
    public static string BIG_PACK = "get_big_pack";
    public static string LITTLE_PACK = "get_little_pack_daimonds";
    public static string MEDIUM_PACK = "pack_medium_diamond";
    public static string KEY_BIG_PACK = "get_key_big_pack";
    public static string KEY_LITTLE_PACK = "get_key_little_pack_daimonds";
    public static string KEY_MEDIUM_PACK = "pack_key_medium_diamond";
    public static string SPECIAL_OFFER = "special_offer";
    public static string NOT_SPECIAL_OFFER = "not_special_offer";
    public static string BALL_TRAIL = "ball_trail";
    public static string BALL_ANIM = "ball_anim";
    public static int NumberOfPrice;
    public static IStoreController _storeController;
    private static IExtensionProvider _storeExtensionProvider;

    public void IapInitialize()
    {
        if (IsIapInitialized())
            return;
        if (NumberOfPrice != 0)
        {
            REMOVE_ADS += "_1";
            BIG_PACK += "_1";
            LITTLE_PACK += "_1";
            MEDIUM_PACK += "_1";
            COFFEE += "_1";
            SPECIAL_OFFER += "_1";
            NOT_SPECIAL_OFFER += "_1";
            BALL_TRAIL += "_1";
            BALL_ANIM += "_1";
        }

        var _builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        _builder.AddProduct(REMOVE_ADS, ProductType.NonConsumable);
        _builder.AddProduct(BIG_PACK, ProductType.Consumable);
        _builder.AddProduct(LITTLE_PACK, ProductType.Consumable);
        _builder.AddProduct(MEDIUM_PACK, ProductType.Consumable);
        _builder.AddProduct(COFFEE, ProductType.Consumable);
        _builder.AddProduct(SPECIAL_OFFER, ProductType.NonConsumable);
        _builder.AddProduct(NOT_SPECIAL_OFFER, ProductType.NonConsumable);
        _builder.AddProduct(BALL_TRAIL, ProductType.NonConsumable);
        _builder.AddProduct(BALL_ANIM, ProductType.NonConsumable);
        _builder.AddProduct(KEY_BIG_PACK, ProductType.Consumable);
        _builder.AddProduct(KEY_LITTLE_PACK, ProductType.Consumable);
        _builder.AddProduct(KEY_MEDIUM_PACK, ProductType.Consumable);


        UnityPurchasing.Initialize(this, _builder);

       Managers.GameManager.instance.StartCoroutine( SkinShopController.instance.InitSkins());
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
        if (CheckProduct("remove_ads") ||
            CheckProduct("remove_ads_1"))
        {
            AdsAndIAP.isRemoveAds = true;
            AdsAndIAP.instance.HideAds();
        }
    }

    public static IEnumerator CheckX2()
    {
        while (!IsIapInitialized())
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (_storeController?.products == null) yield break;
        if (CheckProduct("special_offer") ||
            CheckProduct("not_special_offer_1") ||
            CheckProduct("special_offer_1") ||
            CheckProduct("not_special_offer"))
        {
            SkinShopController.buyElementX2 = 2;
        }
    }

    public static bool CheckProduct(string id)
    {
        if (IsIapInitialized() && _storeController.products.WithID(id) != null)
            return _storeController.products.WithID(id).hasReceipt;
        else
            return false;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (purchaseEvent.purchasedProduct.definition.id == REMOVE_ADS)
        {
            AdsAndIAP.isRemoveAds = true;
            AdsAndIAP.instance.HideAds();
            Debug.Log("buy: " + (purchaseEvent.purchasedProduct.definition.id));
        }
        else if (purchaseEvent.purchasedProduct.definition.id == LITTLE_PACK)
        {
            PlayerDataController.Gems += 600;
        }
        else if (purchaseEvent.purchasedProduct.definition.id == MEDIUM_PACK)
        {
            PlayerDataController.Gems += 4000;
        }
        else if (purchaseEvent.purchasedProduct.definition.id == BIG_PACK)
        {
            PlayerDataController.Gems += 20000;
        } else if (purchaseEvent.purchasedProduct.definition.id == KEY_LITTLE_PACK)
        {
            PlayerDataController.Key += 10;
        } else if (purchaseEvent.purchasedProduct.definition.id == KEY_MEDIUM_PACK)
        {
            PlayerDataController.Gems += 4000;
            PlayerDataController.Key += 20;
        } else if (purchaseEvent.purchasedProduct.definition.id == KEY_BIG_PACK)
        {
            PlayerDataController.Key += 40;
        }
        else if (purchaseEvent.purchasedProduct.definition.id == SPECIAL_OFFER)
        {
            SkinShopController.ConfBuyTrail();
            SkinShopController.ConfBuyAnim();
            SkinShopController.buyElementX2 = 2;
        }
        else if (purchaseEvent.purchasedProduct.definition.id == NOT_SPECIAL_OFFER)
        {
            SkinShopController.ConfBuyTrail();
            SkinShopController.ConfBuyAnim();
            SkinShopController.buyElementX2 = 2;
        }
        else if (purchaseEvent.purchasedProduct.definition.id == BALL_ANIM)
        {
            SkinShopController.ConfBuyAnim();
        }
        else if (purchaseEvent.purchasedProduct.definition.id == BALL_TRAIL)
        {
            SkinShopController.ConfBuyTrail();
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