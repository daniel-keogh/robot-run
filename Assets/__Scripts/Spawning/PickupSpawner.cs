using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Spawner<Pickup>
{
    private IList<SpawnPoint> spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Spawn();
    }

    public override void Spawn()
    {
        int rIndex = Random.Range(0, spawnPoints.Count);

        Instantiate(
            prefab,
            spawnPoints[rIndex].transform.position,
            Quaternion.identity,
            transform
        );
    }
}
