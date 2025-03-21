using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public float healamount = 100f;

    [SerializeField] private AudioClip checkPointClip;
    public Animator anim;

    private bool isActivated = false; // Flag to ensure checkpoint is triggered only once

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated) return; // Exit early if the checkpoint is already activated

        if (other.CompareTag("Player"))
        {
            // Restore health when the player enters the checkpoint
            other.GetComponent<HealthManager>().RestoreHealth(healamount);

            // Play the "CheckPointOn" animation directly
            anim.Play("CheckPointOn");

            // Play the checkpoint sound
            SoundManager.instance.PlaySFXClip(checkPointClip, transform, 1f);

            // Set the flag to true to prevent re-triggering
            isActivated = true;
        }
    }
}
