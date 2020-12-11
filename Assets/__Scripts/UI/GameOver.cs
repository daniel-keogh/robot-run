using System.Collections;
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
            if (gc.CurrentLevel == SceneNames.LEVEL_ONE)
            {
                if (playFabStats.LevelOneHighScore < gc.PlayerScore)
                {
                    playFabStats.LevelOneHighScore = gc.PlayerScore;
                }
            }
            else if (gc.CurrentLevel == SceneNames.LEVEL_TWO)
            {
                if (playFabStats.LevelTwoHighScore < gc.PlayerScore)
                {
                    playFabStats.LevelTwoHighScore = gc.PlayerScore;
                }
            }
            else if (gc.CurrentLevel == SceneNames.LEVEL_THREE)
            {
                if (playFabStats.LevelThreeHighScore < gc.PlayerScore)
                {
                    playFabStats.LevelThreeHighScore = gc.PlayerScore;
                }
            }
            else
            {
                return;
            }

            playFabStats.SetStats();
        }
    }
}
