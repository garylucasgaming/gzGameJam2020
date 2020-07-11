using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour, Interactable
{
    public Transform driverSpot;

    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null && player.heldInteractable == typeof(Item))
        {
            //put item into cart
        }
        else
        {
            //player drop interactable (kid / item not on list / other cart?)
        }
    }
}
