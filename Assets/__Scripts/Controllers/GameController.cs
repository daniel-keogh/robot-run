using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonController
{
    public int PlayerScore
    {
        get => playerScore;
    }

    private int playerScore = 0;

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
