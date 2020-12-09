using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private int minPickups = 0;
    [SerializeField] private int maxPickups = 3;
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

    public void Spawn()
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

        return (Time.time - lastPowerUpTime) > timeBetweenPowerUps;
    }
}
