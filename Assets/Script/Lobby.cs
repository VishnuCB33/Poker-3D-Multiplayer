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
    public string Id { get; private set; }

    [Header("Lobby Active Long Time")]
    private Lobby hostLobby;
    private float heartBeatTimer;

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


    private void Update()
    {
        HandleLobbyHeartBeat();
    }
    //30 sec inactive lobby so increseits life span
    private async void HandleLobbyHeartBeat()
    {
        if(hostLobby != null)
        {
            heartBeatTimer -= Time.deltaTime;
            if(heartBeatTimer < 0 )
            {
                float heartBeatTimerMAx = 15f;
                heartBeatTimer = heartBeatTimerMAx;
               await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }
    //Lobby Function
    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPlayers = 4;
            CreateLobbyOptions createLobbyOption = new CreateLobbyOptions
            {
                IsPrivate = false,
            };
            //parameter loobyname and max no.of players
            Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers,createLobbyOption);
            Unity.Services.Lobbies.Models.Lobby hostLobby = lobby;
            Debug.Log("LobbyName  !" + lobby.Name + "  " + lobby.MaxPlayers+" "+lobby.Id+" "+lobby.LobbyCode);
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
            QueryLobbiesOptions queryLobbiesOption = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots,"0",QueryFilter.OpOptions.GT)
                },
                //new lobby first 
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false,QueryOrder.FieldOptions.Created)
                }
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOption);
            Debug.Log("Lobbies Found " + queryResponse.Results.Count);
            IList list = queryResponse.Results;
            for (int i = 0; i < list.Count; i++)
            {
                Lobby lobby = (Lobby)list[i];
                Debug.Log(lobby.Name + "  " + lobby.MaxPlayers);
            }
        }catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public  async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
          
           
            
           await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);
            Debug.Log("Joined Lobby with Code :" + lobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
       
    }
    //players Quick Join
    public async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
}
