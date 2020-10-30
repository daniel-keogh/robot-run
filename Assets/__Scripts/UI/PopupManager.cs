using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupBox confirmPopup;

    private Animator animator;

    public const string IS_SHOWING = "IsShowing";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowPopup(string title, string body)
    {
        confirmPopup.SetContent(title, body);
        animator.SetBool(IS_SHOWING, true);
    }

    public void HidePopup() => animator.SetBool(IS_SHOWING, false);
}
