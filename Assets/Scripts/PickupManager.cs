using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] public enum Pickup
{
    None = 0,
    cup = 0x1,
    basket = 0x2,
    PICKUP_LENGTH 
};

public class PickupManager : MonoBehaviour
{
    public Pickup data;
}
