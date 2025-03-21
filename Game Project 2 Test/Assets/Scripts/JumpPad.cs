using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpBoost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerControl component from the player
            PlayerControl playerScript = other.GetComponent<PlayerControl>();

            if (playerScript != null)
            {
                // Call the JumpForce method in the PlayerControl script
                playerScript.JumpForce(jumpBoost);  // Capital 'J' to match the method in PlayerControl
            }
            else
            {
                Debug.LogError("PlayerControl script not found on the player object.");
            }
        }
    }
}
