using UnityEngine;

public class DonutAttack : MonoBehaviour
{
    // Define the inner and outer radii of the donut shape
    public float innerRadius = 5f;
    public float outerRadius = 10f;

    // Reference to the player's transform
    public Transform player;

    // Timer for attack interval
    public float attackInterval = 5f;
    private float attackCooldown = 0f;

    // Warning duration before the attack
    public float warningDuration = 2f;
    private bool warningActive = false;

    // Visual warning indicators (optional)
    public GameObject warningCirclePrefab; // A prefab for the warning area (e.g., a circle on the ground)
    private GameObject warningInstance;

    public float attackDamage = 25f;
    private HealthManager healthManager;

    private void Start()
    {
        // Initialize HealthManager from player if it's not assigned
        if (player != null)
        {
            healthManager = player.GetComponent<HealthManager>();

            if (healthManager == null)
            {
                Debug.LogError("No HealthManager component found on player!");
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned in DonutAttack.");
        }
    }

    void Update()
    {
        // Handle attack cooldown
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            // Trigger the warning if it's not active
            if (!warningActive)
            {
                StartWarningPhase();
            }
        }

        // Check if warning phase is over and trigger the attack
        if (warningActive && attackCooldown <= 0)
        {
            PerformDonutAttack();
        }
    }

    void StartWarningPhase()
    {
        // Activate warning phase
        warningActive = true;
        attackCooldown = warningDuration; // Set cooldown to warning duration

        // Create warning visual if assigned
        if (warningCirclePrefab != null)
        {
            warningInstance = Instantiate(warningCirclePrefab, transform.position, Quaternion.identity);
            warningInstance.transform.localScale = new Vector3(outerRadius * 2, 1, outerRadius * 2); // Adjust size for outer radius
        }

        Debug.Log("Warning: Donut attack incoming!");
    }

    void PerformDonutAttack()
    {
        // End the warning phase
        warningActive = false;
        attackCooldown = attackInterval; // Reset cooldown for the next attack

        // Destroy the warning visual
        if (warningInstance != null)
        {
            Destroy(warningInstance);
        }

        // Ensure the player and health manager are set
        if (player == null || healthManager == null)
        {
            return;
        }

        // Calculate distance from enemy (this object) to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within the donut attack range
        if (distanceToPlayer > innerRadius && distanceToPlayer <= outerRadius)
        {
            // Player is within the donut attack zone
            Debug.Log("Player hit by donut attack!");
            // Apply damage or effects here
            ApplyDamageToPlayer();
        }
        else
        {
            Debug.Log("Player is outside the donut attack range.");
        }
    }

    void ApplyDamageToPlayer()
    {
        // Logic to apply damage to the player
        if (healthManager != null)
        {
            healthManager.TakeDamage(attackDamage);
            Debug.Log("Damage applied to player: " + attackDamage);
        }
    }

    // For visualization in the Unity Editor
    void OnDrawGizmosSelected()
    {
        // Draw the inner and outer radius in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, innerRadius); // Inner circle (no damage zone)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, outerRadius); // Outer circle (damage zone)
    }
}