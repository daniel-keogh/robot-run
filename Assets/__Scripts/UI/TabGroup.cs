using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Modified version of this script: https://www.youtube.com/watch?v=211t6r12XPQ
public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSwap;
    [SerializeField] private TabButton selected;

    [Header("Colours")]
    [SerializeField] private Color tabIdle;
    [SerializeField] private Color tabHover;
    [SerializeField] private Color tabActive;
    [SerializeField] private Color tabTextIdle;
    [SerializeField] private Color tabTextHover;
    [SerializeField] private Color tabTextActive;

    private List<TabButton> tabButtons;
    private TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        if (button == selected)
        {
            OnTabSelected(button);
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            button.Background.color = tabHover;
            button.TextColor = tabTextHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;

        ResetTabs();
        button.Background.color = tabActive;
        button.TextColor = tabTextActive;

        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }

            button.Background.color = tabIdle;
            button.TextColor = tabTextIdle;
        }
    }
}
