using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonController
{
    public int PlayerScore
    {
        get => playerScore;
    }

    public int PickupCount
    {
        get => pickupCount;
    }

    private int playerScore = 0;
    private int pickupCount = 0;

    private bool powerUpEnabled;

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

        pickupCount += points;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
