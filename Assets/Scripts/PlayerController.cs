using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //movement
    [SerializeField]
    float moveSpeed = 3;
    Vector3 moveDirection;

    //interacting
    bool hitInteract = false;
    [SerializeField]
    Pickup heldItem = Pickup.Empty;

    PlayerActions inputs;

    private void Awake()
    {
        inputs = new PlayerActions();
        inputs.Move.Move.performed += ctx => OnMove(ctx.ReadValue<float>());
        inputs.Move.Pause.performed += ctx => OnPause();
        inputs.Move.Interact.performed += ctx => OnInteract(ctx.ReadValue<float>());
    }

    private void OnMove(float input)
    {
        //Debug.Log(input);
        moveDirection = new Vector3(input, 0);
    }

    private void OnPause()
    {
        Debug.Log("hit pause");
    }
    private void OnInteract(float input) // unity doesn't like casting inputs as bool, so have to do it as float
    {
        Debug.Log("hit interact");
        if (input > 0)
            hitInteract = true;
        else
            hitInteract = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hitInteract)
            return;
        switch(collision.tag)
        {
            case "Pickup":
                if (heldItem == Pickup.Empty) // only pickup if not holding item
                {
                    hitInteract = false; // prevent doing multiple actions this frame if multiple collisions occur
                    heldItem = collision.GetComponent<PickupManager>().data; // set held item
                    Destroy(collision.gameObject);
                }
                break;
            case "Talkable":
                hitInteract = false; // prevent doing multiple actions this frame if multiple collisions occur
                collision.GetComponent<ElderManager>().Talk();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDirection.x < 0) // if going left, flip sprite
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDirection.x > 0) // if going right, set sprite back
            transform.localScale = new Vector3(1, 1, 1);

        transform.position += moveDirection * moveSpeed * Time.deltaTime; // move player
        //hitInteract = false; // reset if didnt hit anything //BUG: preventing it from working at all, events orders or something
    }

    //these two are needed for the inputs to work
    private void OnEnable()
    {
        inputs.Move.Enable();
    }

    private void OnDisable()
    {
        inputs.Move.Disable();
    }
}