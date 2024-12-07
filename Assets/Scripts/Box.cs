using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    public float groundAxis = -10f;
    public float gravity = 9.81f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ProcessGravity();
    }

    void ProcessGravity()
    {
        Vector3 gravityTarget = new Vector3(rb.position.x, groundAxis, rb.position.z);
        Vector3 diff = transform.position - gravityTarget;
        rb.AddForce(-diff.normalized * gravity * (rb.mass));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }
}
