using UnityEngine;

public class CrabChasePlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 3f; // Speed at which the crab moves
    public Animator animator; // Reference to the crab's Animator
    private Vector3 direction; // Direction in which the crab will move

    public float idleThreshold = 0.1f; // Threshold for being "close enough" to the player

    void Update()
    {
        // Check the player's position relative to the crab
        Vector3 playerPosition = player.position;
        Vector3 crabPosition = transform.position;
        float distanceToPlayer = playerPosition.x - crabPosition.x;

        // Determine if the crab should go idle (player is close enough)
        if (Mathf.Abs(distanceToPlayer) < idleThreshold)
        {
            // Player is directly in front of the crab, stop movement and set idle animation
            direction = Vector3.zero; // Stop moving
            animator.SetBool("Idle", true); // Play idle animation
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
        else if (distanceToPlayer > 0)
        {
            // Player is to the right of the crab, move right
            direction = Vector3.right; // Move crab right (local right direction)
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", true); // Play right walk animation
            animator.SetBool("Idle", false); // Disable idle animation

            // Move the crab sideways
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (distanceToPlayer < 0)
        {
            // Player is to the left of the crab, move left
            direction = Vector3.left; // Move crab left (local left direction)
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", true); // Play left walk animation
            animator.SetBool("Idle", false); // Disable idle animation

            // Move the crab sideways
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        animator.SetBool("Cancer_ATK", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Cancer_ATK", true);
    }
}
