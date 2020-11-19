using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LevelButton : MonoBehaviour
{
    [Header("Locked")]
    [SerializeField] private bool isLocked;

    [Header("Popup")]
    [SerializeField] private PopupBox popupBox;
    [SerializeField] private string popupTitle;
    [SerializeField] [TextArea(0, 100)] private string popupBody;

    private SceneController sceneController;

    void Start()
    {
        sceneController = Controller.Find<SceneController>();
    }

    public void OnClick()
    {
        popupBox?.ShowPopup(popupTitle, popupBody);
    }
}
