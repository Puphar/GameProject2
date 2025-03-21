using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFloorForPot : MonoBehaviour
{
    public float speedModify = 2f; // Speed while on the slow floor
    private float originalSpeed = 20f; // Player's original speed
    private bool isPlayerInSlime = false; // Track if the player is in slime
    private PlayerControl playerControl; // Cache reference to PlayerControl

    private void OnTriggerEnter(Collider other)
    {
        playerControl = other.GetComponent<PlayerControl>();
        if (playerControl != null && !isPlayerInSlime)
        {
            // Store the original speed once when entering the slow floor
            originalSpeed = playerControl.speed;
            isPlayerInSlime = true;
            // Set the player's speed to the modified (slow) speed
            playerControl.speed = speedModify;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerControl = other.GetComponent<PlayerControl>();
        if (playerControl != null && isPlayerInSlime)
        {
            // Reset the player's speed when they leave the slow floor
            playerControl.speed = 20f;
            isPlayerInSlime = false;
        }
    }

    private void Update()
    {
        // Failsafe: In case OnTriggerExit is not called correctly, reset speed if the playerControl reference is missing
        if (isPlayerInSlime && playerControl == null)
        {
            isPlayerInSlime = false; // Reset flag
        }

        Destroy(gameObject, 5f);
    }
}
