using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int points = 1;
    [SerializeField] private int powerupPoints = 2;

    private GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            // The object has been picked up by the player
            gc.LogPickup(points);
            Destroy(gameObject);
        }
    }
}
