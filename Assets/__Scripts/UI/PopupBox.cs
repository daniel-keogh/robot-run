using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class PopupBox : MonoBehaviour
{
    [SerializeField] private GameObject popupUI;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;

    private Animator animator;

    private const string IS_SHOWING = "IsShowing";
    private const string INFO_ICON = "\uf05a";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowPopup(string title, string body)
    {
        titleText.text = $"{INFO_ICON} {title}";
        bodyText.text = body;

        // Animate the popup into view
        animator.SetBool(IS_SHOWING, true);
    }

    public void HidePopup() => animator.SetBool(IS_SHOWING, false);
}
