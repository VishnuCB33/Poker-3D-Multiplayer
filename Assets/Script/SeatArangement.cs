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
        
    }

    // Update is called once per frame
    void Update()
    {
        ThePlayerName = LobbyBackend.Instance.PlayerName.text;


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
       /* if (ThePlayerIndex == 2)
        {
            Game_Basic_Code.instance.player_name_backend_1 = ThePlayerName;

        }
        if (ThePlayerIndex == 3)
        {
            Game_Basic_Code.instance.player_name_backend_1 = ThePlayerName;

        }*/
    }
}
