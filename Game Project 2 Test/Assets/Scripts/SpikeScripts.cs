using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScripts : MonoBehaviour
{
    public int spikedamage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(spikedamage);
        }
    }
}
