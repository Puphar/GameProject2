using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPlatform : MonoBehaviour
{
    public float spinspeedX;
    public float spinspeedY;
    public float spinspeedZ;

    void Update()
    {
        transform.Rotate(spinspeedX * Time.deltaTime, spinspeedY * Time.deltaTime, spinspeedZ * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
