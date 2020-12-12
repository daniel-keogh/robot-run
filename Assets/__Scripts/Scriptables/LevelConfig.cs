using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Config")]
public class LevelConfig : ScriptableObject
{
    [Header("Prefabs")]
    [Tooltip("Obstacles for use in this level")]
    [SerializeField] private List<Obstacle> obstacles;
    [Tooltip("Pickups for use in this level")]
    [SerializeField] private Pickup collectible;
    [Tooltip("Set the tiles that will be spawned for this level")]
    [SerializeField] private Tile environent;
    [SerializeField] private PowerUp powerUpPrefab;
    [Tooltip("Content that's spawned beside the tiles")]
    [SerializeField] private List<GameObject> sideContent;

    [Header("Level Unlock")]
    [Tooltip("Number of pickups required to unlock the next level")]
    [SerializeField] private int pickupsUntilLevelUp = 10;

    [Header("Player")]
    [Tooltip("Player's starting run speed")]
    [SerializeField] private float startSpeed = 15f;
    [Tooltip("Maximum speed the player can reach")]
    [SerializeField] private float maxSpeed = 50f;
    [Tooltip("Defines how much the player's speed is increased at any one time")]
    [SerializeField] private float speedIncrementor = 0.5f;

    [Header("Spawning")]
    [Tooltip("Minimum number of obstacles that can be spawned in a given row")]
    [SerializeField] [Range(1, 3)] private int minObstaclesPerRow = 1;
    [Tooltip("Maximum number of obstacles that can be spawned in a given row")]
    [SerializeField] [Range(1, 3)] private int maxObstaclesPerRow = 2;

    [Header("Power Ups")]
    [SerializeField] private float powerUpDuration = 10f;


    // Getters
    public List<Obstacle> Obstacles { get => obstacles; }
    public Pickup Collectible { get => collectible; }
    public Tile Environment { get => environent; }
    public PowerUp PowerUpPrefab { get => powerUpPrefab; }
    public List<GameObject> SideContent { get => sideContent; }

    public int PickupsUntilLevelUp { get => pickupsUntilLevelUp; }

    public float StartSpeed { get => startSpeed; }
    public float MaxSpeed { get => maxSpeed; }
    public float SpeedIncrementor { get => speedIncrementor; }

    public int MinObstaclesPerRow { get => minObstaclesPerRow; }
    public int MaxObstaclesPerRow { get => maxObstaclesPerRow; }

    public float PowerUpDuration { get => powerUpDuration; }
}
