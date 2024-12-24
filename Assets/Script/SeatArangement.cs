using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatArangement : MonoBehaviour
{
    public string ThePlayerName;
    public int ThePlayerAmount;
    public int ThePlayerIndex;
    // Start is called before the first frame update
    void Start()
    {
        nullname();
        
    }

    // Update is called once per frame
    void Update()
    {
        ThePlayerName = LobbyBackend.Instance.PlayerName.text;

        SeatArangements();
        nullname();
    }
    public void nullname()
    {
        if (LobbyManager.Instance.playersNames[0] == null)
        {
            LobbyManager.Instance.playersNames[0] = "unknown";

        }
        if (LobbyManager.Instance.playersNames[1] == null)
        {
            LobbyManager.Instance.playersNames[1] = "unknown";


        }
        if (LobbyManager.Instance.playersNames[2] == null)
        {
            LobbyManager.Instance.playersNames[2] = "unknown";

        }
        if (LobbyManager.Instance.playersNames[3] == null)
        {
            LobbyManager.Instance.playersNames[3] = "unknown";

        }
        
    }
    public void SeatArangements()
    {
        if (ThePlayerName == LobbyManager.Instance.playersNames[0])
        {
            Game_Basic_Code.instance.player_name_backend_1 = LobbyManager.Instance.playersNames[0];
            Game_Basic_Code.instance.player_name_backend_2 = LobbyManager.Instance.playersNames[1];
            Game_Basic_Code.instance.player_name_backend_3 = LobbyManager.Instance.playersNames[2];
            Game_Basic_Code.instance.player_name_backend_4 = LobbyManager.Instance.playersNames[3];
        }
        if (ThePlayerName == LobbyManager.Instance.playersNames[1])
        {

            Game_Basic_Code.instance.player_name_backend_1 = LobbyManager.Instance.playersNames[1];
            Game_Basic_Code.instance.player_name_backend_2 = LobbyManager.Instance.playersNames[2];
            Game_Basic_Code.instance.player_name_backend_3 = LobbyManager.Instance.playersNames[3];
            Game_Basic_Code.instance.player_name_backend_4 = LobbyManager.Instance.playersNames[0];

        } 
        if (ThePlayerName == LobbyManager.Instance.playersNames[2])
        {

            Game_Basic_Code.instance.player_name_backend_1 = LobbyManager.Instance.playersNames[2];
            Game_Basic_Code.instance.player_name_backend_2 = LobbyManager.Instance.playersNames[3];
            Game_Basic_Code.instance.player_name_backend_3 = LobbyManager.Instance.playersNames[0];
            Game_Basic_Code.instance.player_name_backend_4 = LobbyManager.Instance.playersNames[1];

        }
        if (ThePlayerName == LobbyManager.Instance.playersNames[3])
        {

            Game_Basic_Code.instance.player_name_backend_1 = LobbyManager.Instance.playersNames[3];
            Game_Basic_Code.instance.player_name_backend_2 = LobbyManager.Instance.playersNames[0];
            Game_Basic_Code.instance.player_name_backend_3 = LobbyManager.Instance.playersNames[1];
            Game_Basic_Code.instance.player_name_backend_4 = LobbyManager.Instance.playersNames[2];

        }
      
    }
}
