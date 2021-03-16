using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDialogue
{
    Pickup mindex;
    string name;

    string[][] op_dialogue;
    string[] end_dialogue;

    string[] hint_dialogue;
    int hintindex;

    public TaskDialogue() // default constructor
    {
        name = "";
        hintindex = -1;
        mindex = Pickup.None;
        op_dialogue = new string[][] { };
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
                op_dialogue = new string[][] { new string[]{"Before we collect ingredients,", "" },
                    new string[]{ "you will need something to hold them in.", "" },
                    new string[]{ "My ᏔᎷᏣ should be upstairs in my room.", "      (Talutasa)" } };
                hint_dialogue = new string[] { "I wove it myself many years ago.", "It should be on my bed." };
                end_dialogue = new string[] { "Now we can begin gathering our ingredients." };
                break;
            case Pickup.Grapes:
                op_dialogue = new string[][] { new string[]{"The first ingredient we’ll need to make the grape dumplings", "" },
                    new string[]{"is the main ingredient themself.", "" },
                    new string[]{"Go get some ᎤᏂᏖᎸᎳᏗ from the vines in the garden.", "                       (Unitelvladi)" } };
                hint_dialogue = new string[] { "The garden is outside to the left.",
                    "They are bright purple fruit." };
                end_dialogue = new string[] { "Thank you child, these ᎤᏂᏖᎸᎳᏗ are lovely." };
                break;
            case Pickup.Sugar:
                op_dialogue = new string[][] { new string[]{"These dumplings are going to be quite a treat.", "" },
                    new string[]{"Go get some ᎧᎵᏎᏥ from the Kitchen so we can make them sweet", "                      (Kalisetsi)" } };
                hint_dialogue = new string[] { "It should be in a bag near the fridge.", "It is in the kitchen." };
                end_dialogue = new string[] { "Wonderful, these will be quite a sweet treat!" };
                break;
            case Pickup.Mixingbowl:
                op_dialogue = new string[][] { new string[]{"We need a ᎠᏟᏍᏙᏗ to mix all of the ingredients in.", "                     (Atlisdodi)" },
                    new string[]{"There should be one in the Kitchen.", "" } };
                hint_dialogue = new string[] { "It is wooden and brown.", "I think it is by the oven in the kitchen." };
                end_dialogue = new string[] { "Now let’s collect the last ingredients." };
                break;
            case Pickup.Flour:
                op_dialogue = new string[][] { new string[] { "We need something to make the dough.", "" },
                new string[] {"Can you get me the bag of ᎢᏒᏩᏂᎨ.", "                                                (Isvwanige)" } };
                hint_dialogue = new string[] { "It should be in the kitchen.",
                    "I think it is in a bag with a blue stripe." };
                end_dialogue = new string[] { "We almost have everything we need to make the grape dumplings." };
                break;
            case Pickup.Mint:
                op_dialogue = new string[][] { new string[]{"Why don’t we add something to make the dumplings look pretty?", "" },
                    new string[]{"Could you gather ᎠᏕᎸᏧᏃᏢᏗ from from the garden?", "                              (Adelvtsunotlvdi)" } };
                hint_dialogue = new string[] { "It has small pointed green leaves.",
                    "It is in the garden bed next to the grape vines." };
                end_dialogue = new string[] { "This will make for a lovely decoration for our dish." };
                break;
            case Pickup.Juice:
                op_dialogue = new string[][] { new string[] { "In order to get the right flavor,", "" },
                new string[] { "we need to cook the dumplings in ᎦᏁᎲ .", "                                                           (Ganehv)" } };
                hint_dialogue = new string[] { "ᎦᏁᎲ is made by juicing grapes.",
                    "There is a bottle of it in the kitchen." };
                end_dialogue = new string[] { "Now we should have all of the ingredients." };
                break;
            case Pickup.Spoon:
                op_dialogue = new string[][] { new string[] { "Now, we need something to stir this with.", "" },
                    new string[]{ "Could you go get a ᎠᏗᏙᏗ and fork?", "                                  (Adidodi)" } };
                hint_dialogue = new string[] { "I keep them with the silverware.", "They are kept in the kitchen." };
                end_dialogue = new string[] { "Now with a ᎠᏗᏙᏗ (Adidodi) I can finish making the grape dumplings." };
                break;
            case Pickup.ServingBowl:
                op_dialogue = new string[][] { new string[]{"Before we eat, we need something to eat our grape dumplings in.", "" },
                    new string[]{"Go get some ᎠᏟᏍᏙᏗ for us to use.", "                        (Atlisdodi)" } };
                hint_dialogue = new string[] { "They are upstairs in the dining room.", "They are on the shelf upstairs." };
                end_dialogue = new string[] { "Thank you dear, these were a special gift from one of your ancestors." };
                break;
            case Pickup.Napkin:
                op_dialogue = new string[][] { new string[] { "Finally, we need something in case we make a mess", "" },
                    new string[]{ "Go get some ᏗᎩᏑᎵᏙᏗ so we can wipe our hands and faces.", "                       (Digisulidodi)" } };
                hint_dialogue = new string[] { "They are folded up on the side of the table.", "They should be upstairs in the dining room." };
                end_dialogue = new string[] { "Shall we set these out?" };
                break;
            default:
                op_dialogue = new string[][] { new string[] { "Can you give me my " + name + "?", "" } };
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
    public string[][] StartLine()
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