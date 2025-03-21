using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    public GameObject[] gamePoints; // Array of points in the game
    private int currentIndex = 0;  // Track the current point index

    void Update()
    {
        // Skip to the next point
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            SkipToNextPoint();
        }

        // Jump to a specific point by index
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            JumpToPoint(1);
        }

        // Reload the current scene
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    void SkipToNextPoint()
    {
        if (currentIndex < gamePoints.Length - 1)
        {
            currentIndex++;
            MoveToGamePoint(gamePoints[currentIndex]);
            Debug.Log($"Skipped to point: {gamePoints[currentIndex].name}");
        }
        else
        {
            Debug.Log("No more points to skip to.");
        }
    }

    void JumpToPoint(int index)
    {
        if (index >= 0 && index < gamePoints.Length)
        {
            currentIndex = index;
            MoveToGamePoint(gamePoints[currentIndex]);
            Debug.Log($"Jumped to point: {gamePoints[currentIndex].name}");
        }
        else
        {
            Debug.Log("Invalid point index.");
        }
    }

    void MoveToGamePoint(GameObject point)
    {
        // Logic to move the player or camera to the GameObject
        Transform player = GameObject.FindWithTag("Player").transform;
        player.position = point.transform.position;
    }

    void ReloadCurrentScene()
    {
        // Reload the current active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

        Debug.Log($"Reloaded scene: {currentScene.name}");
    }
}
