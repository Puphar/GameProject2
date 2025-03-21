using UnityEngine;
using System.Collections; // Required for Coroutines

public class CrabAttack : MonoBehaviour
{
    public int crabDamage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthManager>().TakeDamage(crabDamage);
        }
    }

}
