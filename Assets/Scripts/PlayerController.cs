using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Play,
    Jump,
    Pause,
    Talk
};
public class PlayerController : MonoBehaviour
{
    public static GameState State;
    GameState pre_pausestate;

    //movement
    [SerializeField]
    float moveSpeed = 3;
    Vector3 moveDirection;
    Rigidbody2D rb;

    // stay on ground variables
    [SerializeField]
    Transform feet;
    public bool OnStair;
    
    //interacting
    bool hitInteract = false;
    public Pickup heldItem = Pickup.None;
    SpeechBubbleManager dialogue;

    PlayerActions inputs;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        inputs = new PlayerActions();
        inputs.Move.Move.performed += ctx => OnMove(ctx.ReadValue<float>());
        inputs.Move.Pause.performed += ctx => OnPause();
        inputs.Move.Interact.performed += ctx => OnInteract(ctx.ReadValue<float>());
        inputs.Move.Jump.performed += ctx => OnJump(ctx.ReadValue<float>());
    }
    private void Start()
    {
        dialogue = GameObject.FindObjectOfType<SpeechBubbleManager>(); // in start in case Awake causes problems
    }

    private void OnMove(float input)
    {
        moveDirection = new Vector3(input, 0);
    }

    private bool OnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(feet.position + new Vector3(0, 0.1f), Vector2.down, 1, LayerMask.GetMask("Ground"));
        if (Mathf.Abs(hit.distance - 0.1f) < 0.01f && State == GameState.Play)
            return true;
        return false;
    }
    private void OnJump(float input)
    {
        if (OnGround())
            rb.AddForce(Vector2.up * 300.0f, ForceMode2D.Force);
    }

    public void OnPause()
    {
        if (State == GameState.Pause)
            State = pre_pausestate; // return state to last
        else
        {
            pre_pausestate = State; // hold local value state to go back to
            State = GameState.Pause; // pause game
        }
        Debug.Log("hit pause");
    }
    private void OnInteract(float input) // unity doesn't like casting inputs as bool, so have to do it as float
    {
        Debug.Log("hit interact");
        if (input > 0)
            hitInteract = true;
        else
            hitInteract = false;

        if (State == GameState.Talk && input == 1)
        {
            hitInteract = false; // prevent getting stuck in a loop when walking and talking
            dialogue.NextLine();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hitInteract || State != GameState.Play)
            return;
        switch(collision.tag)
        {
            case "Pickup":
                if (heldItem == Pickup.None && collision.GetComponent<PickupManager>().data == ElderManager.assignedTask.GetTask()) // only pickup if not holding item and item matches task
                {
                    hitInteract = false; // prevent doing multiple actions this frame if multiple collisions occur
                    heldItem = collision.GetComponent<PickupManager>().data; // set held item
                    SoundManager.instance.PlayPickupSFX();
                    Destroy(collision.gameObject);
                }
                break;
            case "Talkable":
                hitInteract = false; // prevent doing multiple actions this frame if multiple collisions occur
                collision.GetComponent<ElderManager>().Talk(heldItem);
                State = GameState.Talk;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (State != GameState.Play)
            return;

        if (moveDirection.x < 0) // if going left, flip sprite
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDirection.x > 0) // if going right, set sprite back
            transform.localScale = new Vector3(1, 1, 1);

        float stairmodifier = (OnStair && !OnGround() && moveDirection.x < 0) ? 0.5f : 1; // slow down going down stairs
        transform.position += moveDirection * moveSpeed * Time.deltaTime * stairmodifier; // move player

        if (OnStair)
        {
            RaycastHit2D hit = Physics2D.Raycast(feet.position + new Vector3(0, 0.3f), Vector2.down, 1, LayerMask.GetMask("Stair"));
            Vector2 slope = new Vector2(-0.6f, 0.8f);

            // if not a huge distance(no stairs at all)   if not standing still                           if ascending side                         stop slipping
            if (Mathf.Abs(hit.distance - 0.3f) < 0.3f && Mathf.Abs(hit.distance - 0.3f) > 0.03f && slope.ToString() == hit.normal.ToString() && moveDirection.x != 0)
                transform.position -= Vector3.up * (hit.distance - 0.3f);
        }        
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
