using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioClip powerUpClip;

    private GameController gc;
    private SoundController sc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sc = FindObjectOfType<SoundController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            // Power up the player
            PowerUpEnabledEvent?.Invoke(duration);
            sc.PlayOneShot(powerUpClip);
            Destroy(gameObject);
        }
    }

    // Delegate type to use for event
    public delegate void PowerUpEnabled(float duration);

    // Static method to be implemented in the listener
    public static PowerUpEnabled PowerUpEnabledEvent;
}
