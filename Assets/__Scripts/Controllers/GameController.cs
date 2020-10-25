using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int PlayerScore
    {
        get { return playerScore; }
    }

    private int playerScore = 0;

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void SetupSingleton()
    {
        // Check for any other objects of the same type
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject); // destroy the current object
        }
        else
        {
            DontDestroyOnLoad(gameObject); // persist across scenes
        }
    }

    public static GameController FindGameController()
    {
        GameController gc = FindObjectOfType<GameController>();

        if (!gc)
        {
            Debug.LogWarning("Missing GameController");
        }

        return gc;
    }
}
