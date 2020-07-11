using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    Vector2 directionalInput;
    Collider2D collider;
    Rigidbody2D rigidBody;

    public Transform handSlot;
    public Interactable availableInteractable;
    public Interactable heldInteractable;
    bool doInteraction;

    private void Start()
    {
        collider  = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
        HandleInput();
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
        else
        {
            //drop held interactable?
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        availableInteractable = collision.GetComponent<Interactable>();
    }

}
