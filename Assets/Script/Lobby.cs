using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
public class Lobby : MonoBehaviour
{
    public string Name { get; private set; }
    public string MaxPlayers { get; private set; }

    private async void Start()
    {
        //Initialise unity services
       await UnityServices.InitializeAsync();
        //create new account for this user
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("SignedIn: "+AuthenticationService.Instance.PlayerId);
        };
        //players join without any signIn Option
      await  AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    //Lobby Function
    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPlayers = 4;
            //parameter loobyname and max no.of players
            Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            Unity.Services.Lobbies.Models.Lobby hostLobby = lobby;
            Debug.Log("LobbyName  !" + lobby.Name + "  " + lobby.MaxPlayers);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void ListLobbies()
    {
        try
        {
            //Lobbies ne filter aakki
            QueryLobbiesOptions queryLobboesOption = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    //GT means Greater Than
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots,"0",QueryFilter.OpOptions.GT)
                },
                //Rooms kanandath latest create aakkiyath 1st
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false,QueryOrder.FieldOptions.Created)
                }
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobboesOption);
            Debug.Log("Lobbies Found:  " + queryResponse.Results.Count);
            IList list = queryResponse.Results;
            for (int i = 0; i < list.Count; i++)
            {
                Lobby lobby = (Lobby)list[i];
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
