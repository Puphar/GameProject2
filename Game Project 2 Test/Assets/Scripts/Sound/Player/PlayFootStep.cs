using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootStep : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.PlaySound(SoundType.Walk, 1);
    }
}
