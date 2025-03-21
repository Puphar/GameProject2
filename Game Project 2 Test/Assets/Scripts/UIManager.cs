using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage previosStage;
    public RawImage currentStage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            previosStage.gameObject.SetActive(false);
            currentStage.gameObject.SetActive(true);
        }
    }
}
