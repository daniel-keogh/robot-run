using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
    }

    public bool IsMuted()
    {
        return audioSource.mute;
    }
}
