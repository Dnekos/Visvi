using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDialogue
{
    Pickup mindex;
    string name;

    string op_dialogue;
    string end_dialogue;

    string[] hint_dialogue;
    int hintindex;

    public TaskDialogue() // default constructor
    {
        name = "";
        hintindex = -1;
        mindex = Pickup.None;
        op_dialogue = "";
        hint_dialogue = new string[] {  };
        end_dialogue = "";
    }

    public TaskDialogue(Pickup index) // proper constructor (with all dialogue)
    {
        hintindex = -1;
        mindex = index;
        switch (index)
        {
            case Pickup.basket:
                name = "\u13D4\u13B7\u13E3";
                op_dialogue = "Can you give me my "+name+"?";
                hint_dialogue = new string[] { "I left it outside in the left yard." };
                break;
            case Pickup.spoons:
                //name = "\u13A4\u13B5\u13AB\u13AB"; // cup name
                name = "spoons";
                op_dialogue = "Can you give me some " + name + "?";
                hint_dialogue = new string[] { "It is to the right, in the kitchen." };
                break;
            case Pickup.flour:
                name = "\u13A2\u13D2\u13E9\u13C2\u13A8";
                op_dialogue = "Can you give some " + name + "?";
                hint_dialogue = new string[] { "I keep it in the Kitchen, by the door." };
                break;
            case Pickup.grapes:
                name = "\u13A4\u13C2\u13D6\u13B8\u13B3\u13D7";
                op_dialogue = "Can you get me some " + name + "?";
                hint_dialogue = new string[] { "They grow by the far end of the left yard." };
                break;
            case Pickup.mixingbowl:
                name = "";
                op_dialogue = "Can you give me a " + index + "?";
                hint_dialogue = new string[] { "Its around here somewhere, keep looking." };
                break;
            case Pickup.grapejuice:
                name = "";
                op_dialogue = "Can you give me a " + index + "?";
                hint_dialogue = new string[] { "Its around here somewhere, keep looking." };
                break;
            case Pickup.servingbowl:
                name = "";
                op_dialogue = "Can you give me a " + index + "?";
                hint_dialogue = new string[] { "Its around here somewhere, keep looking." };
                break;
            case Pickup.sugar:
                name = "";
                op_dialogue = "Can you give me a " + index + "?";
                hint_dialogue = new string[] { "Its around here somewhere, keep looking." };
                break;
            default:
                name = "";
                op_dialogue = "Can you give me a " + index + "?";
                hint_dialogue = new string[] { "Its around here somewhere, keep looking." };
                break;
        }

        end_dialogue = "Thank you!";
    }
    public string GiveHint()
    {
        hintindex = (hintindex + 1) % hint_dialogue.Length; // increment hint for rotation
        return hint_dialogue[hintindex];
    }
    public string StartLine()
    {
        return op_dialogue;
    }
    public string CompletionLine()
    {
        return end_dialogue;
    }
    public Pickup GetTask()
    {
        return mindex;
    }
}