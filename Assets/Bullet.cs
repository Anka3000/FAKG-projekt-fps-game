using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float life = 3.0f;
    private float maxRange = 50.0f;
    public CameraMove cameraPos;
    public CameraController orientation;

    private Vector3 startPosition;
    private Vector3 velocity;
    private float startTime;

    void Awake()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // Przemieszczanie pocisku bez użycia Rigidbody
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < life) // Sprawdzamy, czy czas życia pocisku jeszcze nie minął
        {
            // Przemieszczamy pocisk w kierunku, w którym był początkowo ustawiony
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }
        else
        {
            Destroy(gameObject);
        }

        // Sprawdzamy, czy pocisk przekroczył maksymalny zasięg
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
            Debug.Log("Hit a Box!");  // Dodaj log, aby sprawdzić, czy trafiono w boksa
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
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
    public void ChildMethod()
    {
        Debug.Log("Child method called!");
    }
}