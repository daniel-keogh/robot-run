﻿using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Utilities;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float splashDuration = 3f;

    private SceneController sc;

    void Start()
    {
        sc = FindObjectOfType<SceneController>();

        // Try and log the user in automatically
        StartCoroutine(ChooseMenu());
    }

    private IEnumerator ChooseMenu()
    {
        yield return new WaitForSeconds(splashDuration);

        // If there's an email in PlayerPrefs try and login automatically using that
        if (PlayerPrefs.HasKey(PrefKeys.EMAIL))
        {
            string email = PlayerPrefs.GetString(PrefKeys.EMAIL);
            string password = PlayerPrefs.GetString(PrefKeys.PASSWORD);

            var request = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
            // Failed login, go to auth menu
            sc.AuthMenu(true);
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        // User is automatically logged in
        FindObjectOfType<PlayFabStats>().GetStats();
        sc.MainMenu(true);
    }
    private void OnLoginFailure(PlayFabError error)
    {
        // Failed login, go to auth menu
        sc.AuthMenu(true);
    }
}
