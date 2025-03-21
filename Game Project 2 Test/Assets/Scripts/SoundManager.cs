using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Attack,
    Walk,
    Bow_load,
    Bow_Shoot,
    Scopio_Walk,
    Scopio_Attack,
    Fish_Attack
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource sfxObject;

    [SerializeField] private AudioClip[] soundlist;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        sfxObject = GetComponent<AudioSource>();
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTranform, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTranform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSFXClip(AudioClip[] audioClip, Transform spawnTranform, float volume)
    {
        int rand = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(sfxObject, spawnTranform.position, Quaternion.identity);
        audioSource.clip = audioClip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public static void PlaySound(SoundType sound, float volume)
    {
        instance.sfxObject.PlayOneShot(instance.soundlist[(int)sound], volume);
    }
}
