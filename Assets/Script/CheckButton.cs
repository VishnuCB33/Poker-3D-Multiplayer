using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckButton : MonoBehaviour
{
    public static CheckButton Instance;
    [Header("Win")]
    public bool[] winnerlist;

    [Header("Card Comparison")]
    //V
    public List<GameObject> playerOneCard;
    public List<GameObject> playerTwoCard;
    public List<GameObject> playerThreeCard;
    public List<GameObject> playerFourCard;
   
    public List<GameObject> finalCheckFiveCard;
   /* [Header("Card Series")]

    public List<GameObject> clubs;
    public List<GameObject> diamonds;
    public List<GameObject> hearts;
    public List<GameObject> spades;*/
    [Header("Royal Flush")]
    public List<GameObject> clubsRoyalFlush;

    [SerializeField] private int changeCards;
    [SerializeField] private int count;
    [SerializeField] private List<GameObject> duplicateCheck;
    [Header("royalFlushVar Variables")]
    int checkClubs = 0;
    [SerializeField] private List<GameObject> royalFlushMerge;
    int royalFleshClubCount;
    int royalFleshDiamondCount;
    int royalFleshHeartCount;
    int royalFleshSpadesCount;
    //PAir Values
    int pairCounts;
    [SerializeField]private List<GameObject> duplicatePair=new List<GameObject>();
    //straight
    [SerializeField]
    private List<GameObject> straightCheck;
    [SerializeField] int temp;
    [SerializeField] List<int> values;
    private int flushChecker = 0;
    private int twoPairChecker = 0;
    [Header("Checking Final")]
    public int royalFlushVar = 0;
    public int straightFlushVar = 0;
    public int fourOFaKindVar = 0;
    public int fullHouseVar = 0;
    public int flushVar = 0;
    public int straightVar = 0;
    public int threeOfaKindVar = 0;
    public int twoPairVar = 0;
    public int pairVar = 0;
    public int highCardVar = 0;
    public List<int>player2Win=new List<int>(10);
    public List<int> player3Win = new List<int>(10);
    public List<int> player4Win = new List<int>(10);

    /*  [Header("IdentifyWinsCount")]
      public List<int>winnerCount = new List<int>();
      int countWinner = 0;*/
    private void Start()
    {
       Instance = this;
    }
    private void Update()
    {

       
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            RoyalFlush();
        }*/


      /*  if (Input.GetKeyDown(KeyCode.Space))//A
        {
            flush();
            Four_of_a_kind();
            Three_of_a_kind(); 
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Pair();
            TwoPair();  
        }*/


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

       /* if (Input.GetKeyDown(KeyCode.D))
        {
            Straight();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StraightFlush();
        }*/

    }
    public void ResetFlush()
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
                    //player1
                   royalFlushVar = 10;
                   player2Win[0] = 10;
                    winnerlist[0] = true;
                 player3Win[0] = 10;
                    player4Win[0] = 10;


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
            flushVar = 6;
           flushChecker = 1;
            x = 0;
           player3Win[4] = 6;
            player3Win[4] = 6;
            player4Win[4] = 6;

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
            fourOFaKindVar = 8;
            winnerlist[2] = true;
           player2Win[2] = 8;
            player3Win[2] = 8;
            player4Win[2] = 8;
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
           threeOfaKindVar = 4;
            player2Win[6] = 4;
            player3Win[6] = 4;
            player4Win[6] = 4;

        }
        else 
        {
            Debug.Log("not have three_of_a_kind");
        }

    }
    public void Pair()
    {
        // A dictionary to track card ranks and their counts
        Dictionary<string, int> cardRankCounts = new Dictionary<string, int>();
        pairCounts = 0; // Reset pair count
        duplicatePair.Clear(); // Clear the previous pairs

        // Count the occurrences of each card rank
        foreach (var card in finalCheckFiveCard)
        {
            string rank = card.tag; // Assuming 'tag' represents the card rank (e.g., "2", "3", "A")
            if (cardRankCounts.ContainsKey(rank))
            {
                cardRankCounts[rank]++;
            }
            else
            {
                cardRankCounts[rank] = 1;
            }
        }

        // Check for pairs
        foreach (var entry in cardRankCounts)
        {
            if (entry.Value == 2) // A pair is exactly two cards of the same rank
            {
                pairCounts++;

                // Add the duplicate cards to the duplicatePair list
                foreach (var card in finalCheckFiveCard)
                {
                    if (card.tag == entry.Key)
                    {
                        duplicatePair.Add(card);
                    }
                }
            }
        }

        // Check if there is at least one pair
        if (pairCounts > 0)
        {
            //Debug.Log($"Found {pairCounts} pair(royalFlushVar)!");
            Debug.Log("PAIR.....");
            pairVar = 2; // Indicate pair logic or scoring
            winnerlist[8] = true;
          
            player2Win[8] = 2;
            player3Win[8] = 2;
            player4Win[8] = 2;
        }
        else
        {
            Debug.Log("No pairs found.");
        }
    }

    public void TwoPair()
    {
        // Dictionary to count occurrences of each card rank
        Dictionary<string, int> cardRankCounts = new Dictionary<string, int>();
        pairCounts = 0; // Reset pair count
        duplicatePair.Clear(); // Clear the previous pairs

        // Count occurrences of each card rank
        foreach (var card in finalCheckFiveCard)
        {
            string rank = card.tag; // Assuming 'tag' represents the card rank
            if (cardRankCounts.ContainsKey(rank))
            {
                cardRankCounts[rank]++;
            }
            else
            {
                cardRankCounts[rank] = 1;
            }
        }

        // Identify pairs
        foreach (var entry in cardRankCounts)
        {
            if (entry.Value == 2) // If exactly two cards of the same rank exist
            {
                pairCounts++;

                // Add cards of this rank to duplicatePair
                foreach (var card in finalCheckFiveCard)
                {
                    if (card.tag == entry.Key)
                    {
                        duplicatePair.Add(card);
                    }
                }
            }
        }

        // Check if there are exactly two pairs
        if (pairCounts == 2)
        {
            Debug.Log("Two Pair.....");
            winnerlist[7] = true; // Example: Mark the winner or state
           twoPairVar = 3; // Indicate logic or scoring for Two Pair
            player2Win[7] = 3;
            player3Win[7] = 3;
            player4Win[7] = 3;
        }
        else
        {
            Debug.Log("Not Two Pair.");
        }
    }

    public void Straight()
    {

       
        int y = 0;
       for(int i = 0;i< finalCheckFiveCard.Count; i++)
        {
            values[y] = finalCheckFiveCard[i].GetComponent<CardsAttached>().properties.value;
            y++;

        }
      
      values.Sort();
      int check1 = values[0] - values[1];
        int check2=values[1] - values[2];
        int check3=values[2] - values[3];
      int check4=values[3] - values[4];

        if(check1==-1)
        {
            Debug.Log("1stFAce....");
            if(check2==-1)
            {
                Debug.Log("2ndFAce");
                if (check3==-1)
                {
                    Debug.Log("3ndFAce");
                    if (check4 == -1)
                    {
                        Debug.Log("Straight.....");
                        winnerlist[5] = true;
                        straightVar = 5;
                        player2Win[5] = 5;
                        player3Win[5] = 5;
                        player4Win[5] = 5;


                    }
                }
            }
        }
    }
    public void StraightFlush()
    {
        if (flushChecker == 1)
        {
            Straight();
            straightFlushVar = 9;
            Debug.Log("StraightFlush...");
            winnerlist[1] = true;
         player2Win[1] = 9;
            player3Win[1] = 9;
            player4Win[1] = 9;

        }
    }
    public void FullHouse()
    {
        // Dictionary to count occurrences of each card rank
        Dictionary<string, int> cardRankCounts = new Dictionary<string, int>();
        int threeOfAKindCount = 0; // Counter for Three of a Kind
        int pairCount = 0; // Counter for Pairs
        duplicatePair.Clear(); // Clear previous duplicates

        // Count occurrences of each card rank
        foreach (var card in finalCheckFiveCard)
        {
            string rank = card.tag; // Assuming 'tag' represents the card rank
            if (cardRankCounts.ContainsKey(rank))
            {
                cardRankCounts[rank]++;
            }
            else
            {
                cardRankCounts[rank] = 1;
            }
        }

        // Determine if there is a Three of a Kind and a Pair
        foreach (var entry in cardRankCounts)
        {
            if (entry.Value == 3) // Check for Three of a Kind
            {
                threeOfAKindCount++;
                // Add cards of this rank to duplicatePair
                foreach (var card in finalCheckFiveCard)
                {
                    if (card.tag == entry.Key)
                    {
                        duplicatePair.Add(card);
                    }
                }
            }
            else if (entry.Value == 2) // Check for Pair
            {
                pairCount++;
                // Add cards of this rank to duplicatePair
                foreach (var card in finalCheckFiveCard)
                {
                    if (card.tag == entry.Key)
                    {
                        duplicatePair.Add(card);
                    }
                }
            }
        }

        // Check if there is one Three of a Kind and one Pair
        if (threeOfAKindCount == 1 && pairCount == 1)
        {
            Debug.Log("Full House!");
            winnerlist[3] = true; // Example: Mark the winner or state
            fullHouseVar = 7; // Indicate logic or scoring for Full House
            player2Win[3] = 7;
            player3Win[3] = 7;
            player4Win[3] = 7; 
        }
        else
        {
            Debug.Log("Not a Full House.");
        }
    }

}
