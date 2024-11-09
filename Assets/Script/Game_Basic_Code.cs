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
    public int Raise_amount_1;
    public int Raise_change_1=10;
 

    [Header("player two")]
    public int player_num_backend_2;
    public string player_name_backend_2;
    public int amount_backend_2;
    public float Time2=10f;
    public int Raise_amount_2;
   

    [Header("player three")]
    public int player_num_backend_3;
    public string player_name_backend_3;
    public int amount_backend_3;
    public float Time3=10f;
    public int Raise_amount_3;
 

    [Header("player four")]
    public int player_num_backend_4;
    public string player_name_backend_4;
    public int amount_backend_4;
    public float Time4=10f;
    public int Raise_amount_4;
  


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
    public GameObject raise_panel;
    public GameObject raise_button_object, fold_button_object, check_button_object,call_button_object;
    public TextMeshProUGUI raise_text;
    public TextMeshProUGUI raise_change_text;
    public Slider raise_slider;
    public TextMeshProUGUI Call_text;
    int call_amount_referance;

    public void Start()
    {
       Player_turn = 1;
       player_details.player_setected = player_details.setected.none;


    }
    public void Update()
    {
        player_Details_funtion();
        time_turn_funtion();
        clock();
        raise_function();
        display_player_details_alltime_funtion();
        call_funtion();
        fold_funtion();
      
    }
    public void fold_funtion()
    {
        if(player_details.player_setected==player_details.setected.Fold)
        {
            raise_button_object.SetActive(false);
            fold_button_object.SetActive(false);
            check_button_object.SetActive(false);
            call_button_object.SetActive(false);
            raise_panel.SetActive(false);
        }
        if(Player_turn==1&& player_details.player_setected == player_details.setected.Fold)
        {
            Player_turn = 2;
        }
    }
    public void display_player_details_alltime_funtion()
    {
        player_name_1.text = player_name_backend_1;
        player_amount_1.text = amount_backend_1.ToString();

        player_name_2.text = player_name_backend_2;
        player_amount_2.text = amount_backend_2.ToString();

        player_name_3.text = player_name_backend_3;
        player_amount_3.text = amount_backend_3.ToString();

        player_name_4.text = player_name_backend_4;
        player_amount_4.text = amount_backend_4.ToString();
    }
    public void call_funtion()
    {
        if ((Raise_amount_1 != 0 || Raise_amount_2 != 0 || Raise_amount_3 != 0 || Raise_amount_4 != 0)&& Player_turn == 1)
        {
            call_button_object.SetActive(true);
            if (Raise_amount_2 < Raise_amount_3 && Raise_amount_4 < Raise_amount_3)
            {
                Call_text.text = "CALL[" + Raise_amount_3 + "]";
                call_amount_referance = Raise_amount_3;
            }
            else if (Raise_amount_3 < Raise_amount_4 && Raise_amount_2 < Raise_amount_4)
            {

                Call_text.text = "CALL[" + Raise_amount_4 + "]";
                call_amount_referance = Raise_amount_4;

            }
            else
            {

                Call_text.text = "CALL[" + Raise_amount_2 + "]";
                call_amount_referance = Raise_amount_2;

            }

        }
        else 
        {

            call_button_object.SetActive(false);

        }
    }
    public void raise_function()
    {
        if(Player_turn==1)
        {
            raise_button_object.SetActive(true);
            fold_button_object.SetActive(true);
            check_button_object.SetActive(true);
         
        }
        else
        {
            raise_button_object.SetActive(false);
            fold_button_object.SetActive(false);
            check_button_object.SetActive(false);
            call_button_object.SetActive(false);
            raise_panel.SetActive(false);
        }
        raise_text.text = "Raise[" + Raise_amount_1 + "]";
        raise_change_text.text =  Raise_change_1.ToString();
        raise_slider.maxValue = amount_backend_1;
        raise_slider.value = Raise_amount_1;
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
        if(Time1<0&&Time2==10)
        {
            Player_turn = 2;
            Time4 = 10;
        }
        if(Time2<0 && Time3 == 10)
        {
            Player_turn = 3;
            Time1 = 10;
        }
        if(Time3<0 && Time4 == 10)
        {
            Player_turn = 4;
            Time2 = 10;
        }
        if(Time4<0 && Time1 == 10)
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
            player_details.raise_smount = Raise_amount_1;

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
            player_details.raise_smount = Raise_amount_2;

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
            player_details.raise_smount = Raise_amount_3;

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
            player_details.raise_smount = Raise_amount_4;

            player_name_4.text = player_name_backend_4;
            player_amount_4.text = amount_backend_4.ToString();
            Time4 -= Time.deltaTime * 1;

        }

    }
    //UI button function

    public void fold_button()
    {
        player_details.player_setected = player_details.setected.Fold;

    }
    public void raise_panel_button()
    {
        raise_panel.SetActive(true);
       
    }
    public void raise_button()
    {
        raise_panel.SetActive(false);
        Player_turn = 2;
        player_details.player_setected = player_details.setected.Raise;

        amount_backend_1 -= Raise_amount_1;
        Raise_amount_1 = 0;

    }
    public void raise_back_button()
    {
        raise_panel.SetActive(false);
        Raise_amount_1 = 0; 

    }
    public void raise_increase_button()
    {
        if (amount_backend_1+1> Raise_amount_1+ Raise_change_1)
        {
            Raise_amount_1 += Raise_change_1;
        }
      
    } 
    public void raise_decrease_button()
    {
        if(-1< Raise_amount_1 - Raise_change_1)
        {
            Raise_amount_1 -= Raise_change_1;
        }
        
    }
    public void raise_10_button()
    {
        Raise_change_1 = 10;
    }
   
    public void raise_50_button()
    {
        Raise_change_1 = 50;

    }
    public void raise_100_button()
    {
        Raise_change_1 = 100;

    }
    public void raise_500_button()
    {
        Raise_change_1 = 500;

    }
    public void raise_1000_button()
    {
        Raise_change_1 = 1000;

    }
    public void call_button()
    {
        amount_backend_1 -= call_amount_referance;
        
            Player_turn = 2;

        player_details.player_setected = player_details.setected.call;

    }
    //Card Spawn Random(Vishnu)


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
    public int raise_smount;
    public enum setected
    {
        none,Fold, Check, Raise,call 
        
    }public enum role
    {
        Dealer, Small_Blind, Big_Blind, 
        
    }
   
}

