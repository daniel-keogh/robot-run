using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static SoundController FindSoundController()
    {
        SoundController sc = FindObjectOfType<SoundController>();

        if (!sc)
        {
            Debug.LogWarning("Missing SoundController");
        }

        return sc;
    }
}
