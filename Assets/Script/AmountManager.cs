using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine;

public class AmountManager : MonoBehaviour
{public static AmountManager Instance;
    [Header("UI")]
    public TextMeshProUGUI amount;

    void Start()
    {
        Login();
    }
    public void Awake()
    {
        Instance = this;
    }
    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful Login");
        GetVirtualCurrencies();
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        if (result.VirtualCurrency != null && result.VirtualCurrency.ContainsKey("CN"))
        {
            int coins = result.VirtualCurrency["CN"];
            amount.text = coins.ToString();
        }
        else
        {
            Debug.LogWarning("Virtual currency 'CN' not found.");
            amount.text = "0";
        }
    }
  
    void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.ErrorMessage);
    }
}
