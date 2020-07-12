using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),  typeof(BoxCollider2D))]
public class Item : MonoBehaviour, Interactable
{
    public enum ItemType
    {
        Apple, Orange, Chips, Cola, Wine, Cheese
    }

   [SerializeField] public ItemType itemType;


    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null)
        {
            //drop heldInteractable
        }

        //pickup item
    }
}
