using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using Utilities;

public class AccountInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private TextMeshProUGUI email;

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
        PlayFabClientAPI.ForgetAllCredentials();
        FindObjectOfType<SceneController>()?.AuthMenu(true);
    }
}
