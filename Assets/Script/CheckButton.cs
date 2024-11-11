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
    [Header("check Variables")]
    int checkClubs = 0;
    [SerializeField] private List<GameObject> royalFlushMerge;
    int royalFleshClubCount;
    int royalFleshDiamondCount;
    int royalFleshHeartCount;
    int royalFleshSpadesCount;
    //PAir Values
    int pairCounts;
   
    private void Start()
    {
       
    }
    private void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            RoyalFlush();
        }*/


        /*if (Input.GetKeyDown(KeyCode.Space))//A
        {
            flush();
            Four_of_a_kind();
            Three_of_a_kind();
        }
*/
        if (Input.GetKeyDown(KeyCode.S))
        {
            Pair();
        }


        if (Input.GetKeyDown(KeyCode.A))//A
        {
            flush_Diamonds_count = 0;
            flush_Club_count = 0;
            flush_Heart_count = 0;
            flush_Spades_count = 0;
            x = 0;
            y = 0;
            count_four_of_a_kind = 0;
            count_three_of_a_kind = 0;
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
                    royalFlushMerge.Add(merge[i]);
                        Debug.Log(countA);
                        break;
                   case CardSeries.K: isK = true;
                        countK++;
                    royalFlushMerge.Add(merge[i]);
                    Debug.Log(countK);
                        break;
                    case CardSeries.Q: isQ = true;
                        countQ++;
                    royalFlushMerge.Add(merge[i]);
                    Debug.Log(countQ);
                        break;
                    case CardSeries.J: isJ = true;
                        countJ++;
                    royalFlushMerge.Add(merge[i]);
                    Debug.Log(countJ);
                        break;
                    case CardSeries.Ten: isTen = true;
                        countTen++;
                    royalFlushMerge.Add(merge[i]);
                    Debug.Log(countTen);
                        break;
                         
                       
                }


            if (isA == true && isK == true && isQ == true && isJ == true && isTen == true)
            {
                Debug.Log("ppp");
                int o = 0;
                while (5 > o)
                {
                    if (royalFlushMerge[o].layer == 6)
                    {
                        royalFleshClubCount++;
                    }
                    if (royalFlushMerge[o].layer == 7)
                    {
                        royalFleshDiamondCount++;
                    }
                    if(royalFlushMerge[o].layer == 8)
                    {
                        royalFleshHeartCount++;
                    }
                   if (royalFlushMerge[0].layer == 7)
                    {
                        royalFleshSpadesCount++;   
                      
                    }
                    o++;
                }
               if(royalFleshSpadesCount >= 5 || royalFleshHeartCount >= 5 || royalFleshDiamondCount >= 5 || royalFleshClubCount >= 5)
                {
                    Debug.Log("RoyalFlush");
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
    public void Pair()
    {
        for(int i=0;i< finalCheckFiveCard.Count;i++)
        {
           for(int j = 1; i < finalCheckFiveCard.Count; j++)
            {
                if(finalCheckFiveCard[i].tag== finalCheckFiveCard[j].tag)
                {
                    pairCounts++;
                    Debug.Log(pairCounts);
                }
                   
            }
        }
    }

}
 