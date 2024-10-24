using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeCorrector : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume;

    void Awake()
    {
        audioSource.volume = volume;
    }

}
