using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    private PlayerActions inputs;
    private float waitTimer;
    public float waitTime;
    public bool top;

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
        waitTimer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime;
    }

    void OnStairs(float input)
    {
        if (input != 0 && waitTime <= 0 && !top)
        {
            effector2D.surfaceArc = 180f;
            waitTime = waitTimer;
        }
        if (input < 0 && top)
        {
            effector2D.surfaceArc = 0f;
            waitTime = waitTimer;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && !top)
        {
            effector2D.surfaceArc = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && top)
        {
            effector2D.surfaceArc = 180f;
        }
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
