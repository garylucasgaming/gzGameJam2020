using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour, Interactable
{
    new Transform transform;

    int chanceToMove = 10;
    int chanceToDestroy = 1;
    public float moveSpeed;

    public bool beingHeld = false;

    bool destroying = false;
    bool movingToWaypoint = false;
    public Transform currentWaypoint;
    public float waypointSearchRange = 5f;
    public float waypointDetectionRange = 0.1f;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }


    private void Update()
    {
        if (beingHeld) return;

        if (!movingToWaypoint && !destroying)
        {
            int randomValue = Random.Range(1, 100);
            print(randomValue);
            if (randomValue <= chanceToDestroy)
            {
                print("Kid is going to destroy an aisle!");
                //look for a shelf nearby to destroy
            }
            else if (randomValue <= chanceToMove)
            {
                print("Kid has chosen a place to go");
                SelectWaypoint();
            }
        }
        else if (movingToWaypoint)
        {
            MoveToWaypoint();
        }
        else if (destroying)
        {
            //do destroying
        }
    }

    public void OnInteract(Player player)
    {
        if (player.heldInteractable != null)
        {
            //drop heldInteractable
            player.DropHeldInteractable();
        }

        //pickup kid
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        transform.SetParent(player.transform);
        player.heldInteractable = this;
        movingToWaypoint = false;
        destroying = false;
        beingHeld = true;
    }

    public void SelectWaypoint()
    {
        int waypointLayerMask = 1 << LayerMask.NameToLayer("Waypoints");
        //Collider2D[] waypoints = Physics2D.OverlapCircleAll(transform.position, waypointSearchRange, waypointLayerMask);
        List<Collider2D> waypointCols = 
            new List<Collider2D>(Physics2D.OverlapCircleAll(
            transform.position, 
            waypointSearchRange, 
            waypointLayerMask));

        if (waypointCols.Count <= 0) return;

        int aisleLayerMask = 1 << LayerMask.NameToLayer("Aisles");
        while (waypointCols.Count > 0)
        {
            Collider2D chosenWaypointCol = waypointCols[Random.Range(0, waypointCols.Count)];

            //See if kid would need to path around an aisle in order to get to waypoint, 
            //throw it out if yes
            var hit = Physics2D.Raycast(
                transform.position,
                chosenWaypointCol.transform.position,
                Vector2.Distance(transform.position, chosenWaypointCol.transform.position),
                aisleLayerMask);
            if (hit.collider != null)
            {
                waypointCols.Remove(chosenWaypointCol);
            }
            else
            {
                print("waypoint chosen");
                currentWaypoint = chosenWaypointCol.transform;
                movingToWaypoint = true;
                break;
            }
        }
    }

    private void MoveToWaypoint()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            currentWaypoint.position,
            moveSpeed * Time.deltaTime);
        CheckDistanceToCurrentWaypoint();
    }

    private void CheckDistanceToCurrentWaypoint()
    {
        if (Vector2.Distance(transform.position, currentWaypoint.position)
            < waypointDetectionRange)
        {
            movingToWaypoint = false;
        }
    }
}
