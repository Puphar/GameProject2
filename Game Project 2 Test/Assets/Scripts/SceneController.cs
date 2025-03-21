using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll(); // Clears all saved player data
        PlayerPrefs.Save();      // Ensures the changes are saved
        Debug.Log("Player data has been reset.");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
