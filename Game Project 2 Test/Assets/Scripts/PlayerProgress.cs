using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    private int highestLevelUnlocked = 1;

    private void Start()
    {
        // Load the highest unlocked level
        highestLevelUnlocked = PlayerPrefs.GetInt("HighestLevelUnlocked", 1);
    }

    public bool IsLevelUnlocked(int levelID)
    {
        return levelID <= highestLevelUnlocked;
    }

    public void UnlockNextLevel(int currentLevelID)
    {
        if (currentLevelID >= highestLevelUnlocked)
        {
            highestLevelUnlocked = currentLevelID + 1;
            PlayerPrefs.SetInt("HighestLevelUnlocked", highestLevelUnlocked);
            PlayerPrefs.Save();
        }
    }

    // Reset progress to the initial state
    public void ResetProgress()
    {
        highestLevelUnlocked = 1;
        PlayerPrefs.SetInt("HighestLevelUnlocked", highestLevelUnlocked);
        PlayerPrefs.Save(); // Save the reset state
    }
}
