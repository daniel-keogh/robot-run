using System.Collections;
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
        // Make sure the menu is hidden on startup
        menuUI.SetActive(false);

        sc = FindObjectOfType<SceneController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            // Pause/Unpause
            if (isPaused)
            {
                // Show a countdown on unpause
                menuUI.GetComponent<CountDown>()?.BeginCountDown();
            }
            else
            {
                SetPauseStatus(!isPaused);
            }
        }
    }

    void OnDisable()
    {
        // Make sure the static timeScale and isPaused variables are reset
        // before leaving the scene
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() => SetPauseStatus(true);
    public void Resume() => SetPauseStatus(false);

    public void NewGame()
    {
        if (sc)
        {
            Time.timeScale = 1f;
            sc.ReplayGame(true);
        }
    }

    public void QuitToMainMenu()
    {
        if (sc)
        {
            Time.timeScale = 1f;
            sc.MainMenu(true);
        }
    }

    private void SetPauseStatus(bool status)
    {
        if (LevelMessage.IsShowing)
            return; // don't pause if the pre-level message is showing

        Time.timeScale = status ? 0 : 1;
        menuUI.SetActive(status);
        isPaused = status;
    }
}
