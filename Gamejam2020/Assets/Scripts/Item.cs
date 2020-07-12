using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Item : MonoBehaviour, Interactable
{
    new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    public enum ItemType
    {
        Apple, Orange, Chips, Cola, Wine, Cheese
    }

    public ItemType itemType;

    public void OnInteract(Player player)
    {
        print("Interacting with item");
        if (player.heldInteractable != null)
        {
            //drop heldInteractable
            ((MonoBehaviour)player.heldInteractable).transform.SetParent(null);
        }

        //pickup item
        transform.SetParent(player.transform);
        player.heldInteractable = this;
    }
}
