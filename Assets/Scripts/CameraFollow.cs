using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 cameraOffset;
    Vector3 velocity = Vector3.zero;
    [SerializeField]
    float smoothTime = 0.3f;
    private void Update()
    {
        Vector3 target_position = followTarget.position + cameraOffset + new Vector3(1f * followTarget.localScale.x, 0, 0);

        transform.position = Vector3.SmoothDamp(transform.position, target_position, ref velocity, smoothTime);
    }
}
