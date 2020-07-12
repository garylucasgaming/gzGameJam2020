using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour, Interactable
{
    new Transform transform;
    GameManager gameManager;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null && player.heldInteractable is Item)
        { 
            Item playerHeldItem = (Item)player.heldInteractable;
            gameManager.AddToCollectedList(playerHeldItem.gameObject);
            gameManager.UpdateToggles();

            playerHeldItem.transform.position = this.transform.position;
            playerHeldItem.transform.SetParent(this.transform);
            playerHeldItem.GetComponent<Collider2D>().enabled = false;
            player.heldInteractable = null;
        }
        else if (player.heldInteractable is Cart)
        {
            //Drop cart
            Cart playerCart = (Cart)player.heldInteractable;
            playerCart.transform.SetParent(null);
            player.heldInteractable = null;
        }
        else
        {
            //player drop interactable (kid / item not on list / other cart?)
            if (player.heldInteractable != null)
            {
                ((MonoBehaviour)player.heldInteractable).transform.SetParent(null);
            }

            //drive cart

            Collider2D[] colliders = GetComponents<Collider2D>();

            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }

            this.transform.SetParent(player.transform);
            player.heldInteractable = this;

            
        }
    }
}
