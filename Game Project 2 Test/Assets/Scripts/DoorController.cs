using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Pedistal pedistal1;         // First pedestal reference
    public Pedistal pedistal2;         // Second pedestal reference

    [SerializeField] private AudioClip DoordestroyClip;

    private void Update()
    {
        // Check if both pedestals have had their items placed and the door hasn't opened yet
        if (pedistal1.itemPlaced && pedistal2.itemPlaced)
        {
            SoundManager.instance.PlaySFXClip(DoordestroyClip, transform, 1f);
            Destroy(gameObject);
        }
    }
}
