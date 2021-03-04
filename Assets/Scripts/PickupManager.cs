using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pickup
{
    None,
    Basket,
    Grapes,
    Sugar,
    Mixingbowl,
    Flour,
    Mint,
    Juice,
    Spoon,
    ServingBowl,
    Napkin,
    Complete
};

public class PickupManager : MonoBehaviour
{
    public Pickup data;
}
