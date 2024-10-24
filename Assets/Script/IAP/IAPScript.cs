using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPScript : MonoBehaviour, IStoreListener
{
    private IAPData data;
    public Button noAdsButton;

    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    // Replace "your_product_id" with the actual product ID
    private static string productID = "noads";

    public GameObject red;
    public GameObject yellow;
    public GameObject green;

    void Start()
    {
        //set visibility of button based of whether purchase has been made
        
        data = SaveSystem.getIAPData();

        if (data.getNoAds()) {
            noAdsButton.interactable = false;
        }

        InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add your product ID to the builder
        builder.AddProduct(productID, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
        
        // yellow.SetActive(true);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // green.SetActive(true);
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // red.SetActive(true);
        Debug.Log("Initialization failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        // red.SetActive(true);
        Debug.LogError("Initialization failed: " + error + ", " + message);
    }

    public void PurchaseProduct()
    {
        if (storeController != null)
        {
            Product product = storeController.products.WithID(productID);

            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
                Debug.Log("product purchased");
            }

            else
            {
                Debug.Log("Product not available for purchase.");
            }
        }

        else
        {
            Debug.Log("Store controller is not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        data.setNoAds();
        noAdsButton.interactable = false;
        Debug.Log("Purchase successful: " + args.purchasedProduct.definition.id);
        
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase failed: " + failureReason);
        // Add your logic for handling the failed purchase here
    }
}
