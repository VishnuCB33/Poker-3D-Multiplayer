using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardSeries
{
    A, K, Q, J, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten
}
public enum Symbols
{
    Clubs, Heart, Diamond, Spades
}


[CreateAssetMenu(fileName ="AllCheckingCards",menuName ="Cards")]

public class AllCardsChecking : ScriptableObject
{
  
   
    public CardSeries allCards;
   public Symbols symbols;
    public int value;

}
