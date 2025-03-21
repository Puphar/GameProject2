using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMultiButton : MonoBehaviour
{
    public Animator animator;  // The door that opens
    public Vector3 openPosition;  // Position for the door when opened
    private Vector3 closedPosition;  // Position for the door when closed

    public GameObject[] buttons;  // The pressure plates in the scene

    private bool[] buttonStates;  // Track whether each button is pressed

    [SerializeField] private AudioClip doorOpenClip;

    void Start()
    {
        // Initialize the button states to all false (none are pressed at the start)
        buttonStates = new bool[buttons.Length];
    }

    // Function to be called when a player steps on or presses a button
    public void StepOnPlate(int buttonIndex)
    {
        // Mark the button as pressed
        buttonStates[buttonIndex] = true;

        // Check if all buttons are pressed
        if (AreAllButtonsPressed())
        {
            OpenDoor();  // Open the door when all buttons are pressed
        }
    }

    // Function to check if all buttons are pressed
    bool AreAllButtonsPressed()
    {
        foreach (bool buttonPressed in buttonStates)
        {
            if (!buttonPressed)  // If any button is not pressed, return false
            {
                return false;
            }
        }
        return true;  // If all buttons are pressed, return true
    }

    // Open the door
    void OpenDoor()
    {
        SoundManager.instance.PlaySFXClip(doorOpenClip, transform, 1f);
        animator.SetBool("Open",true);
    }
}
