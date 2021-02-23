using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 cameraOffset;

    private void FixedUpdate()
    {
        transform.position = followTarget.position + cameraOffset;
    }
}
