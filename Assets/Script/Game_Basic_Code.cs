using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static player_details;

public class Game_Basic_Code : MonoBehaviour
{
    public static Game_Basic_Code instance;
    [Header("player one")]
    public int player_num_backend_1;
    public string player_name_backend_1;
    public int amount_backend_1;
    public float Time1 = 10f;
    public int Raise_amount_1;
    public int Raise_change_1 = 10;
    public P1_setected P1__setected = new P1_setected();

    public enum P1_setected
    {
        none, Fold, Check, Raise, call

    }


    [SerializeField] private List<GameObject> playerOneCards;
    [SerializeField] private List<int> player1GetNumCard;

    [Header("player two")]
    public int player_num_backend_2;
    public string player_name_backend_2;
    public int amount_backend_2;
    public float Time2 = 10f;
    public int Raise_amount_2;
    public P2_setected P2__setected = new P2_setected();

    public enum P2_setected
    {
        none, Fold, Check, Raise, call

    }
    [SerializeField] private List<GameObject> playerTwoCards;
    [SerializeField] private List<int> player2GetNumCard;

    [Header("player three")]
    public int player_num_backend_3;
    public string player_name_backend_3;
    public int amount_backend_3;
    public float Time3 = 10f;
    public int Raise_amount_3;
    public P3_setected P3__setected = new P3_setected();

    public enum P3_setected
    {
        none, Fold, Check, Raise, call

    }
    [SerializeField] private List<GameObject> playerThreeCards;
    [SerializeField] private List<int> player3GetNumCard;

    [Header("player four")]
    public int player_num_backend_4;
    public string player_name_backend_4;
    public int amount_backend_4;
    public float Time4 = 10f;
    public int Raise_amount_4;
    public P4_setected P4__setected = new P4_setected();

    public enum P4_setected
    {
        none, Fold, Check, Raise, call

    }
    [SerializeField] private List<GameObject> playerFourCards;
    [SerializeField] private List<int> player4GetNumCard;


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
    public GameObject raise_button_object, fold_button_object, check_button_object, call_button_object;
    public TextMeshProUGUI raise_text;
    public TextMeshProUGUI raise_change_text;
    public Slider raise_slider;
    public TextMeshProUGUI Call_text;
    int call_amount_referance;
    [Header("Dealer")]
    public List<int> dealerSelection = new List<int>() { 1, 2, 3, 4 };
    [SerializeField] private int dealer;
    [Header("CardAllocation")]
    public List<int> randomCards = new List<int>(8);
    public List<GameObject> randomCardsGameObject = new List<GameObject>();
    public List<Transform> cardPos;
    [Header("WinnerSelection")]
    int yes = 0;
    public List<int>winnerCount = new List<int>();
    public List<int> winnerSelectP1 = new List<int>();
    public List<int> winnerSelectP2 = new List<int>();
    public List<int> winnerSelectP3 = new List<int>();
    public List<int> winnerSelectP4 = new List<int>();
    int one = 0;
    int two = 0;
    int three = 0;
    int checkComplete = 0;
    private void Awake()
    {
        dealer = Random.Range(1, 5);
    }
    public void Start()
    {
        Player_turn = 1;
        player_details.player_setected = player_details.setected.none;
        round_refarance = 0;
        DealerSelect();
        AllCardsAllocationFirst();
        instance = this;

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
        EqualingEnum_funtion();
        Round_selection_funtion();
        if (Input.GetKeyDown(KeyCode.A))
        {
            round_1_funtion();
        }if (Input.GetKeyDown(KeyCode.S))
        {
            round_2_funtion();
        }if (Input.GetKeyDown(KeyCode.D))
        {
            round_3_funtion();
        }if (Input.GetKeyDown(KeyCode.F))
        {
            round_4_funtion();
        }
    }

