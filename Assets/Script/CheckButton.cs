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


        RoyalFlush();


    }
    public bool RoyalFlush()
    {
        bool isA = false;
        int count;
       List<GameObject>merge=finalCheckFiveCard.Concat(playerOneCard).ToList();
       for(int i=0;i<merge.Count;i++)
        {
           for(int j = 0; j < 5; j++)
            {
                switch(merge[i].GetComponent<CardsAttached>().properties.allCards )
                {
                    case CardSeries.A: isA = true;
                        break;

                }
               
               
            }
        }
        return isA;

    }
}
