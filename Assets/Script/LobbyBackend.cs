using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyBackend : MonoBehaviour
{
    public TextMeshProUGUI PlayerName; // Text to display player's name
    public TMP_InputField PlayerName_input; // Input field for player's name
    public GameObject PlayerName_input_panel; // Panel to input player's name

    void Update()
    {
        // Update the UI display of the player's name if PlayfabManager exists
        if (PlayfabManager.instance != null)
        {
            PlayerName.text = PlayfabManager.instance.playername; // Display the player's name

            // Show or hide the input panel based on whether the player's name is set
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

    // Called when the submit button is clicked
    public void SubmitButton()
    {
        // Ensure PlayfabManager instance exists
        if (PlayfabManager.instance != null)
        {
            string enteredName = PlayerName_input.text; // Get the entered name

            // Ensure the entered name is not empty
            if (!string.IsNullOrEmpty(enteredName))
            {
                PlayfabManager.instance.playername = enteredName; // Update playername in PlayfabManager
                PlayfabManager.instance.submintnameButton(); // Save the name to PlayFab
                PlayerName_input_panel.SetActive(false); // Optionally hide input panel
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
