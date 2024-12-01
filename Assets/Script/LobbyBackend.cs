using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyBackend : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerAmount;
    public TMP_InputField PlayerName_input;
    public GameObject PlayerName_input_panel;
    
    public int player_amount ;
    public int previous_player_amount ;
    private void Start()
    {
        player_amount = 100;
            PlayfabManager.instance.GetPlayerData();
        PlayfabManager.instance.StorePlayerAvatarAndAmount(player_amount);

    }
    void Update()
    {
        
       
        if (player_amount != previous_player_amount)
        {
           
            PlayfabManager.instance.StorePlayerAvatarAndAmount(player_amount);
            
            previous_player_amount = player_amount;

         
        }
        PlayerName.text = PlayfabManager.instance.playername.ToString();
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayfabManager.instance.GetPlayerData();
        }
        PlayerAmount.text = PlayfabManager.instance.player_amount_backend.ToString();

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player_amount += 100;

        };
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
    }
}
