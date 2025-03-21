using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 100f;
    public float health;

    // Reference to the PlayerRespawn script
    private PlayerRespawn playerRespawn;

    // Reference to the player's renderer (for changing the model color)
    public Renderer playerRenderer;  // Public reference, set this in the Inspector
    private Color originalColor;

    public AudioClip hitSFX;

    private void Start()
    {
        health = maxHealth;
        playerRespawn = GetComponent<PlayerRespawn>(); // Get the PlayerRespawn component on the same GameObject
        UpdateHealthBar();

        // Ensure we have a player renderer assigned
        if (playerRenderer != null)
        {
            // Store the original color of the player's model
            originalColor = playerRenderer.material.color;
        }
        else
        {
            Debug.LogWarning("No Renderer assigned for player model!");
        }
    }

    void Update()
    {
        // Clamp health to be between 0 and maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);

        // Update the health bar only if necessary
        if (healthBar.value != health)
        {
            UpdateHealthBar();
        }

        // Check if the player's health has reached 0
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        SoundManager.instance.PlaySFXClip(hitSFX, transform, 1f);

        health -= damage;
        Debug.Log("Player took damage: " + damage);

        // Change the player's model color to red when taking damage
        ChangePlayerColor(Color.red);

        // Optionally, reset the color back to normal after a short delay
        StartCoroutine(ResetPlayerColor());
    }

    // Optional method to restore health
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        Debug.Log("Player healed: " + healAmount);
    }

    private void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    // Method to handle player death
    private void HandleDeath()
    {
        Debug.Log("Player is dead. Respawning at checkpoint...");
        health = maxHealth; // Reset health to max on respawn
        playerRespawn.RespawnPlayer(); // Call the respawn method in PlayerRespawn script
    }

    // Change the player's model color
    private void ChangePlayerColor(Color color)
    {
        if (playerRenderer != null)
        {
            playerRenderer.material.color = color;
        }
    }

    // Coroutine to reset the player's color back to the original color after a short delay
    private IEnumerator ResetPlayerColor()
    {
        yield return new WaitForSeconds(0.5f); // Wait for half a second before resetting
        if (playerRenderer != null)
        {
            playerRenderer.material.color = originalColor;
        }
    }
}
