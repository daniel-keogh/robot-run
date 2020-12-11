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

    private void Spawn()
    {
        int obstaclesInRow = Random.Range(levelConfig.MinObstaclesPerRow, levelConfig.MaxObstaclesPerRow + 1);

        List<int> usedRows = new List<int>();

        for (int i = 0; i < obstaclesInRow; i++)
        {
            int spawnIndex;

            // Generate an available spawnIndex
            do
            {
                spawnIndex = Random.Range(0, spawnPoints.Count);
            }
            while (usedRows.Contains(spawnIndex));

            int obstacleIndex = Random.Range(0, levelConfig.Obstacles.Count);

            Instantiate<Obstacle>(
                levelConfig.Obstacles[obstacleIndex],
                spawnPoints[spawnIndex].transform.position,
                Quaternion.identity,
                transform
            );

            // keep track of used spawnIndexes
            usedRows.Add(spawnIndex);
        }
    }
}
