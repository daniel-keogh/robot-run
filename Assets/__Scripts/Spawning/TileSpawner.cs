﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Tooltip("The number of tiles that won't have any obstacles when the level begins")]
    [SerializeField] private int numDisabledAtStart = 2;

    private SpawnPoint spawnPoint;
    private LevelConfig levelConfig;

    void Start()
    {
        // Get active tiles & set the next spawn point
        var tiles = GetComponentsInChildren<Tile>();
        spawnPoint = tiles[tiles.Length - 1].GetComponentInChildren<SpawnPoint>();

        var gc = FindObjectOfType<GameController>();
        levelConfig = gc.CurrentLevelConfig;

        // Disable the first few tiles
        for (int i = 0; i < numDisabledAtStart; i++)
        {
            tiles[i].GetComponentInChildren<ObstacleSpawner>().gameObject.SetActive(false);
            tiles[i].GetComponentInChildren<PickupSpawner>().gameObject.SetActive(false);
        }
    }

    public void Spawn()
    {
        var tile = Instantiate(
            levelConfig.Environment,
            spawnPoint.transform.position,
            Quaternion.identity,
            transform
        );

        // Set current tile as next spawn point
        spawnPoint = tile.GetComponentInChildren<SpawnPoint>();
    }
}
