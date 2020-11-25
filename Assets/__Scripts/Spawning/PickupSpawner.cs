using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Spawner<Pickup>
{
    private IList<SpawnPoint> spawnPoints;

    private LevelConfig levelConfig;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();

        var gc = FindObjectOfType<GameController>();
        levelConfig = gc.CurrentLevelConfig;

        Spawn();
    }

    public override void Spawn()
    {
        int rIndex = Random.Range(0, spawnPoints.Count);

        Instantiate<Pickup>(
            levelConfig.Collectible,
            spawnPoints[rIndex].transform.position,
            Quaternion.identity,
            transform
        );
    }
}
