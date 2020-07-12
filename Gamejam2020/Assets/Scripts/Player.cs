using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    Vector2 directionalInput;
    Collider2D collider;
    Rigidbody2D rigidBody;
    new public Transform transform;

    public Transform handSlot;
    public Interactable availableInteractable;
    public Interactable heldInteractable;
    bool doInteraction;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        GetInput();
        HandleInput();
        //print($"HeldInteractable: {heldInteractable} \nAvailableInteractable: {availableInteractable}");
    }

    void GetInput()
    {
        directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.E)) doInteraction = true;
        else doInteraction = false;
    }

    void HandleInput()
    {
        HandleMovement();
        if (doInteraction) HandleInteraction();
        UpdateHandSlot();
    }

    void HandleMovement()
    {
        rigidBody.velocity = directionalInput.normalized * moveSpeed;
    }

    void HandleInteraction()
    {
        if (availableInteractable != null)
        {
            availableInteractable.OnInteract(this);
        }
        else if (heldInteractable != null)
        {
            //Drop held interactable
            ((MonoBehaviour)heldInteractable).transform.SetParent(null);

            Collider2D[] colliders = ((MonoBehaviour)heldInteractable).GetComponents<Collider2D>();

            foreach (Collider2D col in colliders)
            {
                col.enabled = true;
            }

            heldInteractable = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        availableInteractable = collision.GetComponent<Interactable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        availableInteractable = null;
    }

    private void UpdateHandSlot()
    {
        if (directionalInput.y == 1f && handSlot.localPosition.y < 0 
            || directionalInput.y == -1f && handSlot.localPosition.y > 0)
        {
            handSlot.localPosition = -handSlot.localPosition;
        }
        if (heldInteractable != null)
        {
            ((MonoBehaviour)heldInteractable).transform.position = handSlot.position;
        }
    }

}
