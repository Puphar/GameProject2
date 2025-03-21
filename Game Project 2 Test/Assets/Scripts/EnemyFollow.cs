using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;  // NavMeshAgent component to move the enemy
    public Transform player;    // Player's transform

    public Animator animator;   // Animator to control enemy animations
    public float detectionRange = 10.0f; // Range at which the enemy starts following the player

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.position);

        // If the player is within the detection range, start following
        if (distanceToPlayer < detectionRange)
        {
            enemy.SetDestination(player.position);  // Move towards the player
            animator.SetBool("Cancer_Walk", true);  // Play the walking animation
        }
        else
        {
            enemy.ResetPath();  // Stop moving if the player is out of range
            animator.SetBool("Cancer_Walk", false); // Stop the walking animation
        }
    }
}
