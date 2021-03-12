using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    PlatformEffector2D effector2D;
    PlayerActions inputs;

    [SerializeField]
    bool isSecondFloor;
    
    // stay on ground variables
    [SerializeField]
    Transform feet;

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
    }

    // Update is called once per frame
    void Update()
    {

        if (false)
        {
            RaycastHit2D hit = Physics2D.Raycast(feet.position + new Vector3(0, 0.3f), Vector2.down, 1, LayerMask.GetMask("Ground"));
            Debug.Log(hit.distance - 0.3);
            Debug.DrawRay(feet.position + Vector3.up, Vector2.down, Color.white, 1);
            //if (!hit.collider.isTrigger)
            transform.position -= Vector3.up * (hit.distance - 0.3f);
        }
    }

    void OnStairs(float input)
    {
        if (PlayerController.State != GameState.Play)
            return;

        if (input != 0 &&  !isSecondFloor)
            effector2D.surfaceArc = 180f;
        if (input < 0 && isSecondFloor)
            effector2D.surfaceArc = 0f;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && !isSecondFloor)
            effector2D.surfaceArc = 0f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && !col.isTrigger && isSecondFloor)
            effector2D.surfaceArc = 180f;
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
