using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class ClaimAmount : MonoBehaviour
{
    public int claim;

   

    public void PurchaseVirtualCurrency(int claim)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CN",
            Amount = claim
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnError);
    }

    public void OnPurchaseButtonClick()
    {
      // int claim = 100; 
        PurchaseVirtualCurrency(claim);
    }

    void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log($"Successfully added {result.BalanceChange} coins. New Balance: {result.Balance}");

        // Update the UI in AmountManager
        if (AmountManager.Instance != null)
        {
            AmountManager.Instance.GetVirtualCurrencies();
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.ErrorMessage);
    }
}
