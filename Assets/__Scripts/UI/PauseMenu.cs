﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;

    private KeyCode pauseKey = KeyCode.Escape;
    private SceneController sc;

    private static bool isPaused = false;

    public static bool IsPaused
    {
        get => isPaused;
    }

    void Start()
    {
        menuUI.SetActive(false);
        sc = SceneController.FindSceneController();
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            // Pause/Unpause
            if (isPaused)
            {
                menuUI.GetComponent<CountDown>()?.BeginCountDown();
            }
            else
            {
                SetPauseStatus(!isPaused);
            }
        }
    }

    public void Pause() => SetPauseStatus(true);
    public void Resume() => SetPauseStatus(false);

    public void NewGame()
    {
        if (sc)
        {
            Time.timeScale = 1;
            sc.PlayGame();
        }
    }

    public void QuitToMainMenu()
    {
        if (sc)
        {
            Time.timeScale = 1;
            sc.GoToMainMenu();
        }
    }

    private void SetPauseStatus(bool status)
    {
        if (LevelMessage.IsShowing)
            return;

        Time.timeScale = status ? 0 : 1;
        menuUI.SetActive(status);
        isPaused = status;
    }
}
