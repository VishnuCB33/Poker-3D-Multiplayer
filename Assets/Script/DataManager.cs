using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance; // for use the data in all script

    [Header("UI")]
    public TMP_Text messageText; // Message for user feedback
    public string playername; // Current player'royalFlushVar name
    public TMP_InputField emailInput; // Input field for email
    public TMP_InputField passwordInput; // Input field for password
    public TMP_InputField nameInputText; // Input field for display name
    public GameObject login_panel;
    public GameObject Resent_panel;

    public static string PlayerDisplayName; // Global variable for display name
    public int player_playerprofile_backend; // Amount to be stored and retrieved from PlayFab
    public int player_language_backend; // Amount to be stored and retrieved from PlayFab

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject); // Keep this object across scenes
    }

    #region Register/Login/Reset Password
    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6) //Checks if the password is long enough (at least 6 characters). If not, shows a message.
        {
            messageText.text = "Password too short!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text, // for Get email entered by the player.
            Password = passwordInput.text, //get password by the player
            RequireBothUsernameAndEmail = false // We don't want to require username, just the display name.
        };
        login_panel.SetActive(true);

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError); // Sends the request to PlayFab server.
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
        Debug.Log("Registration successful!");
        SceneManager.LoadScene(1); // Load next scene

        var request = new GetAccountInfoRequest(); // Request to get account information.
        PlayFabClientAPI.GetAccountInfo(request, OnAccountInfoReceived, OnError);
    }

    void OnAccountInfoReceived(GetAccountInfoResult result)
    {
        string name = result.AccountInfo.TitleInfo.DisplayName;

        if (string.IsNullOrEmpty(name)) // Check if display name is empty.
        {
            messageText.text = "Set a display name.";
            LobbyBackend.Instance.nemeopenpanel();
        }
        else
        {
            PlayerDisplayName = name;
            playername = PlayerDisplayName;
            /* LobbyBackend.Instance.PlayerName_input_panel.SetActive(false);*/
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
        login_panel.SetActive(true);


        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);

    }

    void OnLoginSuccess(LoginResult result)
    {

        messageText.text = "Logged in!";
        SceneManager.LoadScene(1); // Load next scene

        string name = result.InfoResultPayload.PlayerProfile?.DisplayName;

        if (string.IsNullOrEmpty(name)) // Check if display name exists.
        {
            messageText.text = "Set a display name.";
            LobbyBackend.Instance.nemeopenpanel();

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
            TitleId = "4F828" // Use your game title ID here
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        Resent_panel.SetActive(true);
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

    // Store player language and profile pic in PlayFab
    public void StorePlayerAvatarAndAmount(int playerprofile, int playerlanguage)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "playerprofile", playerprofile.ToString() } , // Convert player amount to string
                { "playerlanguage", playerlanguage.ToString() } // Convert player playerlanguage to string
                 
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataStored, OnError);
    }

    private void OnDataStored(UpdateUserDataResult result)
    {
        messageText.text = "Player data updated!";
        Debug.Log("Player amount stored successfully.");
    }

    // Retrieve player data
    public void GetPlayerData()
    {
        var request = new GetUserDataRequest
        {
            PlayFabId = PlayFabSettings.staticPlayer.PlayFabId
        };

        PlayFabClientAPI.GetUserData(request, OnDataReceived, OnError);
    }

    private void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data == null || result.Data.Count == 0)
        {
            player_language_backend = 0;
            player_playerprofile_backend = 0;

            Debug.LogError("No user data found.");
            return;
        }

        if (result.Data.ContainsKey("playerprofile"))
        {
            if (int.TryParse(result.Data["playerprofile"].Value, out int playerprofile))
            {
                player_playerprofile_backend = playerprofile;
            }
            else
            {
                player_playerprofile_backend = 0;
                Debug.LogError("'PlayerAmount' value is not a valid integer.");
            }

            if (int.TryParse(result.Data["playerlanguage"].Value, out int playerlanguage))
            {
                player_language_backend = playerlanguage;
            }
            else
            {
                player_language_backend = 0;

            }
        }
        else
        {
            Debug.LogWarning("'PlayerAmount' key not found.");
        }
    }

    #region Error Handling
    void OnError(PlayFabError error)
    {
        login_panel.SetActive(false);

        messageText.text = error.ErrorMessage;
        Debug.LogError(error.GenerateErrorReport());
    }
    //ui
    public void closeResentPanel()
    {
        Resent_panel.SetActive(false);
    }
    #endregion
}