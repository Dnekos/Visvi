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
    Vector3 originalscale;
    private void Start()
    {
        originalscale = transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && ElderManager.assignedTask.GetTask() == data)
            transform.localScale = originalscale * 1.2f;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && ElderManager.assignedTask.GetTask() == data)
            transform.localScale = originalscale;
    }
}
