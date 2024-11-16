using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Button profilelobby;
    public Button profile;
    public Button amount;
    public Button backfromprofile;
    public Button prof1;
    public Button prof2;
    public Button prof3;
    public Button prof4;
    public Button prof5;
    public Button prof6;
    public Button prof7;
    public Button prof8;
    public Button prof9;
    public Button prof10;
    public Button prof11;
    public Button prof12;
    public Button profprefab;
    public GameObject profilepage;
    public GameObject profilepanel;
    public GameObject amountpanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        profilelobby.onClick.AddListener(OpenProfilePage);
        profile.onClick.AddListener(OpenProfilePanel);
        amount.onClick.AddListener(OpenAmountPanel);
        backfromprofile.onClick.AddListener(Back);
        prof1.onClick.AddListener(Prof1);
        prof2.onClick.AddListener(Prof2);
        prof3.onClick.AddListener(Prof3);
        prof4.onClick.AddListener(Prof4);
        prof5.onClick.AddListener(Prof5);
        prof6.onClick.AddListener(Prof6);
        prof7.onClick.AddListener(Prof7);
        prof8.onClick.AddListener(Prof8);
        prof9.onClick.AddListener(Prof9);
        prof10.onClick.AddListener(Prof10);
        prof11.onClick.AddListener(Prof11);
        prof12.onClick.AddListener(Prof12);
    }
    public void OpenProfilePage()
    {
        profilepage.SetActive(true);
        profilepanel.SetActive(true);
        amountpanel.SetActive(false);
    }
    public void OpenProfilePanel()
    {
        profilepage.SetActive(true);
        profilepanel.SetActive(true);
        amountpanel.SetActive(false);
    }
    public void OpenAmountPanel()
    {
        profilepage.SetActive(true);
        profilepanel.SetActive(false);
        amountpanel.SetActive(true);
    }
    public void Back()
    {
        profilepage.SetActive(false );
    }
    public void Prof1()
    {
        profprefab.GetComponent<Image>().sprite = prof1.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof1.GetComponent<Image>().sprite;
    }
    public void Prof2()
    {
        profprefab.GetComponent<Image>().sprite = prof2.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof2.GetComponent<Image>().sprite;
    }
    public void Prof3()
    {
        profprefab.GetComponent<Image>().sprite = prof3.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof3.GetComponent<Image>().sprite;
    }
    public void Prof4()
    {
        profprefab.GetComponent<Image>().sprite = prof4.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof4.GetComponent<Image>().sprite;
    }
    public void Prof5()
    {
        profprefab.GetComponent<Image>().sprite = prof5.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof5.GetComponent<Image>().sprite;
    }
    public void Prof6()
    {
        profprefab.GetComponent<Image>().sprite = prof6.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof6.GetComponent<Image>().sprite;
    }
    public void Prof7()
    {
        profprefab.GetComponent<Image>().sprite = prof7.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof7.GetComponent<Image>().sprite;
    }
    public void Prof8()
    {
        profprefab.GetComponent<Image>().sprite = prof8.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof8.GetComponent<Image>().sprite;
    }
    public void Prof9()
    {
        profprefab.GetComponent<Image>().sprite = prof9.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof9.GetComponent<Image>().sprite;
    }
    public void Prof10()
    {
        profprefab.GetComponent<Image>().sprite = prof10.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof10.GetComponent<Image>().sprite;
    }
    public void Prof11()
    {
        profprefab.GetComponent<Image>().sprite = prof11.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof11.GetComponent<Image>().sprite;
    }
    public void Prof12()
    {
        profprefab.GetComponent<Image>().sprite = prof12.GetComponent<Image>().sprite;
        profilelobby.GetComponent<Image>().sprite = prof12.GetComponent<Image>().sprite;
    }

}
 