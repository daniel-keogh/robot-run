using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using TMPro;

public class GameController : SingletonController
{
    [SerializeField] private TextMeshProUGUI scoreText;

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

    private int playerScore = 0;
    private int pickupCount = 0;
    private string currentLevel;

    private bool powerUpEnabled;

    void Start()
    {
        if (scoreText)
        {
            RefreshScoreUI();
        }

        SetCurrentLevel();
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
            case SceneNames.LEVEL_TWO:
            case SceneNames.LEVEL_THREE:
                currentLevel = activeScene;
                break;
            default:
                break;
        }
    }
}
