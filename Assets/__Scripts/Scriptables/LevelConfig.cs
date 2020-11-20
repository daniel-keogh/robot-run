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

    public List<Obstacle> Obstacles { get => obstacles; }
    public Pickup Collectible { get => collectible; }
    public Tile Environment { get => environent; }
    public PowerUp PowerUpPrefab { get => powerUpPrefab; }

    public int PickupsUntilLevelUp { get => pickupsUntilLevelUp; }

    public float MaxSpeed { get => maxSpeed; }
    public float SpeedIncrementor { get => speedIncrementor; }
}
