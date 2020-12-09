using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Tile : MonoBehaviour
{
    [SerializeField] float destoryDelay = 3f;

    private TileSpawner tileSpawner;

    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            // Once the player has left this tile destroy it after a short delay.
            // Then spawn a new one in its place at the end of the road
            tileSpawner.Spawn();
            Destroy(gameObject, destoryDelay);
        }
    }
}
