using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
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

    public void Spawn()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        int obstacleIndex = Random.Range(0, levelConfig.Obstacles.Count);

        Instantiate<Obstacle>(
            levelConfig.Obstacles[obstacleIndex],
            spawnPoints[spawnIndex].transform.position,
            Quaternion.identity,
            transform
        );
    }
}
