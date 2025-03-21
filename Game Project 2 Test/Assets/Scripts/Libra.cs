using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
    public float attackInterval = 3f;  // Time interval between attacks
    private float currentAttackTime;    // Timer to track attack cooldown

    public Animator anim;

    private void Start()
    {
        // Initialize the attack timer
        currentAttackTime = attackInterval;
    }

    private void Update()
    {
        // Decrease the attack timer only if it's greater than 0
        if (currentAttackTime > 0)
        {
            currentAttackTime -= Time.deltaTime;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        // Check if the player enters the attack area and if the attack interval is over
        if (other.CompareTag("Player") && currentAttackTime <= 0)
        {
            // Trigger attack animation
            anim.SetTrigger("Attack");

            // Reset the attack timer for the next attack
            currentAttackTime = attackInterval;
        }
    }
}
