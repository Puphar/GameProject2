using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float healAmount = 100f;
    public float threshold; // Y threshold to trigger respawn
    private Vector3 playerPosition; // Last checkpoint position
    [SerializeField] private List<GameObject> checkPoints; // List of all possible checkpoints

    void Start()
    {
        // Initialize the player's position to their starting point
        playerPosition = transform.position;
    }

    void Update()
    {
        // If the player falls below the threshold, respawn at the last checkpoint
        if (transform.position.y < threshold)
        {
            RespawnPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the player hits a checkpoint, update their last known checkpoint position
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.RestoreHealth(healAmount);
            }
            playerPosition = other.transform.position;
            Debug.Log("Checkpoint reached");
        }
    }

    // Method to respawn the player at the last checkpoint
    public void RespawnPlayer()
    {
        // Respawn player at the last checkpoint
        transform.position = new Vector3(playerPosition.x, playerPosition.y + 2, playerPosition.z);
        Debug.Log("Player respawned at: " + playerPosition);
    }
}
