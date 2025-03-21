using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialPressurePlate : MonoBehaviour
{
    public GameObject door;  // The door that opens
    public Vector3 openPosition;  // Position for the door when opened
    private Vector3 closedPosition;  // Position for the door when closed

    public GameObject[] pressurePlates;  // The pressure plates in the scene
    public int[] correctSequence;  // The correct sequence of plate indices (e.g., 3 → 4 → 2 → 1)

    private int currentStep = 0;  // The current step the player is on in the sequence

    private bool isDoorOpen = false;

    void Start()
    {
        // Store the closed position of the door
        closedPosition = door.transform.position;
    }

    // Function to be called when a player steps on a pressure plate
    public void StepOnPlate(int plateIndex)
    {
        if (plateIndex == correctSequence[currentStep])
        {
            // Player stepped on the correct plate in the sequence
            currentStep++;

            if (currentStep == correctSequence.Length)
            {
                // Player completed the sequence, open the door
                OpenDoor();
            }
        }
        else
        {
            // Player stepped on the wrong plate, reset the sequence
            ResetSequence();
        }
    }

    void OpenDoor()
    {
        if (!isDoorOpen)
        {
            door.transform.position = openPosition;
            isDoorOpen = true;
        }
    }

    void CloseDoor()
    {
        if (isDoorOpen)
        {
            door.transform.position = closedPosition;
            isDoorOpen = false;
        }
    }

    void ResetSequence()
    {
        currentStep = 0;
        Debug.Log("Incorrect sequence! Resetting...");
    }
}
