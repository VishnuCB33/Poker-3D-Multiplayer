using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CheckButton : MonoBehaviour
{
    [Header("Win")]
    public bool[] winnerlist;

    [Header("Card Comparison")]
    //V
    public List<GameObject> playerOneCard;
    public List<GameObject> playerTwoCard;
    public List<GameObject> playerThreeCard;
    public List<GameObject> playerFourCard;
   
    public List<GameObject> finalCheckFiveCard;
    [Header("Card Series")]

    public List<GameObject> clubs;
    public List<GameObject> diamonds;
    public List<GameObject> hearts;
    public List<GameObject> spades;
    [Header("Royal Flush")]
    public List<GameObject> clubsRoyalFlush;

    [SerializeField] private int changeCards;
    [SerializeField] private int count;
    [SerializeField] private List<GameObject> duplicateCheck;
   

    private void Start()
    {
       
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RoyalFlush();
        }

      


    }
    public bool RoyalFlush()
    {
        bool isA = false;
        bool isK = false;
        bool isQ = false;
        bool isJ = false;
        bool isTen = false;
        int countA=0;
        int countK=0;
        int countQ=0;
        int countJ=0;
        int countTen = 0;
        List<GameObject>merge=finalCheckFiveCard.Concat(playerOneCard).ToList();
       for(int i=0;i<merge.Count;i++)
        {
           
                CardSeries check = merge[i].GetComponent<CardsAttached>().properties.allCards; 
                switch(check)
                {
                    case CardSeries.A: isA = true;
                        countA++;
                        Debug.Log(countA);
                        break;
                   case CardSeries.K: isK = true;
                        countK++;
                        Debug.Log(countK);
                        break;
                    case CardSeries.Q: isQ = true;
                        countQ++;
                        Debug.Log(countQ);
                        break;
                    case CardSeries.J: isJ = true;
                        countJ++;
                        Debug.Log(countJ);
                        break;
                    case CardSeries.Ten: isTen = true;
                        countTen++;
                        Debug.Log(countTen);
                        break;
                         
                       
                }


            if (isA == true && isK == true && isQ == true && isJ == true && isTen == true)
            {

                while (7 > x)
                {
                    if (finalCheckFiveCard[x].layer == 6)
                    {
                        flush_Club_count++;
                    }
                    if (finalCheckFiveCard[x].layer == 7)
                    {
                        flush_Diamonds_count++;
                    }
                    if (finalCheckFiveCard[x].layer == 8)
                    {
                        flush_Heart_count++;
                    }
                    if (finalCheckFiveCard[x].layer == 9)
                    {
                        flush_Spades_count++;
                    }

                    x++;
                }
                if (flush_Club_count >= 5 || flush_Diamonds_count >= 5 || flush_Heart_count >= 5 || flush_Spades_count >= 5)
                {
                    Debug.Log("player_one_have_flush");
                    winnerlist[4] = true;

                    x = 0;
                }
            }
        }
       
        return isA;

    }
   
    int x = 0; 
    int flush_Club_count, flush_Diamonds_count, flush_Heart_count, flush_Spades_count;
    public void flush()//A
    {

       
        while(7>x)
        {
            if (finalCheckFiveCard[x].layer== 6)
            {
             flush_Club_count++;
            }
            if(finalCheckFiveCard[x].layer == 7)
            {
                flush_Diamonds_count++;
            }
            if(finalCheckFiveCard[x].layer == 8)
            {
                flush_Heart_count++;
            }
            if(finalCheckFiveCard[x].layer == 9)
            {
                flush_Spades_count++;
            }
           
            x++;
        }
        if(flush_Club_count >= 5|| flush_Diamonds_count >= 5|| flush_Heart_count >= 5|| flush_Spades_count >= 5)
        {
            Debug.Log("player_one_have_flush");
            winnerlist[4] = true;

            x = 0;
        }
        else
        {
            Debug.Log("Dont_player_one_have_flush");
           
            x = 0;
        }
    }
    int y;
    public int count_four_of_a_kind;
    public void Four_of_a_kind()//A
    {
        Debug.Log("hi");
        x = 0;
        y = 0;
        while(7>x)
        {

            if(finalCheckFiveCard[x].tag== finalCheckFiveCard[y].tag)
            {
                count_four_of_a_kind++;
            }
            x++;
        }
        if(count_four_of_a_kind<4)
        {
            x = 0;
            y++;
            count_four_of_a_kind = 0;
        }
        else if(count_four_of_a_kind >= 4)
        {
            Debug.Log("player one have Four_of_a_kind");
            winnerlist[2] = true;
        }
        else 
        {
            Debug.Log("not have Four_of_a_kind");
        }

    }
    public int count_three_of_a_kind;

    public void Three_of_a_kind()//A
    {
        Debug.Log("hi");
        x = 0;
        y = 0;
        while(7>x)
        {

            if(finalCheckFiveCard[x].tag== finalCheckFiveCard[y].tag)
            {
                count_three_of_a_kind++;
            }
            x++;
        }
        if(count_three_of_a_kind < 3)
        {
            x = 0;
            y++;
            count_three_of_a_kind = 0;
        }
        else if(count_three_of_a_kind >= 3)
        {
            Debug.Log("player one have three_of_a_kind");
            winnerlist[6] = true;

        }
        else 
        {
            Debug.Log("not have three_of_a_kind");
        }

    }


}
 