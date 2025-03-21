using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBridge : MonoBehaviour
{
    public Animator bridge1;  // The first bridge (one of the opposite ones)
    public Animator bridge2;  // The second bridge (the opposite one)
    public float openDuration = 5f;  // Time in seconds the bridges stay open before closing automatically

    private bool playerOnButton = false;
    private bool bridgesOpen = false;
    private float timer = 0f;

    private void Update()
    {
        // If the player is on the button, open the bridges
        if (playerOnButton && !bridgesOpen)
        {
            OpenBridges();
            bridgesOpen = true;
            timer = openDuration;  // Reset the timer when bridges open
        }

        // If the bridges are open, count down the timer
        if (bridgesOpen)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                CloseBridges();
                bridgesOpen = false;
            }
        }
    }

    private void OpenBridges()
    {
        bridge1.SetTrigger("open");
        bridge2.SetTrigger("open");

        Debug.Log("Open");
    }

    private void CloseBridges()
    {
        bridge1.SetTrigger("close");
        bridge2.SetTrigger("close");

        Debug.Log("Close");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the player is tagged correctly
        {
            playerOnButton = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnButton = false;
        }
    }
}
