using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Modified version of this script: https://www.youtube.com/watch?v=211t6r12XPQ
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TabGroup tabGroup;

    private Image background;
    private TextMeshProUGUI buttonText;

    public Image Background
    {
        get => background;
        set => background = value;
    }

    public Color TextColor
    {
        get => buttonText.color;
        set => buttonText.color = value;
    }

    void Start()
    {
        background = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}
