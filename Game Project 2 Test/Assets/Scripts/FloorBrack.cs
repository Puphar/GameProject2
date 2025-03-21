using System.Collections;
using UnityEngine;

public class FloorBrack : MonoBehaviour
{
    public GameObject floor;

    public float timeBeforeDestroy = 3f;  // Time before the floor breaks
    public float timeRespawn = 2f;        // Time before the floor respawns

    private bool playerOnBridge = false;  // Whether the player is on the floor
    private bool floorDisable = false;    // Whether the floor is disabled
    private float timer = 0f;             // Timer to track the time the player stays on the floor

    public AudioClip craking;  // Cracking sound when the player steps on it
    public AudioClip bracking; // Breaking sound when the floor breaks

    void Update()
    {
        if (playerOnBridge && !floorDisable)
        {
            timer += Time.deltaTime;

            if (timer >= timeBeforeDestroy)
            {
                // Disable the floor and start respawn
                floorDisable = true;
                floor.SetActive(false);
                StartCoroutine(Respawn());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFXClip(craking, transform, 1f);

            playerOnBridge = true;
            timer = 0f;  // Reset the timer when the player steps on the floor
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnBridge = false;
        }
    }

    private IEnumerator Respawn()
    {
        // Wait for the respawn time before re-enabling the floor
        yield return new WaitForSeconds(timeRespawn);

        // Re-enable the floor and reset the variables
        floor.SetActive(true);
        floorDisable = false;  // Allow the floor to break again
        timer = 0f;            // Reset the timer
    }
}
