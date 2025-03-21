using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float speed = 5f;

    private Rigidbody rb;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = pointB.position;
    }

    void FixedUpdate()
    {
        // Move the platform using Rigidbody physics
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        // If the platform reaches the target position, switch to the other point
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Parent the player to the platform
            other.transform.SetParent(transform);

            // Optional: Also, reset the player's Rigidbody velocity to prevent them from being "stuck" in place
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero; // Stop player velocity when they step on the platform
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Unparent the player from the platform
            other.transform.SetParent(null);
        }
    }
}
