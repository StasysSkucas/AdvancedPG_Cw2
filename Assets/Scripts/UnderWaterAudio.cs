using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterAudio : MonoBehaviour
{

    //Audio Script to Play Underwater Sound
    public AudioClip audioClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
