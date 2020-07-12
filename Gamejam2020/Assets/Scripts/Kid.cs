using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour, Interactable
{
    bool beingHeld;

    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null)
        {
            //drop heldInteractable
            ((MonoBehaviour)player.heldInteractable).transform.SetParent(null);
        }

        //pickup kid
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        transform.SetParent(player.transform);
        player.heldInteractable = this;
    }
}
