using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Utilities;

public class SceneController : MonoBehaviour
{
    [Header("Animate Scene Transitions")]
    [SerializeField] private float transitionDelay = 1f;
    [SerializeField] private Animator transitionAnimator;

    // Animation triggers
    private const string ANIMATOR_TRIGGER = "Start";

    public void PlayGame()
    {
        // Reset the GameController singleton before re-playing.
        FindObjectOfType<GameController>()?.ResetGame();

        StartCoroutine(SceneTransition(SceneNames.LEVEL_ONE));
    }

    public void GoToMainMenu()
    {
        StartCoroutine(SceneTransition(SceneNames.MAIN_MENU));
    }

    public void LoadLevel(int num)
    {
        string scene;

        switch (num)
        {
            case 1:
                scene = SceneNames.LEVEL_ONE;
                break;
            case 2:
                scene = SceneNames.LEVEL_TWO;
                break;
            case 3:
                scene = SceneNames.LEVEL_THREE;
                break;
            default:
                throw new System.ArgumentException($"Level {num} doesn't exist");
        }

        StartCoroutine(SceneTransition(scene));
    }

    private IEnumerator SceneTransition(string sceneName)
    {
        if (transitionAnimator)
        {
            // Show an animation
            transitionAnimator.SetTrigger(ANIMATOR_TRIGGER);
        }

        yield return new WaitForSeconds(transitionDelay);

        SceneManager.LoadSceneAsync(sceneName);
    }

    public static SceneController FindSceneController()
    {
        SceneController sc = FindObjectOfType<SceneController>();

        if (!sc)
        {
            Debug.LogWarning("Missing SceneController");
        }

        return sc;
    }
}
