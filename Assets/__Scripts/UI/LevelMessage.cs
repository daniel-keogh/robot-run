using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMessage : MonoBehaviour
{
    [SerializeField] private GameObject messageUI;

    private static bool isShowing = false;

    public static bool IsShowing
    {
        get => isShowing;
    }

    void Start()
    {
        DisplayMessage(true);
    }

    public void Continue()
    {
        DisplayMessage(false);
    }

    private void DisplayMessage(bool flag)
    {
        messageUI.SetActive(flag);
        Time.timeScale = flag ? 0 : 1;
        isShowing = flag;
    }
}
