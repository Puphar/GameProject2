using System.Collections;
using UnityEngine;

public class ScorpionAttack : MonoBehaviour
{
    public GameObject attackBox;
    public float scorpionDamage = 25f;       // Damage dealt by the scorpion's attack
    public float poisonDamage = 5f;          // Damage per tick from poison
    public float poisonDuration = 5f;        // Duration for which poison lasts
    public float poisonTickInterval = 1f;    // Time interval between poison ticks
    public float attackCooldown = 2f;        // Cooldown time between attacks

    private float currentCooldown = 0f;

    private void Update()
    {
        // Reduce cooldown over time
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && currentCooldown <= 0)
        {
            // Start the attack sequence
            StartCoroutine(ShowAttackBox());

            // Deal immediate damage to the player
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(scorpionDamage);

                // Start poison effect on the player
                StartCoroutine(ApplyPoison(playerHealth));
            }

            // Reset cooldown
            currentCooldown = attackCooldown;
        }
    }

    // Coroutine to show the attack box for 1 second
    IEnumerator ShowAttackBox()
    {
        attackBox.SetActive(true);  // Show attack box
        yield return new WaitForSeconds(1f); // Wait for 1 second
        attackBox.SetActive(false); // Hide attack box
    }

    // Coroutine to apply poison damage over time
    IEnumerator ApplyPoison(HealthManager playerHealth)
    {
        float elapsed = 0f;

        // Apply poison damage for the duration of the poison
        while (elapsed < poisonDuration)
        {
            playerHealth.TakeDamage(poisonDamage);  // Deal poison damage
            yield return new WaitForSeconds(poisonTickInterval);  // Wait for next tick
            elapsed += poisonTickInterval;
        }
    }
}
