using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    [SerializeField] Transform pointA;   // Starting point
    [SerializeField] Transform pointB;   // Ending point
    [SerializeField] float speed = 10f;   // Speed at which the platform moves
    [SerializeField] float waitTime = 1f; // Time to wait at each point

    private Vector3 targetPosition;
    private bool isWaiting = false;

    void Start()
    {
        // Initially set the platform to start at point A
        targetPosition = pointB.position;
    }

    void Update()
    {
        // If the platform is currently not waiting, move it towards the target position (point A or B)
        if (!isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // If the platform reaches the target position, switch to the other point and wait before continuing
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Start the wait coroutine to pause for the specified wait time
                StartCoroutine(WaitAtPoint());
            }
        }
    }

    private IEnumerator WaitAtPoint()
    {
        // Mark that the platform is waiting
        isWaiting = true;

        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);

        // After waiting, switch direction
        targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;

        // Resume movement
        isWaiting = false;
    }
}
