using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game_Basic_Code : MonoBehaviour
{
    [Header("player one")]
    public int player_num_backend_1;
    public string player_name_backend_1;
    public int amount_backend_1;
    public float Time1=10f;

    [Header("player two")]
    public int player_num_backend_2;
    public string player_name_backend_2;
    public int amount_backend_2;
    public float Time2=10f;

    [Header("player three")]
    public int player_num_backend_3;
    public string player_name_backend_3;
    public int amount_backend_3;
    public float Time3=10f;

    [Header("player four")]
    public int player_num_backend_4;
    public string player_name_backend_4;
    public int amount_backend_4;
    public float Time4=10f;

    public List<GameObject> Cards = new List<GameObject>();
    public players players = new players();
   
    public player_details player_details = new player_details();
  
    public int Player_turn;
    [Header("UI")]

    public TextMeshProUGUI player_name_1;
    public TextMeshProUGUI player_amount_1;
    public TextMeshProUGUI player_name_2;
    public TextMeshProUGUI player_amount_2;
    public TextMeshProUGUI player_name_3;
    public TextMeshProUGUI player_amount_3;
    public TextMeshProUGUI player_name_4;
    public TextMeshProUGUI player_amount_4;
    public Slider player_time_1;
    public Slider player_time_2;
    public Slider player_time_3;
    public Slider player_time_4;
    
    
    public void Update()
    {
        player_Details_funtion();
        time_turn_funtion();
        clock();
    }
    public void clock()
    {
        player_time_1.value = Time1;
        player_time_2.value = Time2;
        player_time_3.value = Time3;
        player_time_4.value = Time4;
    }
    public void time_turn_funtion()
    {
        if(Time1<0)
        {
            Player_turn = 2;
            Time4 = 10;
        }
        if(Time2<0)
        {
            Player_turn = 3;
            Time1 = 10;
        }
        if(Time3<0)
        {
            Player_turn = 4;
            Time2 = 10;
        }
        if(Time4<0)
        {
            Player_turn = 1;
            Time3 = 10;
        }
    }
    public void player_Details_funtion()
    {
        if(Player_turn==1&&Time1>0)
        {
            players = players.Player_One;
            player_details.player_name = player_name_backend_1;
            player_details.player_num = player_num_backend_1;
            player_details.amount = amount_backend_1;
            player_name_1.text = player_name_backend_1;
            player_amount_1.text = amount_backend_1.ToString();
            Time1 -= Time.deltaTime * 1;
        }
        if(Player_turn==2 && Time2 > 0)
        {
            players = players.Player_One;
            player_details.player_name = player_name_backend_2;
            player_details.player_num = player_num_backend_2;
            player_details.amount = amount_backend_2;
            player_name_2.text = player_name_backend_2;
            player_amount_2.text = amount_backend_2.ToString();
            Time2 -= Time.deltaTime * 1;

        }
        if (Player_turn==3 && Time3 > 0)
        {
            players = players.Player_One;
            player_details.player_name = player_name_backend_3;
            player_details.player_num = player_num_backend_3;
            player_details.amount = amount_backend_3;
            player_name_3.text = player_name_backend_3;
            player_amount_3.text = amount_backend_3.ToString();
            Time3 -= Time.deltaTime * 1;

        }
        if (Player_turn==4 && Time4 > 0)
        {
            players = players.Player_One;
            player_details.player_name = player_name_backend_4;
            player_details.player_num = player_num_backend_4;
            player_details.amount = amount_backend_4;
            player_name_4.text = player_name_backend_4;
            player_amount_4.text = amount_backend_4.ToString();
            Time4 -= Time.deltaTime * 1;

        }


    }
}
[System.Serializable]
public enum players
{
   Player_One,Player_Two,Player_Three,Player_Four

}
[System.Serializable]
public class player_details
{
    public role player_role = new role();
    public setected player_setected = new setected();
    [Header("player details")]
    public int player_num;
    public string player_name;
    public int amount;
    public enum setected
    {
        Fold, Check, Raise, 
        
    }public enum role
    {
        Dealer, Small_Blind, Big_Blind, 
        
    }
   
}
