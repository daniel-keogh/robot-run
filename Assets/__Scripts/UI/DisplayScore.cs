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

    void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gc = Controller.Find<GameController>();

        originalText = scoreText.text;
    }

    void Update()
    {
        string score = "0";

        if (gc)
        {
            // Large numbers are separated by commas
            score = string.Format("{0:n0}", gc.PlayerScore);
        }

        scoreText.text = $"{originalText}{score}";
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
}
