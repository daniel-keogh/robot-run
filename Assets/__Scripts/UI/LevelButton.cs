using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Locked")]
    [SerializeField] private bool isLocked;
    [SerializeField] private Color lockedColor;

    [Header("Popup")]
    [SerializeField] private PopupBox popupBox;
    [SerializeField] private string popupTitle;
    [SerializeField] [TextArea(0, 100)] private string popupBody;

    private SceneController sceneController;

    void Start()
    {
        sceneController = Controller.Find<SceneController>();

        if (isLocked)
        {
            GetComponent<Image>().color = lockedColor;
        }
    }

    public void OnClick()
    {
        popupBox?.ShowPopup(popupTitle, popupBody);
    }
}
