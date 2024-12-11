using Unity.Netcode;
using UnityEngine;
using TMPro;

public class AmountSync : NetworkBehaviour
{
    // Define a NetworkVariable to sync across the network
    public NetworkVariable<int> amount = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone, // Everyone can read
        NetworkVariableWritePermission.Server  // Only the server can write
    );

    public TextMeshProUGUI amount_text;

    private void Update()
    {
        // Display the current value of 'amount' in the TextMeshPro component
        amount_text.text = amount.Value.ToString();

        // If the local player presses Space, request a change
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RequestUpdateServerRpc(amount.Value + 1);
        }

        // Display the current amount in the console for debugging
        Debug.Log($"Current Amount: {amount.Value}");
    }

    // A ServerRpc to allow clients to request an update
    [ServerRpc(RequireOwnership = false)]
    private void RequestUpdateServerRpc(int newAmount)
    {
        // Update the amount on the server
        amount.Value = newAmount;
    }
}
