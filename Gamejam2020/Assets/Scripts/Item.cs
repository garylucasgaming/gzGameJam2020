using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactable
{
    public enum ItemType
    {
        //apple, milk, chicken, etc.
    }

    public ItemType itemType;

    public Sprite sprite;

    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null)
        {
            //drop heldInteractable
        }

        //pickup item
    }
}
