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
        Debug.Log(input);
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
                collision.GetComponentInChildren<SpeechBubbleManager>().LoadText("According to all known laws of aviation, there is no way that a bee should be able to fly. Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyway. Because bees don’t care what humans think is impossible.” SEQ. 75 - “INTRO TO BARRY” INT. BENSON HOUSE - DAY ANGLE ON: Sneakers on the ground. Camera PANS UP to reveal BARRY BENSON’S BEDROOM ANGLE ON: Barry’s hand flipping through different sweaters in his closet. BARRY Yellow black, yellow black, yellow black, yellow black, yellow black, yellow black...oohh, black and yellow... ANGLE ON: Barry wearing the sweater he picked, looking in the mirror. BARRY (CONT’D) Yeah, let’s shake it up a little. He picks the black and yellow one. He then goes to the sink, takes the top off a CONTAINER OF HONEY, and puts some honey into his hair. He squirts some in his mouth and gargles. Then he takes the lid off the bottle, and rolls some on like deodorant. CUT TO: INT. BENSON HOUSE KITCHEN - CONTINUOUS Barry’s mother, JANET BENSON, yells up at Barry. JANET BENSON Barry, breakfast is ready! CUT TO:  1. INT. BARRY’S ROOM - CONTINUOUS BARRY Coming! SFX: Phone RINGING. Barry’s antennae vibrate as they RING like a phone. Barry’s hands are wet. He looks around for a towel. BARRY (CONT’D) Hang on a second! He wipes his hands on his sweater, and pulls his antennae down to his ear and mouth. BARRY (CONT'D) Hello? His best friend, ADAM FLAYMAN, is on the other end. ADAM Barry? BARRY Adam? ADAM Can you believe this is happening? BARRY Can’t believe it. I’ll pick you up. Barry sticks his stinger in a sharpener. SFX: BUZZING AS HIS STINGER IS SHARPENED. He tests the sharpness with his finger. SFX: Bing. BARRY (CONT’D) Looking sharp. ANGLE ON: Barry hovering down the hall, sliding down the staircase bannister. Barry’s mother, JANET BENSON, is in the kitchen. JANET BENSON Barry, why don’t you use the stairs? Your father paid good money for those. 'Bee Movie' - JS REVISIONS 8/13/07 2. BARRY Sorry, I’m excited. Barry’s father, MARTIN BENSON, ENTERS. He’s reading a NEWSPAPER with the HEADLINE, “Queen gives birth to thousandtuplets: Resting Comfortably.” MARTIN BENSON Here’s the graduate. We’re very proud of you, Son. And a perfect report card, all B’s. JANET BENSON (mushing Barry’s hair) Very proud. BARRY Ma! I’ve got a thing going here. Barry re-adjusts his hair, starts to leave. JANET BENSON You’ve got some lint on your fuzz. She picks it off. BARRY Ow, that’s me! MARTIN BENSON Wave to us. We’ll be in row 118,000. Barry zips off. BARRY Bye! JANET BENSON Barry, I told you, stop flying in the house! CUT TO: SEQ. 750 - DRIVING TO GRADUATION EXT. BEE SUBURB - MORNING A GARAGE DOOR OPENS. Barry drives out in his CAR.");
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
