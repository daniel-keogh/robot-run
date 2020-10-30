using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LevelButton : MonoBehaviour
{
    [Header("Locked")]
    [SerializeField] private bool isLocked;
    [SerializeField] private Sprite lockedTargetGraphic;

    [Header("Locked Popup")]
    [SerializeField] private string popupTitle;
    [SerializeField] [TextArea(0, 100)] private string popupBody;

    private SceneController sceneController;
    private PopupManager popupManager;

    void Start()
    {
        sceneController = SceneController.FindSceneController();
        popupManager = PopupManager.FindObjectOfType<PopupManager>();

        GetComponent<Image>().sprite = lockedTargetGraphic;
    }

    public void OnClick()
    {
        popupManager?.ShowPopup(popupTitle, popupBody);
    }
}
