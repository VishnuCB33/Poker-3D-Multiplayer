using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine;

public class AmountManager : MonoBehaviour
{ 
    public static AmountManager Instance;
    [Header("UI")]
    public TextMeshProUGUI amount;
    public int amountInt;
    
    void Start()
    {
        Login();
       /* string text = amount.text.Trim(); // Get the text and trim any leading/trailing spaces

        int result;

        // Try to parse the string to an integer
        if (int.TryParse(text, out result))
        {
            Debug.Log("Parsed integer: " + result);

            // Display the parsed integer in the TextMeshProUGUI
            amount.text = result.ToString(); // Convert the integer back to a string and set it in the TextMeshProUGUI
        }
        else
        {
            Debug.LogError("Failed to parse the value: " + text);
        }*/
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
            amountInt = result.VirtualCurrency["CN"];
            amount.text = amountInt.ToString();
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
