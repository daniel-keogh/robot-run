using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Tooltip("Minimum number of pickups per tile")]
    [SerializeField] private int minPickups = 0;
    [Tooltip("Maximum number of pickups per tile")]
    [SerializeField] private int maxPickups = 3;
    [Tooltip("Adds some padding between pickups")]
    [SerializeField] private float spaceBetweenPickups = 3f;

    [Header("Power Ups")]
    [SerializeField] private float timeBetweenPowerUps = 30f;

    private IList<SpawnPoint> spawnPoints;
    private GameController gc;
    private LevelConfig levelConfig;

    private static float lastPowerUpTime;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();

        gc = FindObjectOfType<GameController>();
        levelConfig = gc.CurrentLevelConfig;

        Spawn();
    }

    private void Spawn()
    {
        int rIndex = Random.Range(0, spawnPoints.Count);

        if (PowerUpTime())
        {
            Instantiate<PowerUp>(
                levelConfig.PowerUpPrefab,
                spawnPoints[rIndex].transform.position,
                Quaternion.identity,
                transform
            );

            lastPowerUpTime = Time.time;
        }
        else
        {
            int numToSpawn = Random.Range(minPickups, maxPickups);

            for (int i = 0; i < numToSpawn; i++)
            {
                var pos = spawnPoints[rIndex].transform.position;
                // Add some padding between pickups
                pos.z += (spaceBetweenPickups * i);

                Instantiate<Pickup>(
                    levelConfig.Collectible,
                    pos,
                    Quaternion.identity,
                    transform
                );
            }
        }
    }

    private bool PowerUpTime()
    {
        if (gc.PowerUpEnabled)
        {
            lastPowerUpTime = Time.time;
            return false;
        }

        // Check if the player is due another power up
        return (Time.time - lastPowerUpTime) > timeBetweenPowerUps;
    }
}
