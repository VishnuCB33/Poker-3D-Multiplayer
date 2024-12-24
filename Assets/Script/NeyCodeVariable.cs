using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NeyCodeVariable : NetworkBehaviour
{
    public NetworkVariable<int> PlayerAmount = new NetworkVariable<int>(0); // Networked variable to sync across players

    private int previousAmount = -1; // Local variable to track changes

    void Update()
    {
        // Handle spacebar press to increment the amount
        if (IsOwner && Input.GetKeyDown(KeyCode.Space))
        {
            IncrementAmount();
        }

        // Sync the local backend amount to the NetworkVariable only if it changes
        if (IsOwner && Game_Basic_Code.instance.amount_backend_1 != previousAmount)
        {
            previousAmount = Game_Basic_Code.instance.amount_backend_1;
            PlayerAmount.Value = previousAmount; // Synchronize the new amount
        }
    }

    private void IncrementAmount()
    {
        Game_Basic_Code.instance.amount_backend_1++; // Update the backend value
        PlayerAmount.Value = Game_Basic_Code.instance.amount_backend_1; // Sync with the network
    }

    private void OnGUI()
    {
        // Display the current PlayerAmount for debugging
        GUILayout.Label("Player Amount: " + PlayerAmount.Value);
    }
}