    //V
    public void RandomCards()
    {


        while (randomCards.Count < 13)
        {
            int store = Random.Range(0, 52);


            if (!randomCards.Contains(store))
            {
                randomCards.Add(store);
                randomCardsGameObject.Add(Cards[store]);


            }
        }

    }
    //vishnu CB
    public void AllCardsAllocationFirst()
    {
        RandomCards();
        player1GetNumCard[0] = randomCards[0];
        player1GetNumCard[1] = randomCards[1];
        player2GetNumCard[0] = randomCards[2];
        player2GetNumCard[1] = randomCards[3];
        player3GetNumCard[0] = randomCards[4];
        player3GetNumCard[1] = randomCards[5];
        player4GetNumCard[0] = randomCards[6];
        player4GetNumCard[1] = randomCards[7];
        playerOneCards[0] = randomCardsGameObject[0];
        GameObject one = Instantiate(playerOneCards[0], cardPos[0].position, cardPos[0].transform.rotation);
        //one.transform.rotation = new Quaternion(0, 180, 0, 0);
        playerOneCards[1] = randomCardsGameObject[1];
        GameObject two = Instantiate(playerOneCards[1], cardPos[1].position, cardPos[1].transform.rotation);
        // two.transform.rotation = new Quaternion(0, 180, 0, 0);
        playerTwoCards[0] = randomCardsGameObject[2];
        Instantiate(playerTwoCards[0], cardPos[2].position, cardPos[2].transform.rotation);
        playerTwoCards[1] = randomCardsGameObject[3];
        Instantiate(playerTwoCards[1], cardPos[3].position, cardPos[3].transform.rotation);
        playerThreeCards[0] = randomCardsGameObject[4];
        Instantiate(playerThreeCards[0], cardPos[4].position, cardPos[4].transform.rotation);
        playerThreeCards[1] = randomCardsGameObject[5];
        Instantiate(playerThreeCards[1], cardPos[5].position, cardPos[5].transform.rotation);
        playerFourCards[0] = randomCardsGameObject[6];
        Instantiate(playerFourCards[0], cardPos[6].position, cardPos[6].transform.rotation);
        playerFourCards[1] = randomCardsGameObject[7];
        Instantiate(playerFourCards[1], cardPos[7].position, cardPos[7].transform.rotation);

    }
    int round_refarance = 0;// need to reset to 0 after this round
    public void Round_selection_funtion()
    {
        if (((P1__setected == P1_setected.Check || P1__setected == P1_setected.Fold) && (P2__setected == P2_setected.Check || P2__setected == P2_setected.Fold) && (P3__setected == P3_setected.Check || P3__setected == P3_setected.Fold) && (P4__setected == P4_setected.Check || P4__setected == P4_setected.Fold)) && round_refarance == 0)
        {
            round_1_funtion();
        }
        if (((P1__setected == P1_setected.Check || P1__setected == P1_setected.Fold) && (P2__setected == P2_setected.Check || P2__setected == P2_setected.Fold) && (P3__setected == P3_setected.Check || P3__setected == P3_setected.Fold) && (P4__setected == P4_setected.Check || P4__setected == P4_setected.Fold)) && round_refarance == 2)
        {
            round_2_funtion();
        }
        if (((P1__setected == P1_setected.Check || P1__setected == P1_setected.Fold) && (P2__setected == P2_setected.Check || P2__setected == P2_setected.Fold) && (P3__setected == P3_setected.Check || P3__setected == P3_setected.Fold) && (P4__setected == P4_setected.Check || P4__setected == P4_setected.Fold)) && round_refarance == 3)
        {
            round_3_funtion();
        }
        if (((P1__setected == P1_setected.Check || P1__setected == P1_setected.Fold) && (P2__setected == P2_setected.Check || P2__setected == P2_setected.Fold) && (P3__setected == P3_setected.Check || P3__setected == P3_setected.Fold) && (P4__setected == P4_setected.Check || P4__setected == P4_setected.Fold)) && round_refarance == 4)
        {
            round_4_funtion();
        }
    }
    public List<GameObject> referance = new List<GameObject>();
    public void round_1_funtion()
    {
        if (P1__setected != P1_setected.Fold) { P1__setected = P1_setected.none; }
        if (P2__setected != P2_setected.Fold) { P2__setected = P2_setected.none; }
        if (P3__setected != P3_setected.Fold) { P3__setected = P3_setected.none; }
        if (P4__setected != P4_setected.Fold) { P4__setected = P4_setected.none; }
        randomCardsGameObject[8].transform.localScale = new Vector3(2, 2, 2);
        randomCardsGameObject[9].transform.localScale = new Vector3(2, 2, 2);
        randomCardsGameObject[10].transform.localScale = new Vector3(2, 2, 2);
        randomCardsGameObject[11].transform.localScale = new Vector3(2, 2, 2);
        randomCardsGameObject[12].transform.localScale = new Vector3(2, 2, 2);
        Instantiate(randomCardsGameObject[8], referance[0].transform.position, referance[0].transform.rotation);
        CheckButton.Instance.finalCheckFiveCard[0]=randomCardsGameObject[8];
        Instantiate(randomCardsGameObject[9], referance[1].transform.position, referance[1].transform.rotation);
        CheckButton.Instance.finalCheckFiveCard[1] = randomCardsGameObject[9];
        Instantiate(randomCardsGameObject[10], referance[2].transform.position, referance[2].transform.rotation);
        CheckButton.Instance.finalCheckFiveCard[2] = randomCardsGameObject[10];
        round_refarance = 2;

        Debug.Log("R1");
    }
    public void round_2_funtion()
    {
        Debug.Log("R2");
        if (P1__setected != P1_setected.Fold) { P1__setected = P1_setected.none; }
        if (P2__setected != P2_setected.Fold) { P2__setected = P2_setected.none; }
        if (P3__setected != P3_setected.Fold) { P3__setected = P3_setected.none; }
        if (P4__setected != P4_setected.Fold) { P4__setected = P4_setected.none; }
        round_refarance = 3;
        Instantiate(randomCardsGameObject[11], referance[3].transform.position, referance[3].transform.rotation);
        CheckButton.Instance.finalCheckFiveCard[3] = randomCardsGameObject[11];

    }
    public void round_3_funtion()
    {
        Debug.Log("R3");
        if (P1__setected != P1_setected.Fold) { P1__setected = P1_setected.none; }
        if (P2__setected != P2_setected.Fold) { P2__setected = P2_setected.none; }
        if (P3__setected != P3_setected.Fold) { P3__setected = P3_setected.none; }
        if (P4__setected != P4_setected.Fold) { P4__setected = P4_setected.none; }
        round_refarance = 4;

        Instantiate(randomCardsGameObject[12], referance[4].transform.position, referance[4].transform.rotation);
        CheckButton.Instance.finalCheckFiveCard[4] = randomCardsGameObject[12];
    }
   
    
    public void round_4_funtion()
    {
        Debug.Log("R4");
        if (P1__setected != P1_setected.Fold) { P1__setected = P1_setected.none; }
        if (P2__setected != P2_setected.Fold) { P2__setected = P2_setected.none; }
        if (P3__setected != P3_setected.Fold) { P3__setected = P3_setected.none; }
        if (P4__setected != P4_setected.Fold) { P4__setected = P4_setected.none; }
        round_refarance = 5;

        if (P1__setected != P1_setected.Fold)
        {
          
           // CheckButton.Instance.ResetFlush();
            CheckButton.Instance.finalCheckFiveCard[5] = playerOneCards[0];
            CheckButton.Instance.finalCheckFiveCard[6] = playerOneCards[1];

            CheckButton.Instance.RoyalFlush();
            winnerSelectP1.Add(CheckButton.Instance.royalFlushVar);

            CheckButton.Instance.StraightFlush();
            winnerSelectP1.Add(CheckButton.Instance.straightFlushVar);
            CheckButton.Instance.Four_of_a_kind();
            winnerSelectP1.Add(CheckButton.Instance.fourOFaKindVar);
            CheckButton.Instance.flush();
            winnerSelectP1.Add(CheckButton.Instance.flushVar);
            CheckButton.Instance.Straight();
            winnerSelectP1.Add(CheckButton.Instance.straightVar);
            CheckButton.Instance.Three_of_a_kind();
            winnerSelectP1.Add(CheckButton.Instance.threeOfaKindVar);
            CheckButton.Instance.TwoPair();
            winnerSelectP1.Add(CheckButton.Instance.twoPairVar);
            CheckButton.Instance.Pair();
            winnerSelectP1.Add(CheckButton.Instance.pairVar);
            CheckButton.Instance.FullHouse();
            winnerSelectP1.Add(CheckButton.Instance.fullHouseVar);
            CheckButton.Instance.royalFlushVar = 0;
            CheckButton.Instance.straightFlushVar = 0;
            CheckButton.Instance.fourOFaKindVar = 0;
            CheckButton.Instance.flushVar = 0;
            CheckButton.Instance.straightVar = 0;
            CheckButton.Instance.threeOfaKindVar = 0;
            CheckButton.Instance.twoPairVar = 0;
            CheckButton.Instance.pairVar = 0;
            CheckButton.Instance.fullHouseVar = 0;
            Debug.Log("wtf");
         //checking final
            playerOneCards[2] = CheckButton.Instance.finalCheckFiveCard[0];
            playerOneCards[3] = CheckButton.Instance.finalCheckFiveCard[1];
            playerOneCards[4] = CheckButton.Instance.finalCheckFiveCard[2];
            playerOneCards[5] = CheckButton.Instance.finalCheckFiveCard[3];
            playerOneCards[6] = CheckButton.Instance.finalCheckFiveCard[4];
            winnerSelectP1.Sort();
            checkComplete = 1;
        }
        /*if (checkComplete == 1)
        {
            if (one == 0)
            {
                winnerSelectP1[0] = CheckButton.Instance.royalFlushVar;
                winnerSelectP1[1] = CheckButton.Instance.straightFlushVar;
                winnerSelectP1[2] = CheckButton.Instance.fourOFaKindVar;
                winnerSelectP1[3] = CheckButton.Instance.flushVar;
                winnerSelectP1[4] = CheckButton.Instance.straightVar;
                winnerSelectP1[5] = CheckButton.Instance.threeOfaKindVar;
                winnerSelectP1[6] = CheckButton.Instance.twoPairVar;
                winnerSelectP1[7] = CheckButton.Instance.pairVar;
                winnerSelectP1[8] = CheckButton.Instance.fullHouseVar;
                one = 1;
            }
             
               

            CheckButton.Instance.royalFlushVar=0;
           CheckButton.Instance.straightFlushVar = 0;
            CheckButton.Instance.fourOFaKindVar = 0;
          CheckButton.Instance.flushVar = 0;
           CheckButton.Instance.straightVar=0;
             CheckButton.Instance.threeOfaKindVar = 0;
           CheckButton.Instance.twoPairVar = 0;
         CheckButton.Instance.pairVar = 0;
            CheckButton.Instance.fullHouseVar = 0;  
            checkComplete = 2;

        }*/
        if(checkComplete == 2)
        {
            if (P2__setected != P2_setected.Fold)
            {

                CheckButton.Instance.ResetFlush();
                CheckButton.Instance.finalCheckFiveCard[5] = playerOneCards[0];
                CheckButton.Instance.finalCheckFiveCard[6] = playerOneCards[1];

                CheckButton.Instance.RoyalFlush();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[0]);
                // CheckButton.Instance.player2Win[0] = 10;

                CheckButton.Instance.StraightFlush();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[1]);
                // CheckButton.Instance.player2Win[1] = 9;
                CheckButton.Instance.Four_of_a_kind();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[2]);
                //CheckButton.Instance.player2Win[2] = 8;
                CheckButton.Instance.flush();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[4]);
                //CheckButton.Instance.player2Win[4] = 6;
                CheckButton.Instance.Straight();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[5]);
                //CheckButton.Instance.player2Win[5] = 5;
                CheckButton.Instance.Three_of_a_kind();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[6]);
                //  CheckButton.Instance.player2Win[6] = 4;

                CheckButton.Instance.TwoPair();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[7]);
                //  CheckButton.Instance.player2Win[7] = 3;
                CheckButton.Instance.Pair();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[8]);
                //   CheckButton.Instance.player2Win[8] = 2;
                CheckButton.Instance.FullHouse();
                winnerSelectP2.Add(CheckButton.Instance.player2Win[3]);
                //  CheckButton.Instance.player2Win[3] = 7;
                CheckButton.Instance.royalFlushVar = 0;
                CheckButton.Instance.straightFlushVar = 0;
                CheckButton.Instance.fourOFaKindVar = 0;
                CheckButton.Instance.flushVar = 0;
                CheckButton.Instance.straightVar = 0;
                CheckButton.Instance.threeOfaKindVar = 0;
                CheckButton.Instance.twoPairVar = 0;
                CheckButton.Instance.pairVar = 0;
                CheckButton.Instance.fullHouseVar = 0;
                playerTwoCards[2] = CheckButton.Instance.finalCheckFiveCard[0];
                playerTwoCards[3] = CheckButton.Instance.finalCheckFiveCard[1];
                playerTwoCards[4] = CheckButton.Instance.finalCheckFiveCard[2];
                playerTwoCards[5] = CheckButton.Instance.finalCheckFiveCard[3];
                playerTwoCards[6] = CheckButton.Instance.finalCheckFiveCard[4];
                checkComplete = 3;
                Debug.Log("wtf");

            }
        }
       /* if (checkComplete == 3)
        {
            if (two == 0)
            {
                winnerSelectP2[0] = CheckButton.Instance.royalFlushVar;
                winnerSelectP2[1] = CheckButton.Instance.straightFlushVar;
                winnerSelectP2[2] = CheckButton.Instance.fourOFaKindVar;
                winnerSelectP2[3] = CheckButton.Instance.flushVar;
                winnerSelectP2[4] = CheckButton.Instance.straightVar;
                winnerSelectP2[5] = CheckButton.Instance.threeOfaKindVar;
                winnerSelectP2[6] = CheckButton.Instance.twoPairVar;
                winnerSelectP2[7] = CheckButton.Instance.pairVar;
                winnerSelectP2[8] = CheckButton.Instance.fullHouseVar;
                two = 1;
            }





            CheckButton.Instance.royalFlushVar = 0;
            CheckButton.Instance.straightFlushVar = 0;
            CheckButton.Instance.fourOFaKindVar = 0;
            CheckButton.Instance.flushVar = 0;
            CheckButton.Instance.straightVar = 0;
            CheckButton.Instance.threeOfaKindVar = 0;
            CheckButton.Instance.twoPairVar = 0;
            CheckButton.Instance.pairVar = 0;
            CheckButton.Instance.fullHouseVar = 0;
            checkComplete = 4;
        }*/
        if (checkComplete == 4)
        {
            if (P3__setected != P3_setected.Fold)
            {


                CheckButton.Instance.ResetFlush();
                CheckButton.Instance.finalCheckFiveCard[5] = playerOneCards[0];
                CheckButton.Instance.finalCheckFiveCard[6] = playerOneCards[1];

                CheckButton.Instance.RoyalFlush();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[0]);
                //CheckButton.Instance.player3Win[0] = 10;
                CheckButton.Instance.StraightFlush();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[1]);
                // CheckButton.Instance.player2Win[1] = 9;
                CheckButton.Instance.Four_of_a_kind();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[2]);
                // CheckButton.Instance.player2Win[2] = 8;
                CheckButton.Instance.flush();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[4]);
                //   CheckButton.Instance.player2Win[4] = 6;
                CheckButton.Instance.Straight();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[5]);
                // CheckButton.Instance.player2Win[5] = 5;
                CheckButton.Instance.Three_of_a_kind();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[6]);
                //CheckButton.Instance.player2Win[6] = 4;

                CheckButton.Instance.TwoPair();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[7]);
                // CheckButton.Instance.player2Win[7] = 3;
                CheckButton.Instance.Pair();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[8]);
                //CheckButton.Instance.player2Win[8] = 2;
                CheckButton.Instance.FullHouse();
                winnerSelectP3.Add(CheckButton.Instance.player3Win[3]);
                //CheckButton.Instance.player2Win[3] = 7; CheckButton.Instance.royalFlushVar = 0;
                CheckButton.Instance.straightFlushVar = 0;
                CheckButton.Instance.fourOFaKindVar = 0;
                CheckButton.Instance.flushVar = 0;
                CheckButton.Instance.straightVar = 0;
                CheckButton.Instance.threeOfaKindVar = 0;
                CheckButton.Instance.twoPairVar = 0;
                CheckButton.Instance.pairVar = 0;
                CheckButton.Instance.fullHouseVar = 0;

                playerThreeCards[2] = CheckButton.Instance.finalCheckFiveCard[0];
                playerThreeCards[3] = CheckButton.Instance.finalCheckFiveCard[1];
                playerThreeCards[4] = CheckButton.Instance.finalCheckFiveCard[2];
                playerThreeCards[5] = CheckButton.Instance.finalCheckFiveCard[3];
                playerThreeCards[6] = CheckButton.Instance.finalCheckFiveCard[4];
                checkComplete = 5;
                Debug.Log("wtf");
            }
        }
       /* if (checkComplete == 5)
        {
            if (three == 0)
            {
                winnerSelectP3[0] = CheckButton.Instance.royalFlushVar;
                winnerSelectP3[1] = CheckButton.Instance.straightFlushVar;
                winnerSelectP3[2] = CheckButton.Instance.fourOFaKindVar;
                winnerSelectP3[3] = CheckButton.Instance.flushVar;
                winnerSelectP3[4] = CheckButton.Instance.straightVar;
                winnerSelectP3[5] = CheckButton.Instance.threeOfaKindVar;
                winnerSelectP3[6] = CheckButton.Instance.twoPairVar;
                winnerSelectP3[7] = CheckButton.Instance.pairVar;
                winnerSelectP3[8] = CheckButton.Instance.fullHouseVar;
                three = 1;

            }





            CheckButton.Instance.royalFlushVar = 0;
            CheckButton.Instance.straightFlushVar = 0;
            CheckButton.Instance.fourOFaKindVar = 0;
            CheckButton.Instance.flushVar = 0;
            CheckButton.Instance.straightVar = 0;
            CheckButton.Instance.threeOfaKindVar = 0;
            CheckButton.Instance.twoPairVar = 0;
            CheckButton.Instance.pairVar = 0;
            CheckButton.Instance.fullHouseVar = 0;
            checkComplete = 6;
        }*/
        if (checkComplete == 6)
        {
            if (P4__setected != P4_setected.Fold)
            {

                CheckButton.Instance.ResetFlush();
                CheckButton.Instance.finalCheckFiveCard[5] = playerOneCards[0];
                CheckButton.Instance.finalCheckFiveCard[6] = playerOneCards[1];

                CheckButton.Instance.RoyalFlush();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[0]);
                //CheckButton.Instance.player3Win[0] = 10;
                CheckButton.Instance.StraightFlush();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[1]);
                //CheckButton.Instance.player2Win[1] = 9;
                CheckButton.Instance.Four_of_a_kind();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[2]);
                // CheckButton.Instance.player2Win[2] = 8;
                CheckButton.Instance.flush();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[4]);
                //CheckButton.Instance.player2Win[4] = 6;
                CheckButton.Instance.Straight();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[5]);
                //CheckButton.Instance.player2Win[5] = 5;
                CheckButton.Instance.Three_of_a_kind();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[6]);
                //CheckButton.Instance.player2Win[6] = 4;
                CheckButton.Instance.TwoPair();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[7]);
                //CheckButton.Instance.player2Win[7] = 3;
                CheckButton.Instance.Pair();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[8]);
                //CheckButton.Instance.player2Win[8] = 2;
                CheckButton.Instance.FullHouse();
                winnerSelectP4.Add(CheckButton.Instance.player4Win[3]);
                //CheckButton.Instance.player2Win[3] = 7;
                CheckButton.Instance.royalFlushVar = 0;
                CheckButton.Instance.straightFlushVar = 0;
                CheckButton.Instance.fourOFaKindVar = 0;
                CheckButton.Instance.flushVar = 0;
                CheckButton.Instance.straightVar = 0;
                CheckButton.Instance.threeOfaKindVar = 0;
                CheckButton.Instance.twoPairVar = 0;
                CheckButton.Instance.pairVar = 0;
                CheckButton.Instance.fullHouseVar = 0;
                playerFourCards[2] = CheckButton.Instance.finalCheckFiveCard[0];
                playerFourCards[3] = CheckButton.Instance.finalCheckFiveCard[1];
                playerFourCards[4] = CheckButton.Instance.finalCheckFiveCard[2];
                playerFourCards[5] = CheckButton.Instance.finalCheckFiveCard[3];
                playerFourCards[6] = CheckButton.Instance.finalCheckFiveCard[4];


                Debug.Log("wtf");
            }
            yes = 1;
            WinnerSelection();
        }
    }
        
   
    public void WinnerSelection()
    {
        /* if (yes == 1)
         {
             for(int i = 0; i < winnerSelectP1.Count; i++)
             {
                 if (winnerSelectP1[i] != 0)
                 {
                     winnerCount.Add(winnerSelectP1[i]);
                 }
             }
             for (int i = 0; i < winnerSelectP2.Count; i++)
             {
                 if (winnerSelectP2[i] != 0)
                 {
                     winnerCount.Add(winnerSelectP2[i]);
                 }
             }
             for (int i = 0; i < winnerSelectP3.Count; i++)
             {
                 if (winnerSelectP3[i] != 0)
                 {
                     winnerCount.Add(winnerSelectP3[i]);
                 }
             }
             for (int i = 0; i < winnerSelectP4.Count; i++)
             {
                 if (winnerSelectP4[i] != 0)
                 {
                     winnerCount.Add(winnerSelectP4[i]);
                 }
             }
         }*/
        /*if (yes == 1)
        {
            for(int i = 0; i < playerOneCards.Count; i++)
            {

            }
        }*/
    }
    public void EqualingEnum_funtion()
    {
        if (Player_turn == 1)
        {
            player_details.player_setected = (setected)P1__setected;

        }
        if (Player_turn == 2)
        {
            player_details.player_setected = (setected)P2__setected;

        }
        if (Player_turn == 3)
        {
            player_details.player_setected = (setected)P3__setected;

        }
        if (Player_turn == 4)
        {
            player_details.player_setected = (setected)P4__setected;

        }

    }

    public void fold_funtion()
    {
        if (player_details.player_setected == player_details.setected.Fold && Player_turn == 1)
        {
            raise_button_object.SetActive(false);
            fold_button_object.SetActive(false);
            check_button_object.SetActive(false);
            call_button_object.SetActive(false);
            raise_panel.SetActive(false);
            Player_turn = 2;
            Time1 = 10f;
        }
        if (Player_turn == 2 && P2__setected == P2_setected.Fold)
        {
            Player_turn = 3;
            Time2 = 10f;

        }
        if (Player_turn == 3 && P3__setected == P3_setected.Fold)
        {
            Player_turn = 4;
            Time3 = 10f;

        }
        if (Player_turn == 4 && P4__setected == P4_setected.Fold)
        {
            Player_turn = 1;
            Time4 = 10f;

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
        if ((Raise_amount_1 != 0 || Raise_amount_2 != 0 || Raise_amount_3 != 0 || Raise_amount_4 != 0) && Player_turn == 1)
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
        if (Player_turn == 1)
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
        raise_change_text.text = Raise_change_1.ToString();
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
        if (Time1 < 0 && Time2 == 10)
        {
            Player_turn = 2;
        }
        if (Time2 < 0 && Time3 == 10)
        {
            Player_turn = 3;
        }
        if (Time3 < 0 && Time4 == 10)
        {
            Player_turn = 4;
        }
        if (Time4 < 0 && Time1 == 10)
        {
            Player_turn = 1;
        }
        if (Player_turn == 1)
        {
            Time4 = 10;
        }
        if (Player_turn == 2)
        {
            Time1 = 10;
        }
        if (Player_turn == 3)
        {
            Time2 = 10;
        }
        if (Player_turn == 4)
        {
            Time3 = 10;
        }
    }
    public void player_Details_funtion()
    {
        if (Player_turn == 1 && Time1 > 0)
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
        if (Player_turn == 2 && Time2 > 0)
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
        if (Player_turn == 3 && Time3 > 0)
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
        if (Player_turn == 4 && Time4 > 0)
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
    public void Check_button()
    {
        P1__setected = P1_setected.Check;
        Time1 = 10;
        Player_turn = 2;

    }
    public void fold_button()
    {
        P1__setected = P1_setected.Fold;
    }
    public void raise_panel_button()
    {
        raise_panel.SetActive(true);

    }
    public void raise_button()
    {
        raise_panel.SetActive(false);
        Player_turn = 2;
        P1__setected = P1_setected.Raise;


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
        if (amount_backend_1 + 1 > Raise_amount_1 + Raise_change_1)
        {
            Raise_amount_1 += Raise_change_1;
        }

    }
    public void raise_decrease_button()
    {
        if (-1 < Raise_amount_1 - Raise_change_1)
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

        P1__setected = P1_setected.call;


    }
    //Card Spawn Random(Vishnu)
    public void DealerSelect()
    {

        Debug.Log(" Dealer :" + dealer);
        switch (dealer)
        {
            case 1:
                Debug.Log(" Small Blind :" + 2 + " Big Blind: " + 3);
                break;
            case 2:
                Debug.Log(" Small Blind :" + 3 + " Big Blind: " + 4);
                break;
            case 3:
                Debug.Log(" Small Blind :" + 4 + " Big Blind: " + 1);
                break;
            case 4:
                Debug.Log(" Small Blind :" + 1 + " Big Blind: " + 2);
                break;

        }
    }

}
[System.Serializable]
public enum players
{
    Player_One, Player_Two, Player_Three, Player_Four

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
        none, Fold, Check, Raise, call

    }
    public enum role
    {
        Dealer, Small_Blind, Big_Blind,

    }


}

