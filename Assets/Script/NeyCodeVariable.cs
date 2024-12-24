using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InspectorSyncVariable : NetworkBehaviour
{
    // Networked variable to sync across systems
    public NetworkVariable<int> SyncedValue = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server
    );

    [SerializeField]
    private int inspectorValue; // This is the value displayed in the inspector

    private int previousInspectorValue; // To track changes in the inspector

    void Start()
    {
        // Initialize the local value with the networked value
        if (IsServer)
        {
            inspectorValue = SyncedValue.Value;
            previousInspectorValue = inspectorValue;
        }
    }

    void Update()
    {
        if (IsServer)
        {
            // Check if the inspector value changes
            if (inspectorValue != previousInspectorValue)
            {
                // Update the network variable
                SyncedValue.Value = inspectorValue;
                previousInspectorValue = inspectorValue;
            }
        }
        else
        {
            // Update the local inspector value when the network variable changes
            inspectorValue = SyncedValue.Value;
        }
    }

    private void OnGUI()
    {
        // Display the current synced value for debugging
        GUILayout.Label("Synced Value: " + SyncedValue.Value);
    }
}
