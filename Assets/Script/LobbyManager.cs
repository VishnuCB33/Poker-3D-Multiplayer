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
using UnityEditor.UI;
//using static UnityEditor.Experimental.GraphView.GraphView;
public class LobbyManager : MonoBehaviour
{
    private string playerId;
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuPanel;
    // [SerializeField] private Button getLobbiesListButton;
    [SerializeField] private GameObject lobbyInfoPrefab;
    [SerializeField] private GameObject lobbiesInfoContent;
    //  [SerializeField] private TextMeshProUGUI roomNameText;
   // [SerializeField] private TextMeshProUGUI playerNames;
    [Space(10)]
    [Header("Create Room panel")]
    [SerializeField] private GameObject createRoomPanel;
    [SerializeField] private TMP_InputField roomNameIF;
    [SerializeField] private TMP_InputField maxPlayerIF;
    //[SerializeField] private Button createRoomButt;
    [SerializeField] private GameObject listOfLobbyPanel;
    [SerializeField] private UnityEngine.UI.Toggle isPrivateToggle;

    [Header("Inside Room ")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI roomCode;
    [SerializeField] private GameObject playerInfoContent;
    [SerializeField] private GameObject playerInfoPrefab;
    [SerializeField] private Button leaveRoomButton;
     private Lobby currentLobby;
    [Header("JOinCodeLobby")]
    [SerializeField] private TMP_InputField roomCodeIF;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private GameObject JoinRoomPanel;


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
        leaveRoomButton.onClick.AddListener(LeaveRoom);
        joinRoomButton.onClick.AddListener(JoinLobbyWithCode);
        }

    // Update is called once per frame
    void Update()
    {
        //TimerHeartBeat
        HandleLobbyHeartBeat();
        HandleRoomUpdate();
    }
    //Lobby Creation(It is helpful to seee player names details through the parameter
    public  async void CreateLobby()
    {
        try
        {
            string lobbbyName = roomNameIF.text;
            //maxplayer inputfield Output to integer
            int.TryParse(maxPlayerIF.text, out int maxPlayers);

            CreateLobbyOptions options = new CreateLobbyOptions
            {
                //Private public(toggle) , we create private public in these 1 line
                IsPrivate = isPrivateToggle.isOn,
                //in GetPlayer contain the information of current player
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
        //Wwe are taking each player from current Player.player and printing name
        foreach(Player player  in currentLobby.Players)
        {
            Debug.Log("Player Name : " + player.Data["PlayerName"].Value);
          //  playerNames.text = player.Data["PlayerName"].Value;
        }
        VisualizeRoomDetails();
    }
    //this function is display the early joining(room Update timer)
    private float roomUpdateTimer = 2f;
    private async void HandleRoomUpdate()
    {
        roomUpdateTimer-=Time.deltaTime;
        if (currentLobby != null)
        {
            if (roomUpdateTimer <= 0)
            {
                roomUpdateTimer = 2f;
                try
                {
                    if (IsinLobby())
                    {
                        //We need to send Id of Lobby for Which lobby we want to get inside GEtLobbyAsync function
                        currentLobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
                        VisualizeRoomDetails();
                    }
                   
                }
                catch (LobbyServiceException e)
                {
                    Debug.Log(e);
                }
            }
        }
        
       
    }
    //Check this function to player is in the lobby or not
    private bool IsinLobby()
    {
       //Now check id the player ID matches the any player ID in Lobby
       foreach(Player _player in currentLobby.Players)
        {
            if (_player.Id == playerId)
            {
                return true;
            }
            
        }
        currentLobby = null;
        return false;
        //Now we shall call this function before calling getLobbyAsync
    }
  public void RoomBackButt()
    {
        roomPanel.SetActive(false);
        createRoomPanel.SetActive(true);
    }
    public void JoinRoomCodePanel()
    {
        JoinRoomPanel.SetActive(true);
        createRoomPanel.SetActive(false);
    }
    public void BackJoinRoomCodePanel()
    {
        JoinRoomPanel.SetActive(false);
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
    //JoinLobby With code
    public async void JoinLobbyWithCode()
    {

        //player code inputField
        string lobbyCode = roomCodeIF.text;
        try
        {
            //joinLobbyById change to joinLobbyByCode
            JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions()
            {
                Player = GetPlayer()
            };
            //in public lobby player enterd by id and join code player enter with code
            currentLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, options);
            EnterRoom();
            Debug.Log("Players In Room : " + currentLobby.Players.Count);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    //Others Visibility  Remain
    private Player GetPlayer()
    {
        string playerName=LobbyBackend.Instance.GetPlayerName();
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
    //this function helps to instantiate the player name text butt of the prefab
    private void VisualizeRoomDetails()
    {

        //Previous player info delete
        for(int i=0;i<playerInfoContent.transform.childCount;i++)
        {
            Destroy(playerInfoContent.transform.GetChild(i).gameObject);
        }
        //Check player is in room before accessing the current lobby
        if (IsinLobby())
        {
            //EnterRoom function loop,this is the loop for who is entering the room 
            foreach (Player player in currentLobby.Players)
            {
                GameObject newPlayerInfo = Instantiate(playerInfoPrefab, playerInfoContent.transform);
                newPlayerInfo.GetComponentInChildren<TextMeshProUGUI>().text = player.Data["PlayerName"].Value;
                if (isHost()&& player.Id != playerId)
                {
                    //if is Host only kick the player(the refernce taken by the playerinfo children),in this player.id!=playerId is helps to not seen the kick button in host player(own himself)
                    Button kickBtn=newPlayerInfo.GetComponentInChildren<Button>(true);

                 kickBtn.onClick.AddListener(() => KickPlayer(playerId));
                    kickBtn.gameObject.SetActive(true);
                }
               
            }

        }
        else
        {
            ExitRoomButt();
        }


    }
    //Leave Room Function
    private async void LeaveRoom()
    {
        try
        {
            //We need to call remove player async method for removing player from the lobby so we need to pass Lobby ID and player ID as Input to remove lobby
            await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id,playerId);
            ExitRoomButt();

        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    //Kick Button
    private async void KickPlayer(string _playerId)
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, playerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
    public void ExitRoomButt()
    {
        createRoomPanel.SetActive(true);
        roomPanel.SetActive(false);
    }
}
