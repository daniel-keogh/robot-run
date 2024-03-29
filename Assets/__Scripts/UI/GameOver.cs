﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        var gc = FindObjectOfType<GameController>();
        var playFabStats = FindObjectOfType<PlayFabStats>();

        if (gc && playFabStats)
        {
            switch (gc.CurrentLevel)
            {
                case SceneNames.LEVEL_ONE:
                    if (gc.PlayerScore > playFabStats.LevelOneHighScore)
                        playFabStats.LevelOneHighScore = gc.PlayerScore;
                    break;
                case SceneNames.LEVEL_TWO:
                    if (gc.PlayerScore > playFabStats.LevelTwoHighScore)
                        playFabStats.LevelTwoHighScore = gc.PlayerScore;
                    break;
                case SceneNames.LEVEL_THREE:
                    if (gc.PlayerScore > playFabStats.LevelThreeHighScore)
                        playFabStats.LevelThreeHighScore = gc.PlayerScore;
                    break;
                default:
                    return;
            }

            // Update the players stats since they have a new high score
            playFabStats.SetStats();
        }
    }
}
