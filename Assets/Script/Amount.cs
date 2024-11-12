using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Amount : MonoBehaviour
{
    public int Raise_amount_1;
    public TextMeshProUGUI visible_amount_1;
    public static Amount instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Visibleamount()
    {
        visible_amount_1.text = Raise_amount_1.ToString();
    }
}
