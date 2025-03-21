using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float timer = 5f;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed;

    private void Update()
    {
        TurretShoot();
    }

    void TurretShoot()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0 ) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRB = bulletObj.GetComponent<Rigidbody>();
        bulletRB.velocity = spawnPoint.forward * bulletSpeed;
        Destroy(bulletObj, 2f);
    }
}
