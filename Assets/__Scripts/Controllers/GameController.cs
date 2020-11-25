using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities;
using TMPro;

public class GameController : SingletonController
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Level Config")]
    [SerializeField] private LevelConfig levelOne;
    [SerializeField] private LevelConfig levelTwo;
    [SerializeField] private LevelConfig levelThree;

    [Header("Events")]
    [SerializeField] private UnityEvent onLevelUnlocked;

    public int PlayerScore
    {
        get => playerScore;
    }

    public int PickupCount
    {
        get => pickupCount;
    }

    public string CurrentLevel
    {
        get => currentLevel;
    }

    public LevelConfig CurrentLevelConfig
    {
        get => currentLevelConfig;
    }

    private int playerScore = 0;
    private int pickupCount = 0;
    private bool leveledUp = false;
    private string currentLevel;

    private bool powerUpEnabled;
    private LevelConfig currentLevelConfig;

    void Awake()
    {
        SetCurrentLevel();
    }

    void Start()
    {
        if (scoreText)
        {
            RefreshScoreUI();
        }
    }

    private void OnEnable()
    {
        PowerUp.PowerUpEnabledEvent += OnPowerUpEnabled;
    }

    private void OnDisable()
    {
        PowerUp.PowerUpEnabledEvent -= OnPowerUpEnabled;
    }

    private void OnPowerUpEnabled(float duration)
    {
        StartCoroutine(StartPowerUpCountdown(duration));
    }

    private IEnumerator StartPowerUpCountdown(float duration)
    {
        powerUpEnabled = true;
        yield return new WaitForSeconds(duration);
        powerUpEnabled = false;
    }

    public void LogPickup(int points)
    {
        if (powerUpEnabled)
        {
            points *= 2;
        }

        playerScore += points;
        pickupCount++;

        RefreshScoreUI();

        if (!leveledUp && pickupCount >= currentLevelConfig.PickupsUntilLevelUp)
        {
            LevelUp();
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void RefreshScoreUI()
    {
        // Large numbers are separated by commas
        scoreText.text = string.Format("{0:n0}", playerScore);
    }

    private void SetCurrentLevel()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        switch (activeScene)
        {
            case SceneNames.LEVEL_ONE:
                currentLevelConfig = levelOne;
                break;
            case SceneNames.LEVEL_TWO:
                currentLevelConfig = levelTwo;
                break;
            case SceneNames.LEVEL_THREE:
                currentLevelConfig = levelThree;
                break;
            default:
                break;
        }

        if (currentLevelConfig != null)
        {
            currentLevel = activeScene;
        }
    }

    private void LevelUp()
    {
        if (currentLevel == SceneNames.LEVEL_ONE)
        {
            UnlockLevel(SceneNames.LEVEL_TWO);
        }
        else if (currentLevel == SceneNames.LEVEL_TWO)
        {
            UnlockLevel(SceneNames.LEVEL_THREE);
        }

        leveledUp = true;
    }

    private void UnlockLevel(string level)
    {
        bool isUnlocked = PlayerPrefs.HasKey(level);

        if (!isUnlocked)
        {
            PlayerPrefs.SetInt(level, 1);
            onLevelUnlocked?.Invoke();
        }
    }
}
