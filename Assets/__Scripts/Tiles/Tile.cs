using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] float destoryDelay = 3f;

    private TileSpawner tileSpawner;

    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tileSpawner.SpawnTile();
            Destroy(gameObject, destoryDelay);
        }
    }
}
