using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private GameController gc;
    private string originalText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gc = FindObjectOfType<GameController>();

        string score = "0";

        if (gc)
        {
            // Large numbers are separated by commas
            score = string.Format("{0:n0}", gc.PlayerScore);
        }

        originalText = scoreText.text;
        scoreText.text = $"{originalText}{score}";
    }
}
