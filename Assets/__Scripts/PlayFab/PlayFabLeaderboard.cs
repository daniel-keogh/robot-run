using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayFabLeaderboard : MonoBehaviour
{
    [Tooltip("Max number of items to display in the leaderboard")]
    [SerializeField] private int leaderboardCount = 25;
    [Tooltip("Level associated with this leaderboard screen")]
    [SerializeField] [Range(1, 3)] private int level;

    [Header("UI")]
    [SerializeField] private GameObject leaderboardItem;
    [SerializeField] private VerticalLayoutGroup layoutGroup;

    void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        string statName;

        switch (level)
        {
            case 1:
                statName = PlayFabStats.L1_HIGH_SCORE;
                break;
            case 2:
                statName = PlayFabStats.L2_HIGH_SCORE;
                break;
            case 3:
                statName = PlayFabStats.L3_HIGH_SCORE;
                break;
            default:
                return;
        }

        // Fetch the leaderboard for the level
        var request = new GetLeaderboardRequest
        {
            StartPosition = 0,
            StatisticName = statName,
            MaxResultsCount = leaderboardCount,
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnLeaderboardError);
    }

    private void OnGetLeaderboard(GetLeaderboardResult result)
    {
        foreach (var player in result.Leaderboard)
        {
            // Print out the results
            var obj = Instantiate(leaderboardItem, layoutGroup.transform, false);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = (
                $@"<align=left>{player.Position + 1}. {player.DisplayName}<line-height=0.001>
                <align=right>{string.Format("{0:n0}", player.StatValue)}"
            );
        }
    }

    private void OnLeaderboardError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
