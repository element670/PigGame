using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio source")]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource soundEffects;

    [Header("AudioClip")]
    public AudioClip backgroundClip;
    public AudioClip whooshSound;
    public AudioClip death;
    void Start()
    {
        backgroundSource.clip = backgroundClip;
        backgroundSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip);
    }
    // Update is called once per frame
    
}
