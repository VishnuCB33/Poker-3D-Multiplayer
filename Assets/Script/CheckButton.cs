using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CheckButton : MonoBehaviour
{
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
               
            }
        }
       
        return isA;

    }
}
