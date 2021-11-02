using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShip : MonoBehaviour
{
    Vector3 shipDir;
    float throttleInput = 0;
    private Rigidbody rb;
    public float flySpeed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PitchYawInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        shipDir.x = moveInput.y;
        shipDir.y = moveInput.x;
    }

    public void RollInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        shipDir.z = -moveInput.x;
    }

    public void Throttle(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        throttleInput = input;
    }

    private void Update()
    {
        Vector3 trueDir = new Vector3(shipDir.x, shipDir.y, 0f);
        transform.Rotate(trueDir * 0.2f);
        CheckMoveSpeed();
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * throttleInput * flySpeed);
        rb.AddRelativeTorque(Vector3.forward * 10 * shipDir.z);
        //LevelOut();
    }

    private void CheckMoveSpeed()
    {
        if (rb.velocity.magnitude > 30f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30f);
        }
    }

    //private void LevelOut()
    //{
        //float roll = transform.eulerAngles.z;
    //}
}
