using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int points = 1;
    [SerializeField] private int powerupPoints = 2;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private AudioClip pickupClip;

    private GameController gc;
    private SoundController sc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sc = FindObjectOfType<SoundController>();
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            // The object has been picked up by the player
            gc.LogPickup(points);
            sc.PlayOneShot(pickupClip);
            Destroy(gameObject);
        }
    }
}
