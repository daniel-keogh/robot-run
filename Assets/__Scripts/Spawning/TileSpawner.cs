using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : Spawner<Tile>
{
    [SerializeField] private int numDisabledAtStart = 2;

    private SpawnPoint spawnPoint;

    void Start()
    {
        // Get active tiles & set the next spawn point
        var tiles = GetComponentsInChildren<Tile>();
        spawnPoint = tiles[tiles.Length - 1].GetComponentInChildren<SpawnPoint>();

        // Disable the first few tiles
        for (int i = 0; i < numDisabledAtStart; i++)
        {
            tiles[i].GetComponentInChildren<ObstacleSpawner>().gameObject.SetActive(false);
            tiles[i].GetComponentInChildren<PickupSpawner>().gameObject.SetActive(false);
        }
    }

    public override void Spawn()
    {
        var tile = Instantiate(
            prefab,
            spawnPoint.transform.position,
            Quaternion.identity,
            transform
        );

        // Set current tile as next spawn point
        spawnPoint = tile.GetComponentInChildren<SpawnPoint>();
    }
}
