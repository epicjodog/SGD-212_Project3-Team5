using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rivalRacer : MonoBehaviour
{
    [SerializeField] private GameObject[] checkpoints;
    private Rigidbody rb;
    private int index = 0;
    private bool stopped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion lookOnLook =
        Quaternion.LookRotation(checkpoints[index].transform.position - transform.position);

        transform.rotation =
        Quaternion.Slerp(transform.rotation, lookOnLook, 0.9f);

        if (!stopped) rb.AddRelativeForce(Vector3.forward * 100f);
        CheckMoveSpeed();
    }

    private void CheckMoveSpeed()
    {
        if (rb.velocity.magnitude > 30f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            if (index <= 10)
            {
                index++;
            }
            else
            {
                stopped = true;
            }
        }
    }
}
