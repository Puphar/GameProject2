using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBridge : MonoBehaviour
{
    public GameObject destroybridge;

    public float timeBeforeDestroy = 3f;

    private bool playerOnBridge = false;
    private float timer = 0f;

    [SerializeField] private AudioClip BridgeClip;
    [SerializeField] private AudioClip destroyBridgeClip;

    void Update()
    {
        if (playerOnBridge)
        {
            timer += Time.deltaTime;

            if (timer >= timeBeforeDestroy)
            {
                SoundManager.instance.PlaySFXClip(destroyBridgeClip, transform, 1f);
                Instantiate(destroybridge, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySFXClip(BridgeClip, transform, 1f);
            playerOnBridge = true;
            timer = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOnBridge = false;
        }
    }
}
