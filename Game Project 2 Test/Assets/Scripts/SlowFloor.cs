using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFloor : MonoBehaviour
{
    public float speedModify = 2f;
    private float originalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl playerControl = other.GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            // Store the original speed only when the player enters the slow floor
            originalSpeed = playerControl.speed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerControl playerControl = other.GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            // Modify the player's speed while they are on the slow floor
            playerControl.speed = speedModify;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerControl playerControl = other.GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            // Reset the player's speed when they exit the slow floor
            playerControl.speed = originalSpeed;
        }
    }
}
