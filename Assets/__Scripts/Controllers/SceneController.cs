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
    private const string ANIM_TRIGGER = "Start";

    public void AuthMenu(bool animate = false) => ChangeScene(SceneNames.AUTH, animate);
    public void MainMenu(bool animate = false) => ChangeScene(SceneNames.MAIN_MENU, animate);
    public void Leaderboard(bool animate = false) => ChangeScene(SceneNames.LEADERBOARD, animate);
    public void LevelOne(bool animate = false) => ChangeScene(SceneNames.LEVEL_ONE, animate);
    public void LevelTwo(bool animate = false) => ChangeScene(SceneNames.LEVEL_TWO, animate);
    public void LevelThree(bool animate = false) => ChangeScene(SceneNames.LEVEL_THREE, animate);
    public void GameOver(bool animate = false) => ChangeScene(SceneNames.GAME_OVER, animate);

    public void ReplayGame(bool animate = false)
    {
        var gc = FindObjectOfType<GameController>();

        if (gc)
        {
            // Get the current level's scene name and reload it
            string level = gc.CurrentLevel;
            ChangeScene(level, animate);
        }
    }

    public void ChangeScene(string name, bool animate = false)
    {
        // Reset the GameController if going to a level
        // Since it's a Singleton it won't be destroyed automatically
        switch (name)
        {
            case SceneNames.LEVEL_ONE:
            case SceneNames.LEVEL_TWO:
            case SceneNames.LEVEL_THREE:
                FindObjectOfType<GameController>()?.ResetGame();
                break;
            default:
                break;
        }

        if (animate)
        {
            // Animate the scene transition with a coroutine
            StartCoroutine(SceneTransition(name));
        }
        else
        {
            SceneManager.LoadSceneAsync(name);
        }
    }

    private IEnumerator SceneTransition(string sceneName)
    {
        if (transitionAnimator)
        {
            // Show an animation
            transitionAnimator.SetTrigger(ANIM_TRIGGER);
        }

        yield return new WaitForSeconds(transitionDelay);

        SceneManager.LoadSceneAsync(sceneName);
    }
}
