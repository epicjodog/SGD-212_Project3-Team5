using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float smoothSpeed = 0.4f;

    private void FixedUpdate()
    {
        Vector3 smoothPos = Vector3.Lerp(transform.position, target.position, smoothSpeed);

        transform.position = smoothPos;
        transform.LookAt(player);
    }
}
