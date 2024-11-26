using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in successfully!";
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = PlayFabSettings.TitleId // Ensure this is set in PlayFab settings
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset email sent!";
    }

    // Additional Methods

    public void GetAppearance()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Appearance"))
        {
            string appearanceJson = result.Data["Appearance"].Value;
            Debug.Log("Appearance data received: " + appearanceJson);
        }
        else
        {
            Debug.Log("No Appearance data found.");
        }
    }

    public void SaveAppearance(string appearanceData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "Appearance", appearanceData } }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnError);
    }

    void OnDataSent(UpdateUserDataResult result)
    {
        Debug.Log("Appearance data saved successfully!");
    }

    public void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataReceived, OnError);
    }

    void OnTitleDataReceived(GetTitleDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("GameTitle"))
        {
            string gameTitle = result.Data["GameTitle"];
            Debug.Log("Game title received: " + gameTitle);
        }
        else
        {
            Debug.Log("No title data found.");
        }
    }

    public void SaveCharacters(string characterData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "Characters", characterData } }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnError);
    }

    public void GetCharacters()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharactersDataReceived, OnError);
    }

    void OnCharactersDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Characters"))
        {
            string characterJson = result.Data["Characters"].Value;
            Debug.Log("Character data received: " + characterJson);
        }
        else
        {
            Debug.Log("No character data found.");
        }
    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.LogError(error.GenerateErrorReport());
    }
}
