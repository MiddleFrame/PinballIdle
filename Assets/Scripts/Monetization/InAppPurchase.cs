using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Zenject;

public class InAppPurchase
{
    public const string REMOVE_ADS = "remove_ads";
    public const string COFFEE = "coffee";
    public const string BIG_PACK = "get_big_pack";
    public const string LITTLE_PACK = "get_little_pack_daimonds";
    public const string MEDIUM_PACK = "pack_medium_diamond";
    public const string KEY_BIG_PACK = "get_key_big_pack";
    public const string KEY_LITTLE_PACK = "get_key_little_pack_daimonds";
    public const string KEY_MEDIUM_PACK = "pack_key_medium_diamond";
    public const string SPECIAL_OFFER = "special_offer";
    public const string NOT_SPECIAL_OFFER = "not_special_offer";
    public const string BALL_TRAIL = "ball_trail";
    public const string BALL_ANIM = "ball_anim";

    public static Dictionary<string, bool> fetchedProduct;
    public static StoreController StoreController { get; private set; }

    public async Task IapInitialize()
    {
        if (IsIapInitialized())
            return;


        StoreController = UnityIAPServices.StoreController();
        await StoreController.Connect();

        List<ProductDefinition> initialProductsToFetch = new()
        {
            new ProductDefinition(REMOVE_ADS, ProductType.NonConsumable),
            new ProductDefinition(BIG_PACK, ProductType.Consumable),
            new ProductDefinition(LITTLE_PACK, ProductType.Consumable),
            new ProductDefinition(MEDIUM_PACK, ProductType.Consumable),
            new ProductDefinition(COFFEE, ProductType.Consumable),
            new ProductDefinition(SPECIAL_OFFER, ProductType.NonConsumable),
            new ProductDefinition(NOT_SPECIAL_OFFER, ProductType.NonConsumable),
            new ProductDefinition(BALL_TRAIL, ProductType.NonConsumable),
            new ProductDefinition(BALL_ANIM, ProductType.NonConsumable),
            new ProductDefinition(BALL_ANIM, ProductType.NonConsumable),
            new ProductDefinition(KEY_BIG_PACK, ProductType.Consumable),
            new ProductDefinition(KEY_LITTLE_PACK, ProductType.Consumable),
            new ProductDefinition(KEY_MEDIUM_PACK, ProductType.Consumable)
        };

        StoreController.OnProductsFetched += OnProductsFetched;
        StoreController.OnPurchasesFetched += OnPurchasesFetched;
        StoreController.FetchProducts(initialProductsToFetch);
        StoreController.OnPurchasePending += ProcessPurchase;
        StoreController.OnPurchaseConfirmed += OnPurchaseConfirmed;
    }

    public static bool CheckProduct(string productId)
    {
        return fetchedProduct[productId];
    }
    void OnPurchasesFetched(Orders orders)
    {
        AdsAndIAP.isRemoveAds = false;
        foreach (var _order in orders.ConfirmedOrders)
        {
            var product = _order.CartOrdered.Items().FirstOrDefault()?.Product;
            if (product == null) continue;
            fetchedProduct.Add(product.definition.id, true);
            switch (product.definition.id)
            {
                case "remove_ads":
                case "remove_ads_1":
                    AdsAndIAP.isRemoveAds = true;
                    AdsAndIAP.instance.HideAds();
                    break;
                case "special_offer":
                case "not_special_offer_1":
                case "special_offer_1":
                case "not_special_offer":
                    SkinShopController.buyElementX2 = 2;
                    break;
            }
        }
    }

    public static bool IsIapInitialized()
    {
        return StoreController != null;
    }


    private static void OnProductsFetched(List<Product> products)
    {
        StoreController.FetchPurchases();
    }
    void OnPurchaseConfirmed(Order order)
    {
        switch (order)
        {
            case ConfirmedOrder confirmedOrder:
                OnPurchaseConfirmed(confirmedOrder);
                break;
            case FailedOrder failedOrder:
                OnPurchaseConfirmationFailed(failedOrder);
                break;
            default:
                Debug.Log("Unknown OnPurchaseConfirmed result.");
                break;
        }
    }
    void OnPurchaseConfirmed(ConfirmedOrder order)
    {
        var product = GetFirstProductInOrder(order);
        if (product == null)
        {
            Debug.Log("Could not find product in purchase confirmation.");
        }

        Debug.Log($"Purchase confirmed- Product: {product?.definition.id}");
    }

    void OnPurchaseConfirmationFailed(FailedOrder order)
    {
        var product = GetFirstProductInOrder(order);
        if (product == null)
        {
            Debug.Log("Could not find product in failed confirmation.");
        }

        Debug.Log($"Confirmation failed - Product: '{product?.definition.id}'," +
                  $"PurchaseFailureReason: {order.FailureReason.ToString()},"
                  + $"Confirmation Failure Details: {order.Details}");
    }
    Product GetFirstProductInOrder(Order order)
    {
        return order.CartOrdered.Items().First()?.Product;
    }
    private static void ProcessPurchase(PendingOrder pendingOrder)
    {
        var product = pendingOrder.CartOrdered.Items().FirstOrDefault()?.Product;
        if (product == null)
        {
            Debug.LogError("Purchase confirmed but product is null.");
            return;
        }

        if (product.definition.id == REMOVE_ADS)
        {
            AdsAndIAP.isRemoveAds = true;
            AdsAndIAP.instance.HideAds();
        }
        else if (product.definition.id == LITTLE_PACK)
        {
            PlayerDataController.Gems += 600;
        }
        else if (product.definition.id == MEDIUM_PACK)
        {
            PlayerDataController.Gems += 4000;
        }
        else if (product.definition.id == BIG_PACK)
        {
            PlayerDataController.Gems += 20000;
        }
        else if (product.definition.id == SPECIAL_OFFER)
        {
            SkinShopController.ConfBuyTrail();
            SkinShopController.ConfBuyAnim();
            SkinShopController.buyElementX2 = 2;
        }
        else if (product.definition.id == NOT_SPECIAL_OFFER)
        {
            SkinShopController.ConfBuyTrail();
            SkinShopController.ConfBuyAnim();
            SkinShopController.buyElementX2 = 2;
        }
        else if (product.definition.id == BALL_ANIM)
        {
            SkinShopController.ConfBuyAnim();
        }
        else if (product.definition.id == BALL_TRAIL)
        {
            SkinShopController.ConfBuyTrail();
        }
        StoreController.ConfirmPurchase(pendingOrder);
    }
    

    public static void BuyProductID(string productId)
    {
        if (!IsIapInitialized()) return;
        Debug.Log("Try to buy: " + productId);
        Product _product = StoreController.GetProductById(productId);

        if (_product == null || !_product.availableToPurchase) return;
        Debug.Log("Start buy: " + productId);
        StoreController.PurchaseProduct(productId);
    }
}