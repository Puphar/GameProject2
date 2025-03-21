using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatueTrigger : MonoBehaviour
{
    public float textTime = 3f;     // Countdown duration for text to disappear
    public TMP_Text popUpText;      // Reference to the text to display
    private bool isCountingDown = false; // Tracks whether the countdown should be running

    [SerializeField] private AudioClip sigilClip;
    private bool isActivated = false;

    public GameObject partical;

    // Called when the player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (isActivated) return;

        if (other.CompareTag("Player"))
        {
            partical.SetActive(true);

            SoundManager.instance.PlaySFXClip(sigilClip, transform, 1f);
            popUpText.gameObject.SetActive(true); // Show the text
            isCountingDown = false; // Stop countdown when the player enters again
            textTime = 3f; // Reset the countdown timer
        }
    }

    // Called when the player exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCountingDown = true; // Start the countdown when the player leaves
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // If countdown is active, decrement the time
        if (isCountingDown)
        {
            textTime -= Time.deltaTime; // Decrease the time based on frame rate

            if (textTime <= 0)
            {
                popUpText.gameObject.SetActive(false); // Hide the text when the timer ends
                isCountingDown = false; // Stop countdown after hiding text
            }
        }
    }
}
