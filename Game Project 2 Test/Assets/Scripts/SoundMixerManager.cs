using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("master Volume", Mathf.Log10(level) * 20);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFX Volume", Mathf.Log10(level) * 20);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("music Volume", Mathf.Log10(level) * 20);
    }
}
