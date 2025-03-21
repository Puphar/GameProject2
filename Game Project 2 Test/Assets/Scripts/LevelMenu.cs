using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public int levelID;  // The level that this portal leads to
    private PlayerProgress playerProgress;

    private void Start()
    {
        // Find the PlayerProgress component in the scene
        playerProgress = FindObjectOfType<PlayerProgress>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerProgress != null && playerProgress.IsLevelUnlocked(levelID))
            {
                // Load the level if it is unlocked
                OpenLevel(levelID);
            }
            else
            {
                // Display a message or feedback if the level is locked
                Debug.Log("This level is locked! Complete previous levels to unlock it.");
            }
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level" + levelID;
        SceneManager.LoadScene(levelName);
    }
}
