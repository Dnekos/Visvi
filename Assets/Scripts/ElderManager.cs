using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum ElderState
{
    Introduction, // introductions, then give out task
    AwaitingTask, // task has been given, will say hints
    Completion // all tasks done
}

public class ElderManager : MonoBehaviour
{
    SpeechBubbleManager speech;
    [SerializeField]
    ElderState state;

    // current tasks
    public static TaskDialogue assignedTask;

    [Header("Debug")]
    [SerializeField]
    Pickup CurrentTask;

    private void Start()
    {
        speech = GetComponentInChildren<SpeechBubbleManager>();
        assignedTask = new TaskDialogue();

        // intro text
        //speech.LoadText("Hello child, it is so good to see you!");
        //speech.LoadText("Come over and talk to me!");
    }

    private void Update()
    {
        if (assignedTask.GetTask() != CurrentTask)
            assignedTask = new TaskDialogue(CurrentTask);
        if (state == ElderState.Completion && speech.loadedText.Count == 0)
            SceneManager.LoadScene(2);
    }

    Pickup IncrementPickup(Pickup item)
    {
        return (Pickup)((int)item + 1);
    }

    public void Talk(Pickup held_item)
    {
        if (speech.loadedText.Count != 0)
            return;

        switch (state)
        {
            case ElderState.Introduction:
                speech.LoadText("Now that you are here we can make some grape dumplings,");
                speech.LoadText("but you will need to collect the ingredients.");
                speech.LoadText("I want you to learn about our culture,");
                speech.LoadText("So I will use some Cherokee words when I speak with you.");
                GiveTask();
                break;
            case ElderState.AwaitingTask:
                CompleteTask(held_item);
                break;
            case ElderState.Completion:
                CompletionDialogue();
                break;
        }
        //speech.LoadText("According to all known laws of aviation, there is no way that a bee should be able to fly. Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyway. Because bees don’t care what humans think is impossible.” SEQ. 75 - “INTRO TO BARRY” INT. BENSON HOUSE - DAY ANGLE ON: Sneakers on the ground. Camera PANS UP to reveal BARRY BENSON’S BEDROOM ANGLE ON: Barry’s hand flipping through different sweaters in his closet. BARRY Yellow black, yellow black, yellow black, yellow black, yellow black, yellow black...oohh, black and yellow... ANGLE ON: Barry wearing the sweater he picked, looking in the mirror. BARRY (CONT’D) Yeah, let’s shake it up a little. He picks the black and yellow one. He then goes to the sink, takes the top off a CONTAINER OF HONEY, and puts some honey into his hair. He squirts some in his mouth and gargles. Then he takes the lid off the bottle, and rolls some on like deodorant. CUT TO: INT. BENSON HOUSE KITCHEN - CONTINUOUS Barry’s mother, JANET BENSON, yells up at Barry. JANET BENSON Barry, breakfast is ready! CUT TO:  1. INT. BARRY’S ROOM - CONTINUOUS BARRY Coming! SFX: Phone RINGING. Barry’s antennae vibrate as they RING like a phone. Barry’s hands are wet. He looks around for a towel. BARRY (CONT’D) Hang on a second! He wipes his hands on his sweater, and pulls his antennae down to his ear and mouth. BARRY (CONT'D) Hello? His best friend, ADAM FLAYMAN, is on the other end. ADAM Barry? BARRY Adam? ADAM Can you believe this is happening? BARRY Can’t believe it. I’ll pick you up. Barry sticks his stinger in a sharpener. SFX: BUZZING AS HIS STINGER IS SHARPENED. He tests the sharpness with his finger. SFX: Bing. BARRY (CONT’D) Looking sharp. ANGLE ON: Barry hovering down the hall, sliding down the staircase bannister. Barry’s mother, JANET BENSON, is in the kitchen. JANET BENSON Barry, why don’t you use the stairs? Your father paid good money for those. 'Bee Movie' - JS REVISIONS 8/13/07 2. BARRY Sorry, I’m excited. Barry’s father, MARTIN BENSON, ENTERS. He’s reading a NEWSPAPER with the HEADLINE, “Queen gives birth to thousandtuplets: Resting Comfortably.” MARTIN BENSON Here’s the graduate. We’re very proud of you, Son. And a perfect report card, all B’s. JANET BENSON (mushing Barry’s hair) Very proud. BARRY Ma! I’ve got a thing going here. Barry re-adjusts his hair, starts to leave. JANET BENSON You’ve got some lint on your fuzz. She picks it off. BARRY Ow, that’s me! MARTIN BENSON Wave to us. We’ll be in row 118,000. Barry zips off. BARRY Bye! JANET BENSON Barry, I told you, stop flying in the house! CUT TO: SEQ. 750 - DRIVING TO GRADUATION EXT. BEE SUBURB - MORNING A GARAGE DOOR OPENS. Barry drives out in his CAR.");
    }

    public void CompleteTask(Pickup given_item)
    {

        if (given_item == Pickup.None)
        {
            speech.LoadText(assignedTask.GiveHint());
            return;
        }
        else if (given_item != assignedTask.GetTask())
        {
            speech.LoadText("That isn't what I asked for.");
            return;
        }

        FindObjectOfType<PlayerController>().heldItem = Pickup.None; // TODO: make it without a FOOT call

        foreach (string line in assignedTask.CompletionLine())
            speech.LoadText(line); // completion text bubble

        if (IncrementPickup(assignedTask.GetTask()) != Pickup.Complete) // if not all tasks are done
        {
            //speech.LoadText("There is more that you must get.");
            GiveTask();
        }
        else //else if everything is done
        {
            assignedTask = new TaskDialogue();

            state = ElderState.Completion;
            CompletionDialogue();
        }
    }

    public void GiveTask()
    {
        state = ElderState.AwaitingTask;

        assignedTask = new TaskDialogue(IncrementPickup(assignedTask.GetTask()));
        CurrentTask = assignedTask.GetTask();

        foreach (string[] line in assignedTask.StartLine())
            speech.LoadText(line[0], line[1]); // completion text bubble
    }

    void CompletionDialogue()
    {
        speech.LoadText("Thank you child, now I have everything we need.");
        speech.LoadText("After I’m done cooking, we can enjoy these grape dumplings together.");
        speech.LoadText("Thank you so much for the help!");
    }
}
