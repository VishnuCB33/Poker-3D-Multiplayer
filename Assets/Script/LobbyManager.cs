using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
public class LobbyManager : MonoBehaviour
{
    private string playerId;
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuPanel;
    // [SerializeField] private Button getLobbiesListButton;
    [SerializeField] private GameObject lobbyInfoPrefab;
    [SerializeField] private GameObject lobbiesInfoContent;
    //  [SerializeField] private TextMeshProUGUI roomNameText;
    [SerializeField] private TextMeshProUGUI playerNames;
    [Space(10)]
    [Header("Create Room panel")]
    [SerializeField] private GameObject createRoomPanel;
    [SerializeField] private TMP_InputField roomNameIF;
    [SerializeField] private TMP_InputField maxPlayerIF;
    //[SerializeField] private Button createRoomButt;
    [SerializeField] private GameObject listOfLobbyPanel;

    [Header("Inside Room ")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI roomCode;
    [SerializeField] private GameObject playerInfoContent;
    [SerializeField] private GameObject playerInfoPrefab;

     private Lobby currentLobby;

       async void Start()
       {
         //Initialize unity services,and add await keyword for wait for the initialize done,async automatically add
          await  UnityServices.InitializeAsync();
        
        //print player ID
        AuthenticationService.Instance.SignedIn += () =>
        {
            //after signed in we get playerID
            playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log("You SuccessFully Signed in: " + playerId);
           

        };

        //next step unity authentication,(many type of authentications are there google,fb,we use anonymous authentication means automatically sign
         await  AuthenticationService.Instance.SignInAnonymouslyAsync();
        //CreateLobby Function active when click on Create Room Button
        //createRoomButt.onClick.AddListener(CreateLobby);

        }

    // Update is called once per frame
    void Update()
    {
        //TimerHeartBeat
        HandleLobbyHeartBeat();
    }
    //Lobby Creation
    public  async void CreateLobby()
    {
        try
        {
            string lobbbyName = roomNameIF.text;
            //maxplayer inputfield Output to integer
            int.TryParse(maxPlayerIF.text, out int maxPlayers);

            CreateLobbyOptions options = new CreateLobbyOptions
            {
                Player = GetPlayer()
            };
            //Parameter= Lobby name and max players
             currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbbyName, maxPlayers,options);
            //Lobby Id
            Debug.Log("Room Created :" + currentLobby.Id);
            EnterRoom();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
       
    }
    private void EnterRoom()
    {
        mainMenuPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        roomPanel.SetActive(true);
        //Now RoomCode and roomName to roomPanel
        roomName.text=currentLobby.Name;
        roomCode.text = currentLobby.LobbyCode;
        foreach(Player player  in currentLobby.Players)
        {
            Debug.Log("Player Name : " + player.Data["PlayerName"].Value);
        }

    }
  public void RoomBackButt()
    {
        roomPanel.SetActive(false);
        createRoomPanel.SetActive(true);
    }
   public void ListLobbiesBack()
    {
        listOfLobbyPanel.SetActive(false);
        createRoomPanel.SetActive(true);
    }
    //To display all available public Lobbies
    public async void ListPublicLobbies()
    {
        listOfLobbyPanel.SetActive(true);
        createRoomPanel.SetActive(false);
        try
        {
            //Search---QueryLobbiesAsync Method will return QueryResponse type variable
            QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync();
            //We can get List of lobbies from this response variable dot result ---This debug will shows the number of lobbies available 
            Debug.Log("Available Public Lobbies :"+response.Results.Count);
            foreach(Lobby _lobby in response.Results)
            {
                //Only members can see Lobby code so _lobby.Id 
                Debug.Log("Lobby Name  :" + _lobby.Name + "Lobby ID :" + _lobby.Id);
            }
            VisualizeLobbyList(response.Results);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    //This function is helps to alive the lobby otherwise the lobby  will inactive after 30 second
    private float heartBeatTimer = 15f;
    private async void HandleLobbyHeartBeat()
    {
        //check if Lobby is not be null we decrease the heartbeatTimer and it will be 0 we will increase 15
        if (currentLobby != null&& isHost())
        {
            heartBeatTimer -= Time.deltaTime;
            if( heartBeatTimer < 0)
            {
                heartBeatTimer = 15f;
                //Host will call send  heartBeat ping async function and pass current lobby id to it 
               
               await LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }
    }
    //Host canonly send HeartBEat so check the player host or not
    private bool isHost()
    {
        //We check host id and player id are same or not
        if(currentLobby != null && currentLobby.HostId == playerId)
        {
            return true;
        }
        return false;
    }
    //Visualize the Lobby List Prefab
    private void VisualizeLobbyList(List<Lobby> _publicLobbies)
    {

        // We need to clear previous lobbiesInfo
        for(int i = 0; i < lobbiesInfoContent.transform.childCount; i++)
        {
            Destroy(lobbiesInfoContent.transform.GetChild(i).gameObject);   
        }
        foreach (Lobby _lobby in _publicLobbies)
        {
            // Instantiate the prefab
            GameObject newLobbyInfo = Instantiate(lobbyInfoPrefab, lobbiesInfoContent.transform);

            // Get all TextMeshProUGUI components in the prefab
            TextMeshProUGUI[] lobbyDetailsTexts = newLobbyInfo.GetComponentsInChildren<TextMeshProUGUI>();

            // Assign room name and player numbers to the correct text fields
            if (lobbyDetailsTexts.Length >= 2) // Ensure there are at least 2 text fields
            {
                lobbyDetailsTexts[0].text = _lobby.Name; // Set lobby name
                lobbyDetailsTexts[1].text = (_lobby.MaxPlayers - _lobby.AvailableSlots).ToString() + "/" + _lobby.MaxPlayers.ToString(); // Set player count
             
           

                
            }
          newLobbyInfo.GetComponentInChildren<Button>().onClick.AddListener(()=>JoinLobby(_lobby.Id));
        }
    }
    public async void JoinLobby(string _lobbyId)
    {
      
        //Player join the lobby by using LobbyId
        try
        {
            JoinLobbyByIdOptions options = new JoinLobbyByIdOptions()
            {
                Player = GetPlayer()
            };
           currentLobby= await LobbyService.Instance.JoinLobbyByIdAsync(_lobbyId,options);
            EnterRoom();
            Debug.Log("Players In Room : "+currentLobby.Players.Count);
        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    //Others Visibility
    private Player GetPlayer()
    {
        string playerName=LobbyBackend.Instance.PlayerName.ToString();
        if (playerName == null || playerName == " ")
            playerName = playerId;
        Player player = new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                //playerDataObject has to parameter one visibility and value of player name
                {"PlayerName",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,playerName) }
            }
        };
        return player;
    }

}
