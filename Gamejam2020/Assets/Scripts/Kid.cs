using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour, Interactable
{
    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null)
        {
            //drop heldInteractable
        }

        //pickup kid
    }
}
