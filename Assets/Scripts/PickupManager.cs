using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] public enum Pickup
{
    None = 0,
    spoons = 0x1,
    basket = 0x2,
    flour = 0x4,
    grapes = 0x8,
    grapejuice = 0x10,
    sugar = 0x20,
    servingbowl = 0x40,
    mixingbowl = 0x80,

    //PICKUP_LENGTH = basket | spoons | flour | grapes | grapejuice | sugar | servingbowl | mixingbowl // all of them
    All_PICKUPS = basket | spoons | flour | grapes // only items with dialogue/gameobjects
};

public class PickupManager : MonoBehaviour
{
    public Pickup data;
}
