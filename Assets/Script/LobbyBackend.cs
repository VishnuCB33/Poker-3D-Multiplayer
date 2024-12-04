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

    public int indexnumBackend;
    public int PlayerLanguageBackend;
    public int player_amount ;
    public int previous_player_amount ;
    private void Start()
    {
        Instance = this;
      
        PlayfabManager.instance.GetPlayerData();
       // PlayfabManager.instance.StorePlayerAvatarAndAmount(player_amount);

    }
    void Update()
    {
        indexnumBackend = LobbyManager.Instance.playerIndex;
       
        if (player_amount != previous_player_amount)
        {
           
            PlayfabManager.instance.StorePlayerAvatarAndAmount(player_amount, indexnumBackend, PlayerLanguageBackend);
            
            previous_player_amount = player_amount;

         
        }
        PlayerName.text = PlayfabManager.instance.playername.ToString();
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayfabManager.instance.StorePlayerAvatarAndAmount(player_amount, indexnumBackend, PlayerLanguageBackend);// when player change index time and when player change palyerklanguage time pleas call this thing

            PlayfabManager.instance.GetPlayerData(); //this two
        }
        PlayerAmount.text =PlayfabManager.instance.player_amount_backend.ToString()+"$" ;

        

       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            player_amount += 100;

        };*/
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
        player_amount = 10000;
        clime_panel.SetActive(false);

    }
}
