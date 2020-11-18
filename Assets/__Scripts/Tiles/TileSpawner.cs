using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;

    private SpawnPoint spawnPoint;

    void Start()
    {
        // Get active tiles & set the next spawn point
        var tiles = GetComponentsInChildren<Tile>();
        spawnPoint = tiles[tiles.Length - 1].GetComponentInChildren<SpawnPoint>();
    }

    void Update()
    {

    }

    public void SpawnTile()
    {
        var tile = Instantiate(tilePrefab, spawnPoint.transform.position, Quaternion.identity);
        tile.transform.SetParent(transform);

        // Set current tile as next spawn point
        spawnPoint = tile.GetComponentInChildren<SpawnPoint>();
    }
}
