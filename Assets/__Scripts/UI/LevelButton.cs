using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class LevelButton : MonoBehaviour
{
    [Header("Locked")]
    [Tooltip("The level number associated with this button")]
    [SerializeField] [Range(1, 3)] private int levelNumber = 1;
    [Tooltip("The colour the button will have if its level is locked")]
    [SerializeField] private Color lockedColor;

    [Header("Popup")]
    [SerializeField] private PopupBox popupBox;

    private SceneController sc;
    private bool isUnlocked;
    private string levelName;

    void Start()
    {
        sc = FindObjectOfType<SceneController>();

        // Check if the requested level is unlocked using PlayerPrefs
        levelName = GetLevelName();
        isUnlocked = PlayerPrefs.HasKey(levelName);

        if (!isUnlocked)
        {
            // Alter the appearance of the button to indicate the level is
            // not available yet
            GetComponent<Image>().color = lockedColor;
        }
    }

    public void OnClick()
    {
        if (!isUnlocked)
        {
            // Show the popup indicating the level is unavailable
            popupBox?.ShowPopup(
                "Info",
                $"You must complete level {levelNumber - 1} first!"
            );
        }
        else
        {
            sc.ChangeScene(levelName, true);
        }
    }

    private string GetLevelName()
    {
        // Get the name of the current level's scene
        switch (levelNumber)
        {
            case 1:
                return SceneNames.LEVEL_ONE;
            case 2:
                return SceneNames.LEVEL_TWO;
            case 3:
                return SceneNames.LEVEL_THREE;
            default:
                return null;
        }

    }
}
