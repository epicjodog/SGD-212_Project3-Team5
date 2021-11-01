using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShip : MonoBehaviour
{
    Vector3 shipDir;
    float throttleInput = 0;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PitchYawInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        shipDir.x = -moveInput.y;
        shipDir.y = moveInput.x;
    }

    public void RollThrottleInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        shipDir.z = -moveInput.x;
        throttleInput = moveInput.y;
    }

    private void Update()
    {
        transform.Rotate(shipDir * 0.1f);
        rb.AddRelativeForce(Vector3.forward * throttleInput * 3);
        CheckMoveSpeed();
    }

    private void CheckMoveSpeed()
    {
        if (rb.velocity.magnitude > 30f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30f);
        }
    }
}
