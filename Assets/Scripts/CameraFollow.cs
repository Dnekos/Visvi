using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 cameraOffset;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position + cameraOffset + new Vector3(1f * followTarget.localScale.x, 0, 0), 0.1f);
    }
}
