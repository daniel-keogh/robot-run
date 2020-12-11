using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Config")]
public class LevelConfig : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] private List<Obstacle> obstacles;
    [SerializeField] private Pickup collectible;
    [SerializeField] private Tile environent;
    [SerializeField] private PowerUp powerUpPrefab;

    [Header("Level Unlock")]
    [SerializeField] private int pickupsUntilLevelUp = 10;

    [Header("Player")]
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float speedIncrementor = 0.5f;

    [Header("Spawning")]
    [SerializeField] [Range(1, 3)] private int minObstaclesPerRow = 1;
    [SerializeField] [Range(1, 3)] private int maxObstaclesPerRow = 2;

    // Getters
    public List<Obstacle> Obstacles { get => obstacles; }
    public Pickup Collectible { get => collectible; }
    public Tile Environment { get => environent; }
    public PowerUp PowerUpPrefab { get => powerUpPrefab; }

    public int PickupsUntilLevelUp { get => pickupsUntilLevelUp; }

    public float MaxSpeed { get => maxSpeed; }
    public float SpeedIncrementor { get => speedIncrementor; }

    public int MinObstaclesPerRow { get => minObstaclesPerRow; }
    public int MaxObstaclesPerRow { get => maxObstaclesPerRow; }
}
