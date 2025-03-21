using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike180 : MonoBehaviour
{
    // Rotation speed (degrees per second)
    public float rotationSpeed = 50f;

    // Start and end angles (for 180 degrees rotation back and forth)
    public float startAngle = 0f;  // Default start at 0 degrees
    public float endAngle = 180f;  // Default end at 180 degrees

    // A flag to invert the rotation direction
    public bool invertRotation = false;  // Set this to true for opposite direction

    void Update()
    {
        // Use Mathf.PingPong to create a back and forth rotation effect
        float angle = Mathf.PingPong(Time.time * rotationSpeed, endAngle - startAngle) + startAngle;

        // If invertRotation is true, flip the start and end angles
        if (invertRotation)
        {
            // Swap the start and end angles to rotate in the opposite direction
            angle = startAngle + endAngle - angle;
        }

        // Apply the rotation to the object around its Z-axis
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -angle);
    }
}
