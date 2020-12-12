using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utilities;

public class AccountInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private TextMeshProUGUI email;

    private string prevUsernameText;
    private string prevEmailText;

    void Start()
    {
        if (PlayerPrefs.HasKey(PrefKeys.USERNAME))
        {
            username.text += " " + PlayerPrefs.GetString(PrefKeys.USERNAME);
        }

        if (PlayerPrefs.HasKey(PrefKeys.EMAIL))
        {
            email.text += " " + PlayerPrefs.GetString(PrefKeys.EMAIL);
        }
    }

    public void Logout()
    {
        PlayerPrefs.DeleteAll();
        FindObjectOfType<SceneController>()?.AuthMenu(true);
    }
}
