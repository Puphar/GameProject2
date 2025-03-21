using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public Transform player;          // Reference to the player's transform
    public float followSpeed = 5f;    // Speed at which the item follows the player
    public float distanceBehind = 2f; // Distance behind the player
    public float hoverSpeed = 2f;     // Speed of the hover animation (optional)
    public float hoverAmount = 0.2f;  // Amount of hovering movement (optional)
    private bool isPickedUp = false;  // Tracks if the item is picked up

    private Vector3 initialPosition;  // Store the original position for the hover effect

    private void Start()
    {
        // Store the initial position of the item
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isPickedUp && player != null)
        {
            // Calculate the target position behind the player
            Vector3 targetPosition = player.position - player.forward * distanceBehind; // Behind the player
            targetPosition.y = player.position.y;  // Keep the item at the player's height (optional)

            // Smoothly move the item toward the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Add a slight floating/hovering effect (optional)
            float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
            transform.position = new Vector3(transform.position.x, transform.position.y + hoverOffset, transform.position.z);
        }
        else
        {
            // If not picked up, add an idle floating effect (optional)
            float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
            transform.position = initialPosition + new Vector3(0, hoverOffset, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player picked up the item
        if (other.CompareTag("Player"))
        {
            isPickedUp = true;
            // Optionally, disable the collider so it can't be picked up again
            GetComponent<Collider>().enabled = false;
        }
    }
}
