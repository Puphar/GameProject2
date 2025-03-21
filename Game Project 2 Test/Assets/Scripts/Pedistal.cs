using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pedistal : MonoBehaviour
{
    public GameObject requiredItem;  // The specific item needed for this pedestal
    public bool itemPlaced = false;  // To track if the correct item is placed
    public GameObject pivotplace;     // The exact position where the item should be placed

    // Automatically try to place the item when it enters the pedestal's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger zone is the correct item
        if (other.gameObject == requiredItem && !itemPlaced)
        {
            // Disable FloatingItem script if it exists
            FloatingItem floatingItem = other.gameObject.GetComponent<FloatingItem>();
            if (floatingItem != null)
            {
                floatingItem.enabled = false;
            }

            // Disable Rigidbody physics to stop the item from being affected by forces
            Rigidbody itemRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = true;  // Stops physics forces from acting on the item
            }

            // Mark the item as placed
            itemPlaced = true;

            // Position the item at the exact pivotplace location
            other.transform.position = pivotplace.transform.position;

            // Optionally, parent the item to the pedestal to keep it fixed in place
            other.transform.SetParent(transform);

            Debug.Log("Correct item placed on the pedestal");
        }
    }
}
