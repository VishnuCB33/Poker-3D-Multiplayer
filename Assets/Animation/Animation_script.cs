using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_script : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    public GameObject card6;
    public GameObject card7;
    public GameObject card8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        card1.SetActive(true);
        card2.SetActive(true);
        card3.SetActive(true);
        card4.SetActive(true);
        card5.SetActive(true);
        card6.SetActive(true);
        card7.SetActive(true);
        card8.SetActive(true);
    }
}
