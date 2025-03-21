using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyMaxhealth = 100f;

    [SerializeField] private AudioClip hit;
    void Start()
    {
        enemyHealth = enemyMaxhealth;
    }

    void Update()
    {
        EnemyDead();
    }

    public void EnemyTakeDamage(float playerDamage)
    {
        enemyHealth -= playerDamage;
        SoundManager.instance.PlaySFXClip(hit, transform,1f);

    }

    void EnemyDead()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
