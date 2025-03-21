using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiButton : MonoBehaviour
{
    public BridgeMultiButton bridgeController;  // Reference to the main BridgeMultiButton script
    public int buttonIndex;  // This button's index

    [SerializeField] private AudioClip buttonClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the player steps on the button
        {
            SoundManager.instance.PlaySFXClip(buttonClip, transform, 1f);
            bridgeController.StepOnPlate(buttonIndex);  // Inform the bridge controller about this button press
        }
    }
}
