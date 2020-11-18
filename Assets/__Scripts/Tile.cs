using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Tile : MonoBehaviour
{
    [SerializeField] float destoryDelay = 3f;

    private Spawner<Tile> tileSpawner;

    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            tileSpawner.Spawn();
            Destroy(gameObject, destoryDelay);
        }
    }
}
