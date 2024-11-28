using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;
    [Header("UI")]
    public TMP_Text messageText;
    public string playername;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public bool nameWindow;
    public TextMeshProUGUI nameInputText;

    public static string PlayerDisplayName; // chat gpt: Added static variable to store Display Name globally

    void Start()
    {
        instance = this;
        // Initialize or load any required components
        DontDestroyOnLoad(gameObject); // chat gpt: Ensures this object persists across scenes
    }

    private void Update()
    {
    }

    #region Register/Login/Reset Password
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
        Debug.Log("Registration successful!");
        SceneManager.LoadScene(1);
        // chat gpt: Retrieve Display Name after registration
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnAccountInfoReceived, OnError);
    }

    void OnAccountInfoReceived(GetAccountInfoResult result)
    {
        string name = result.AccountInfo.TitleInfo.DisplayName;

        if (string.IsNullOrEmpty(name))
        {
            nameWindow = true; // chat gpt: Prompt user to enter Display Name
        }
        else
        {
            PlayerDisplayName = name; // chat gpt: Store Display Name globally
            playername = PlayerDisplayName.ToString(); // chat gpt: Update UI
            nameWindow=false;
        }

        Debug.Log("Account information retrieved successfully.");
    }


    public void LoginButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        SceneManager.LoadScene(1); // Optional: Add scene change logic here

        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
        {
            nameWindow=true;
        }
        else
        {
            PlayerDisplayName = name; // chat gpt: Store Display Name globally
            playername = PlayerDisplayName; // chat gpt: Update UI
            nameWindow=false;
        }

        Debug.Log("Successful login!");
        GetCharacters(); // Example call to fetch character data after login
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "4F828"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    public void submintnameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInputText.text,
        };
        nameWindow = false;

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplaynameUpdate, OnError);
    }

    void OnDisplaynameUpdate(UpdateUserTitleDisplayNameResult result) // chat gpt: Fixed parameter type
    {
        Debug.Log("Updated display name!");
        PlayerDisplayName = result.DisplayName; // chat gpt: Store Display Name globally
        playername= PlayerDisplayName; // chat gpt: Update UI
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset mail sent!";
        Debug.Log("Password reset email sent.");
    }
    #endregion

    #region Player Data
    public void GetAppearance()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Appearance"))
        {
            string appearance = result.Data["Appearance"].Value;
            Debug.Log($"Appearance data: {appearance}");
        }
        else
        {
            Debug.Log("No appearance data found.");
        }
    }

    public void SaveAppearance(string appearanceData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                { "Appearance", appearanceData }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnError);
    }

    void OnDataSent(UpdateUserDataResult result)
    {
        Debug.Log("Appearance data saved successfully.");
    }
    #endregion

    #region Title Data
    public void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataReceived, OnError);
    }

    void OnTitleDataReceived(GetTitleDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Key"))
        {
            string titleData = result.Data["Key"];
            Debug.Log($"Title data: {titleData}");
        }
        else
        {
            Debug.Log("No title data found.");
        }
    }
    #endregion

    #region Characters Handling
    public void SaveCharacters(string characterData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                { "Characters", characterData }
            }
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
            string characterData = result.Data["Characters"].Value;
            Debug.Log($"Character data: {characterData}");
        }
        else
        {
            Debug.Log("No character data found.");
        }
    }
    #endregion

    #region Error Handling
    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion
}
