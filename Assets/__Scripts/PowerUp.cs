using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    private GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            // Power up the player
            PowerUpEnabledEvent?.Invoke(duration);
            Destroy(gameObject);
        }
    }

    // Delegate type to use for event
    public delegate void PowerUpEnabled(float duration);

    // Static method to be implemented in the listener
    public static PowerUpEnabled PowerUpEnabledEvent;
}
