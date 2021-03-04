using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDialogue
{
    Pickup mindex;
    string name;

    string[] op_dialogue;
    string[] end_dialogue;

    string[] hint_dialogue;
    int hintindex;

    public TaskDialogue() // default constructor
    {
        name = "";
        hintindex = -1;
        mindex = Pickup.None;
        op_dialogue = new string[] { };
        hint_dialogue = new string[] {  };
        end_dialogue = new string[] { };
    }

    public TaskDialogue(Pickup index) // proper constructor (with all dialogue)
    {
        hintindex = -1;
        mindex = index;
        switch (index)
        {
            case Pickup.Basket:
                //name = "\u13D4\u13B7\u13E3";
                op_dialogue = new string[] { "Before we start getting ingredients,",
                    "you will need something to hold them in.",
                    "My ᏔᎷᏣ (Talutasa) should be upstairs in my room." };
                hint_dialogue = new string[] { "I wove it myself many years ago.", "I keep it near my bed." };
                end_dialogue = new string[] { "Now we can begin gathering our ingredients." };
                break;
            case Pickup.Grapes:
                //name = "\u13A4\u13B5\u13AB\u13AB"; // cup name
                //name = "spoons";
                op_dialogue = new string[] { "The first ingredient we’ll need to make the grape dumplings",
                    "is the main ingredient themself.", "Go get some ᎤᏂᏖᎸᎳᏗ (Unitelvladi) from the vines in the garden." };
                hint_dialogue = new string[] { "You’ll have to go outside to the left to get to the garden.",
                    "They have ripened already, they are now a bright purple." };
                end_dialogue = new string[] { "Thank you child, these ᎤᏂᏖᎸᎳᏗ (Unitelvladi) are lovely." };
                break;
            case Pickup.Sugar:
                //name = "\u13A2\u13D2\u13E9\u13C2\u13A8";
                op_dialogue = new string[] { "These dumplings are going to be quite a treat.",
                    "Go get some ᎧᎵᏎᏥ (Kalisetsi) from the Kitchen to sweeten these up." };
                hint_dialogue = new string[] { "It’s in the cupboard on the right.", "It’s in the kitchen." };
                end_dialogue = new string[] { "Wonderful, these will be quite a sweet treat!" };
                break;
            case Pickup.Mixingbowl:
                //name = "\u13A4\u13C2\u13D6\u13B8\u13B3\u13D7";
                op_dialogue = new string[] { "We need a ᎠᏟᏍᏙᏗ (Atlisdodi) in order to mix the ingredients in.",
                    "There should be one in the Kitchen." };
                hint_dialogue = new string[] { "It's wooden and brown.", "I think it's on the counter." };
                end_dialogue = new string[] { "Now let’s get the last ingredients." };
                break;
            case Pickup.Flour:
                name = "";
                op_dialogue = new string[] { "We’ll need something to hold all of our ingredients together!",
                    "Can you get me the bag of ᎢᏒᏩᏂᎨ (Isvwanige)." };
                hint_dialogue = new string[] { "It’s in the cupboard in a [COLOR] bag.",
                    "It’s on the [shelf number] shelf next to the [random item here]." };
                end_dialogue = new string[] { "Lovely,","we’re close to having all of the ingredients for our grape dumplings." };
                break;
            case Pickup.Mint:
                name = "";
                op_dialogue = new string[] { "Hm, why don’t we add a little bit of something to make it look pretty?",
                    "Could you gather ᎠᏕᎸᏧᏃᏢᏗ from from the garden?" };
                hint_dialogue = new string[] { "It has small pointed green leaves.",
                    "It’s in the garden bed next to the grape vines." };
                end_dialogue = new string[] { "Thank you, this will make for a lovely decoration on our dish." };
                break;
            case Pickup.Juice:
                name = "";
                op_dialogue = new string[] { "The meal will be cooked in some ᎦᏁᎲ (Ganehv)." };
                hint_dialogue = new string[] { "ᎦᏁᎲ is made by juicing grapes.",
                    "You should be able to find a jar in the fridge.",
                    "The Fridge is in the kitchen." };
                end_dialogue = new string[] { "Thank you, now we should have all the ingredients to make the grape dumplings." };
                break;
            case Pickup.Spoon:
                name = "";
                op_dialogue = new string[] { "We need something to stir this with.", "Could you go get a  ᎠᏗᏙᏗ (Adidodi)?" };
                hint_dialogue = new string[] { "I keep them with the silverware.", "They are kept in the kitchen." };
                end_dialogue = new string[] { "Now with a ᎠᏗᏙᏗ (Adidodi) I can finish making the grape dumplings." };
                break;
            case Pickup.ServingBowl:
                name = "";
                op_dialogue = new string[] { "Lastly we’ll need something special to put our grape dumplings in.",
                    "Could you get me two ᎠᏟᏍᏙᏗ (Atlisdodi)" };
                hint_dialogue = new string[] { "They’re in the living room.", "They’re in a special cabinet in the living room." };
                end_dialogue = new string[] { "Thank you dear, these were a special gift from one of your ancestors." };
                break;
            case Pickup.Napkin:
                name = "";
                op_dialogue = new string[] { "Can you help set the table?", "We'll need some [NAPKINS]." };
                hint_dialogue = new string[] { "They are folded up by the side of the table.", "They should be in the dining room." };
                end_dialogue = new string[] { "Thank you, I think we are ready." };
                break;
            default:
                name = "";
                op_dialogue = new string[] { "Can you give me my " + name + "?" };
                hint_dialogue = new string[] { "Its around here somewhere, keep looking." };
                end_dialogue = new string[] { "Thank You" };
                break;
        }

        end_dialogue = new string[] { "Thank You" };

    }
    public string GiveHint()
    {
        hintindex = (hintindex + 1) % hint_dialogue.Length; // increment hint for rotation
        return hint_dialogue[hintindex];
    }
    public string[] StartLine()
    {
        return op_dialogue;
    }
    public string[] CompletionLine()
    {
        return end_dialogue;
    }
    public Pickup GetTask()
    {
        return mindex;
    }
}