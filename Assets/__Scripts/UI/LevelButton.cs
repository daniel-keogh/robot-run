using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class LevelButton : MonoBehaviour
{
    [Header("Locked")]
    [SerializeField] [Range(1, 3)] private int levelNumber = 1;
    [SerializeField] private Color lockedColor;

    [Header("Popup")]
    [SerializeField] private PopupBox popupBox;

    private SceneController sc;
    private bool isUnlocked;
    private string levelName;

    void Start()
    {
        sc = Controller.Find<SceneController>();

        levelName = GetLevelName();
        isUnlocked = PlayerPrefs.HasKey(levelName);

        if (!isUnlocked)
        {
            GetComponent<Image>().color = lockedColor;
        }
    }

    public void OnClick()
    {
        if (!isUnlocked)
        {
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
