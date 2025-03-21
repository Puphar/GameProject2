using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFishSFX : MonoBehaviour
{
    public void PlayFishAttack()
    {
        SoundManager.PlaySound(SoundType.Fish_Attack, 1);
    }
}
