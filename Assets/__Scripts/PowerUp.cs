using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 250f;
    [SerializeField] private AudioClip powerUpClip;

    private GameController gc;
    private SoundController sc;
    private float duration;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sc = FindObjectOfType<SoundController>();

        duration = gc.CurrentLevelConfig.PowerUpDuration;
    }

    void Update()
    {
        // Rotate the pickup around
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
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
