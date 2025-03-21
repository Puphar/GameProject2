using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float detectionRange = 10f;
    public float shootingInterval = 1.5f;
    public float fieldOfView = 45f;
    public float arrowSpeed = 50f;

    private float lastShotTime = 0f;

    public float rotationSpeed = 5f; // Speed of rotation (optional for smoothness)
    public Animator anim;

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer != Vector3.zero)
        {
            // Rotate directly toward the player, including vertical tilt
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (IsPlayerInSight())
        {
            Attack();
        }
    }

    private bool IsPlayerInSight()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > detectionRange) return false;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfView / 2) return false;

        // Check for obstacles using Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            if (hit.collider.gameObject != player.gameObject)
                return false; // Player is blocked by an obstacle
        }

        return true;
    }

    private void Attack()
    {
        anim.SetBool("Attack", true);

        if (Time.time - lastShotTime >= shootingInterval)
        {
            ShootArrow();
            lastShotTime = Time.time;
        }
    }

    private void ShootArrow()
    {
        if (arrowPrefab == null || shootPoint == null) return;

        // Ensure the shoot point is aiming directly at the player
        //shootPoint.LookAt(player.position);

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position - shootPoint.position).normalized;
            rb.velocity = direction * arrowSpeed; // Adjust speed as needed
        }

        anim.SetBool("Attack", false); // Reset attack animation
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
