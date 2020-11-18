using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.tag == "Player")
        {
            tileSpawner.Spawn();
            Destroy(gameObject, destoryDelay);
        }
    }
}
