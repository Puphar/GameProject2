using UnityEngine;

public class TiltingPlatform : MonoBehaviour
{
    public Transform platformPivot; // The pivot point of the platform
    public float tiltSpeed = 5.0f; // Speed at which the platform tilts
    public float tiltLimit = 15.0f; // Maximum angle the platform can tilt

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerPos = other.transform.position;
            Vector3 pivotPos = platformPivot.position;

            // Calculate the direction to tilt based on the player's position
            float tiltDirection = playerPos.x - pivotPos.x;

            // Apply a tilting force or rotation based on the player's position
            rb.AddTorque(Vector3.forward * tiltDirection * tiltSpeed);
        }
    }

    void FixedUpdate()
    {
        // Limit the platform's tilt angle
        float currentTilt = transform.localEulerAngles.z;
        if (currentTilt > tiltLimit && currentTilt < 180.0f)
        {
            rb.rotation = Quaternion.Euler(0, 0, tiltLimit);
        }
        else if (currentTilt < 360.0f - tiltLimit && currentTilt > 180.0f)
        {
            rb.rotation = Quaternion.Euler(0, 0, 360.0f - tiltLimit);
        }
    }
}
