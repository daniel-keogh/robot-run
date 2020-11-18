using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : Spawner<Tile>
{
    private SpawnPoint spawnPoint;

    void Start()
    {
        // Get active tiles & set the next spawn point
        var tiles = GetComponentsInChildren<Tile>();
        spawnPoint = tiles[tiles.Length - 1].GetComponentInChildren<SpawnPoint>();
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
