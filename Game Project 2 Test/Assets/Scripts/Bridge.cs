using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private bool toggle = false;   // Track if the bridge is open or closed
    public Animator anim;          // Reference to the Animator component
    public int timer = 5;          // Time (in seconds) before the bridge closes

    private Coroutine closeCoroutine; // Coroutine to handle bridge closing after timer

    [SerializeField] private AudioClip DooropenClip;


    private void OnCollisionEnter(Collision collision)
    {
        if (!toggle)  // If the bridge is not already open
        {
            Open();  // Open the bridge
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Start the timer to close the bridge if the player exits the trigger zone
        if (toggle && closeCoroutine == null)  // Only start timer if bridge is open
        {
            closeCoroutine = StartCoroutine(CloseAfterDelay(timer));
        }
    }

    void Open()
    {
        SoundManager.instance.PlaySFXClip(DooropenClip, transform, 1f);
        toggle = true;  // Mark the bridge as open
        anim.ResetTrigger("close");
        anim.SetTrigger("open");   // Trigger the "open" animation
    }

    void Close()
    {
        toggle = false;  // Mark the bridge as closed
        anim.ResetTrigger("open");
        anim.SetTrigger("close");  // Trigger the "close" animation
    }

    IEnumerator CloseAfterDelay(int delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Close the bridge after the delay
        Close();

        // Reset the coroutine reference to allow it to be restarted in the future
        closeCoroutine = null;
    }
}
