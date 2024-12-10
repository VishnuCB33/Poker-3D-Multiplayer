using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyBackend : MonoBehaviour
{
    public static LobbyBackend Instance;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerAmount;
    public TMP_InputField PlayerName_input;
    public GameObject PlayerName_input_panel;
    public GameObject clime_panel;

    public int profilnumbackend;
    public int PlayerLanguageBackend;
  
    private void Start()
    {
     
        Instance = this;
      
        PlayfabManager.instance.GetPlayerData();
      

    }
    void Update()
    {
        profilnumbackend = Profile.instance.profilnum;
        PlayerName.text = PlayfabManager.instance.playername.ToString();
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayfabManager.instance.StorePlayerAvatarAndAmount( profilnumbackend, PlayerLanguageBackend);// when player change index time and when player change palyerklanguage time pleas call this thing

            PlayfabManager.instance.GetPlayerData(); //this two
        }
       
    }
    public void nemeopenpanel()
    {
        PlayerName_input_panel.SetActive(true);
    }

    public void SubmitButton()
    {
        if (PlayfabManager.instance != null)
        {
            string enteredName = PlayerName_input.text;

            if (!string.IsNullOrEmpty(enteredName))
            {
                PlayfabManager.instance.playername = enteredName;
                PlayfabManager.instance.submintnameButton();
               
                PlayerName_input_panel.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Entered name is empty. Please enter a valid name.");
            }
        }
        else
        {
            Debug.LogError("PlayfabManager.instance is null. Ensure PlayfabManager is initialized.");
        }
        PlayfabManager.instance.GetPlayerData();

    }
    public string GetPlayerName()
    {
        return PlayerName.text;
    }
    public void backendCoinClaim()
    {
      
        clime_panel.SetActive(false);

    }
    private void OnApplicationQuit()
    {
        Debug.Log("player data saved & player quit");
        PlayfabManager.instance.StorePlayerAvatarAndAmount(profilnumbackend, PlayerLanguageBackend);
    }
}
