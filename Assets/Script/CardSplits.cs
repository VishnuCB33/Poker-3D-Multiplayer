using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class CardSplits : NetworkBehaviour
{
    public static CardSplits instance;
    [SerializeField] private List<GameObject> Cards; // Assign your 52 cards in the inspector
    private List<int> randomCards = new List<int>();
    private List<GameObject> randomCardsGameObject = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    
}
