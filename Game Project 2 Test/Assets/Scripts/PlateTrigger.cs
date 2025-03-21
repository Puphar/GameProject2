using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : MonoBehaviour
{
    public int plateIndex;  // The index of this plate in the sequence
    public SequentialPressurePlate puzzleManager;  // Reference to the main puzzle manager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Assuming the player has the "Player" tag
        {
            puzzleManager.StepOnPlate(plateIndex);
        }
    }
}
