using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;

    public void SetContent(string title, string body)
    {
        titleText.text = title;
        bodyText.text = body;
    }
}
