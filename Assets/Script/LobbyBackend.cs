using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyBackend : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public TMP_InputField PlayerName_input;
    public GameObject PlayerName_input_panel;

    // Update is called once per frame
    void Update()
    {
        // Display the player's name stored in PlayfabManager
        if (PlayfabManager.instance != null)
        {
            PlayerName.text = PlayfabManager.instance.playername; // Display the player's name
        }

        // Show or hide the name input panel based on whether the player has set a name
        if (PlayfabManager.instance != null)
        {
            // If player has not set a name, show the input panel
            if (string.IsNullOrEmpty(PlayfabManager.instance.playername))
            {
                PlayerName_input_panel.SetActive(true); // Show input panel
            }
            else
            {
                PlayerName_input_panel.SetActive(false); // Hide input panel if name is set
            }
        }
    }

    // Method to submit the player name
    public void SubmitButton()
    {
        // Check if PlayfabManager.instance is initialized
        if (PlayfabManager.instance != null)
        {
            string enteredName = PlayerName_input.text; // Get the entered name from the input field
            
            if (!string.IsNullOrEmpty(enteredName)) // Ensure the entered name is not empty
            {
                PlayfabManager.instance.playername = enteredName; // Update the player name in PlayfabManager

                // Call PlayfabManager's method to submit the name to PlayFab
                PlayfabManager.instance.submintnameButton();

                // Optionally, hide the input panel after submitting the name
                PlayerName_input_panel.SetActive(false); // Hide input panel after name submission
            }
            else
            {
                Debug.LogWarning("Entered name is empty. Please enter a name.");
            }
        }
        else
        {
            Debug.LogError("PlayfabManager.instance is null. Ensure PlayfabManager is initialized.");
        }
    }
}
