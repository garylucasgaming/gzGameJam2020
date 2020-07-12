using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierWinZone : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    private List<bool> winList = new List<bool>();
    private bool hasPlayer = false;
    private bool hasCart = false;
    private bool hasKid = false;

    private void Update()
    {
        print("Items found: " + winList.Count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      

        if(collision.GetComponent<Cart>() || collision.transform.Find("Cart"))
        {
            hasCart = true;
        }
        if(collision.GetComponent<Kid>() || collision.transform.Find("Kid"))
        {
            hasKid = true;
        }

        if(collision.GetComponent<Player>())
        {

            hasPlayer = true;

            foreach(GameObject toggle in GM._toggleList)
            {
                if(toggle.gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.activeSelf)
                {
                    winList.Add(true);
                }

            }

            if(hasPlayer == true && hasKid == true && hasCart == false)
            {
                print("Sir, you need groceries to checkout");

            }
            if(hasPlayer == true && hasKid == false && hasCart == false)
            {
                print("Sir, you need groceries to checkout, and you're kid is still out of control");

            }
            if(hasPlayer == true && hasKid == false && hasCart == true)
            {
                print("Sir, you're child is out of control in the store still");
            }
            if(winList.Count != GM._groceryList.Count && hasPlayer == true && hasKid == true && hasCart == true)
            {
                print("You did not collect all the Groceries needed for the party");
                GM.WinPanel.SetActive(true);
                GM.winLostText.text = "You did not collect all the Groceries needed for the party. You are a dissapointment";
                Time.timeScale = 0;
            }
             if(winList.Count == GM._groceryList.Count && hasKid == true && hasCart == true && hasPlayer == true)
            {
                print("You have all the things!");
                GM.WinPanel.SetActive(true);
                GM.winLostText.text = "You got Everything while Containing the Child!";
                Time.timeScale = 0;

            }

        }

        


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            hasPlayer = false;
        }
        if(collision.GetComponent<Cart>())
        {
            hasCart = false;
            winList = new List<bool>();

        }
        if(collision.GetComponent<Kid>())
        {
            hasKid = false;
        }
    }



}
