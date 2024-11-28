using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;

    [Header("UI")]
    public TMP_Text messageText; // Message for user feedback
    public string playername; // Current player's name
    public TMP_InputField emailInput; // Input field for email
    public TMP_InputField passwordInput; // Input field for password
    public TMP_InputField nameInputText; // Input field for display name

    public static string PlayerDisplayName; // Global variable for display name

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject); // Keep this object across scenes
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
        SceneManager.LoadScene(1); // Load next scene

        // Retrieve Display Name after registration
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnAccountInfoReceived, OnError);
    }

    void OnAccountInfoReceived(GetAccountInfoResult result)
    {
        string name = result.AccountInfo.TitleInfo.DisplayName;

        if (string.IsNullOrEmpty(name))
        {
            messageText.text = "Set a display name.";
        }
        else
        {
            PlayerDisplayName = name;
            playername = PlayerDisplayName;
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
        SceneManager.LoadScene(1); // Load next scene

        string name = result.InfoResultPayload.PlayerProfile?.DisplayName;

        if (string.IsNullOrEmpty(name))
        {
            messageText.text = "Set a display name.";
        }
        else
        {
            PlayerDisplayName = name;
            playername = PlayerDisplayName;
        }

        Debug.Log("Successful login!");
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = PlayFabSettings.TitleId
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset email sent!";
        Debug.Log("Password reset email sent.");
    }
    #endregion

    #region Display Name
    public void submintnameButton()
    {
        if (string.IsNullOrEmpty(playername))
        {
            Debug.LogError("Display name is empty. Please enter a valid name.");
            return;
        }

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = playername
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"Display name updated to: {result.DisplayName}");
        PlayerDisplayName = result.DisplayName;
        playername = PlayerDisplayName;
        messageText.text = "Display name updated successfully!";
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
