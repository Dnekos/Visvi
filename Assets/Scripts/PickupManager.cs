using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] public enum Pickup
{
    None = 0,
    cup = 0x1,
    basket = 0x2,
    flour = 0x4,
    grapes = 0x8,
    PICKUP_LENGTH = Pickup.basket | Pickup.cup | Pickup.flour | Pickup.grapes 
};

public class PickupManager : MonoBehaviour
{
    public Pickup data;
}
