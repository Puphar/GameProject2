using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScopioSFX : MonoBehaviour
{
    public void Walk()
    {
        SoundManager.PlaySound(SoundType.Scopio_Walk, 1);
    }

    public void Attack()
    {
        SoundManager.PlaySound(SoundType.Scopio_Attack, 1);
    }
}
