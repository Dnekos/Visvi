using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    PlatformEffector2D effector2D;
    PlayerActions inputs;

    [SerializeField]
    bool isSecondFloor;

    PlayerController player;
    
    private void Awake()
    {
        inputs = new PlayerActions();
        inputs.Move.Stairs.performed += ctx => OnStairs(ctx.ReadValue<float>());
    }
    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
        effector2D.surfaceArc = 0f;
        player = FindObjectOfType<PlayerController>();
    }

    void OnStairs(float input)
    {
        if (PlayerController.State != GameState.Play)
            return;

        if (input != 0 && !isSecondFloor)
        {
            effector2D.surfaceArc = 180f;
        }
        if (input < 0 && isSecondFloor)
            effector2D.surfaceArc = 0f;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && !isSecondFloor)
        {
            effector2D.surfaceArc = 0f;
            player.OnStair = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && isSecondFloor)
            effector2D.surfaceArc = 180f;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && !isSecondFloor && effector2D.surfaceArc == 180f)
            player.OnStair = true;
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
