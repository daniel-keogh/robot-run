using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabStats : MonoBehaviour
{
    private static PlayFabStats instance;

    private int playerLevel;
    private int levelOneHighScore;
    private int levelTwoHighScore;
    private int levelThreeHighScore;

    public const string PLAYER_LEVEL = "PlayerLevel";
    public const string L1_HIGH_SCORE = "L1HighScore";
    public const string L2_HIGH_SCORE = "L2HighScore";
    public const string L3_HIGH_SCORE = "L3HighScore";

    public int PlayerLevel
    {
        get => playerLevel;
        set => playerLevel = value;
    }

    public int LevelOneHighScore
    {
        get => levelOneHighScore;
        set => levelOneHighScore = value;
    }

    public int LevelTwoHighScore
    {
        get => levelTwoHighScore;
        set => levelTwoHighScore = value;
    }

    public int LevelThreeHighScore
    {
        get => levelThreeHighScore;
        set => levelThreeHighScore = value;
    }

    void Awake()
    {
        // Make this object a singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetStats()
    {
        // Update this players statistics
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = PLAYER_LEVEL, Value = playerLevel },
                new StatisticUpdate { StatisticName = L1_HIGH_SCORE, Value = levelOneHighScore },
                new StatisticUpdate { StatisticName = L2_HIGH_SCORE, Value = levelTwoHighScore },
                new StatisticUpdate { StatisticName = L3_HIGH_SCORE, Value = levelThreeHighScore },
            }
        },
        null,
        error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    public void OnGetStats(GetPlayerStatisticsResult result)
    {
        // Store the results that were fetched
        foreach (var stat in result.Statistics)
        {
            switch (stat.StatisticName)
            {
                case PLAYER_LEVEL:
                    playerLevel = stat.Value;
                    break;
                case L1_HIGH_SCORE:
                    levelOneHighScore = stat.Value;
                    break;
                case L2_HIGH_SCORE:
                    levelTwoHighScore = stat.Value;
                    break;
                case L3_HIGH_SCORE:
                    levelThreeHighScore = stat.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
