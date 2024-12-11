using Unity.Netcode;
using UnityEngine;
using TMPro;

public class AmountSync : NetworkBehaviour
{
    // Define a NetworkVariable to sync across the network
    public NetworkVariable<int> amount = new NetworkVariable<int>(0);
    public TextMeshProUGUI amount_text;

    private void Update()
    {
        // Display the current value of 'amount' in the TextMeshPro component
        amount_text.text = amount.Value.ToString();

        // Only the server can change the value of the NetworkVariable
        if (IsServer && Input.GetKeyDown(KeyCode.Space))
        {
            UpdateAmount(amount.Value + 1);
        }

        // Display the current amount in the console for debugging
        Debug.Log($"Current Amount: {amount.Value}");
    }

    // A method to update the amount (only callable on the server)
    public void UpdateAmount(int newAmount)
    {
        if (IsServer)
        {
            amount.Value = newAmount;
        }
    }
}
