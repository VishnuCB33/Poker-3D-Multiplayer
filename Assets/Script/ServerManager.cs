using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class ServerManager : MonoBehaviour
{
    private void Start()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Running as Server");
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client connected with ID: {clientId}");
    }
}
