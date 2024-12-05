using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float life = 3.0f;
    private float maxRange = 50.0f;
    public CameraMove cameraPos;
    public CameraController orientation;

    private Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;
        Destroy(gameObject, life);
    }

    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Platform") ||
            collision.gameObject.CompareTag("Water"))
        {
            return;
        }

        else if (collision.gameObject.CompareTag("Box"))
        {
            // Find the "Attack" script on the player or wherever it is assigned.
            Attack attackScript = FindObjectOfType<Attack>();
            if (attackScript != null)
            {
                attackScript.AddPoints(50); // Increase points by 50 for each hit.
            }
        }

        if (collision.gameObject != cameraPos && collision.gameObject != orientation)
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    public void ChildMethod()
    {
        Debug.Log("Child method called!");
    }
}