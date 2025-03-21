using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBowSFX : MonoBehaviour
{
    public void Bow_load()
    {
        SoundManager.PlaySound(SoundType.Bow_load, 1);
    }

    public void Shoot()
    {
        SoundManager.PlaySound(SoundType.Bow_Shoot, 1);
    }
}
