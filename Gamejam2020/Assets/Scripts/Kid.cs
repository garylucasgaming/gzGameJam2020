using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour, Interactable
{
    new Transform transform;

    public int chanceToMove = 10;
    public int chanceToDemolish = 1;
    public float moveSpeed;

    public bool beingHeld = false;

    [SerializeField] bool demolishing = false;
    [SerializeField] bool goingToDemolish = false;
    [SerializeField] bool movingToWaypoint = false;
    public Transform currentWaypoint;
    public float waypointSearchRange = 5f;
    public float waypointDetectionRange = 0.1f;
    public float aisleSearchRange = 3f;
    public float demolitionDetectionRange = 0.2f;
    public Collider2D demolitionTarget;

    int aisleLayerMask;

    public IEnumerator demolitionCoroutine;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        aisleLayerMask =  1 << LayerMask.NameToLayer("Aisles");
    }


    private void Update()
    {
        if (beingHeld) return;

        if (!movingToWaypoint && !demolishing && !goingToDemolish)
        {
            int randomValue = Random.Range(1, 100);
            print(randomValue);
            if (randomValue <= chanceToDemolish)
            {
                print("Kid is going to destroy an aisle!");
                SelectAisleToDemolish();
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
        else if (goingToDemolish)
        {
            MoveToAisle();
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
        demolishing = false;
        goingToDemolish = false;
        
        if (demolitionCoroutine != null)
        {
            StopCoroutine(demolitionCoroutine);

            if (demolitionTarget.GetComponent<Aisle>().demolished == false) 
            {
                demolitionTarget.GetComponent<Aisle>().ResetVisuals();
            }

            demolitionTarget = null;
        }

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
                print("Something in way");
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

    private void SelectAisleToDemolish()
    {
        Collider2D aisleCol = Physics2D.OverlapCircle(
            transform.position,
            aisleSearchRange,
            aisleLayerMask);

        print("Aisle selected to demolish!: " + aisleCol);
        if (aisleCol != null && aisleCol.GetComponent<Aisle>().demolished == false)
        {
            goingToDemolish = true;
            demolitionTarget = aisleCol;
        }
    }

    private void MoveToAisle()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            demolitionTarget.transform.position,
            moveSpeed * Time.deltaTime);
        CheckDistanceToAisle();
    }

    private void CheckDistanceToAisle()
    {
        Collider2D aisleCol = Physics2D.OverlapCircle(
            transform.position,
            demolitionDetectionRange,
            aisleLayerMask);

        if (aisleCol != null && aisleCol == demolitionTarget)
        { 
            print("Beginning to destroy aisle!");
            demolitionCoroutine = aisleCol.gameObject.GetComponent<Aisle>().Demolish();
            goingToDemolish = false;
            demolishing = true;
            StartCoroutine(demolitionCoroutine);
        }
    }
}
