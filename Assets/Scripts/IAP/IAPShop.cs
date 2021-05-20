using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPShop : MonoBehaviour
{
    private string buy = "com.sansan-33.bellum.buy";

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "buy")
        {
            // implement buy action
            Debug.Log("Buy Success! Get gem!!!");
        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {

    }
}