using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckButton : MonoBehaviour
{
    public AllCards allCards=new AllCards();
    [System.Serializable]


    public struct AllCards
    {
        [Header("Card Series")]

        public List<GameObject> clubs;
        public List<GameObject> diamonds;
        public List<GameObject> hearts;
        public List<GameObject> spades;

      /*  [Header("Winner Combinations")]

        public List<GameObject> royalFlush;
        public List<GameObject> straightFlush;
        public List<GameObject> fourOfKing;
        public List <GameObject> fullHouse;
        public List<GameObject> flush;
        public List<GameObject> straight;
        public List<GameObject> threeOfKind;
        public List<GameObject> twoPair;
        public List<GameObject> onePair;
        public List<GameObject> highCard;
     */

    }
    private void Update()
    {
     
    }
}
