using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierWinZone : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    private List<bool> winList; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if(player.gameObject.name == "Player")
        {

            foreach(GameObject toggle in GM._toggleList)
            {
                if(toggle.gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.activeSelf)
                {
                    winList.Add(true);
                }

            }

            if(winList.Count == GM._groceryList.Count)
            {
                GM.WinPanel.SetActive(true);
                Time.timeScale = 0;
                
            }

        }
    }



}
