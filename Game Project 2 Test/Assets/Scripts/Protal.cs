using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private PlayerProgress playerProgress;

    private void Start()
    {
        // Find and assign the PlayerProgress instance
        playerProgress = FindObjectOfType<PlayerProgress>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (playerProgress != null)
            {
                // Unlock the next level
                playerProgress.UnlockNextLevel(currentSceneIndex);
            }

            // Load the lobby scene
            SceneManager.LoadScene("Lobby");
        }
    }
}
