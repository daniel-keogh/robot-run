﻿using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using Utilities;

public class PlayFabLogin : MonoBehaviour
{
    [Header("Register")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerConfirmPassword;

    [Header("Login")]
    [SerializeField] private TMP_InputField loginEmail;
    [SerializeField] private TMP_InputField loginPassword;

    [Header("Popup")]
    [SerializeField] private PopupBox popup;

    private SceneController sceneController;
    private PlayFabStats playFabStats;

    private const string TITLE_ID = "3E239";

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = TITLE_ID;
        }

        sceneController = FindObjectOfType<SceneController>();
        playFabStats = FindObjectOfType<PlayFabStats>();
    }

    public void OnLogin()
    {
        if (!IsValidLogin())
            return;

        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmail.text,
            Password = loginPassword.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void OnRegister()
    {
        if (!IsValidRegister())
            return;

        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = registerEmail.text,
            Password = registerPassword.text,
            Username = registerUsername.text
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        // Save for faster access
        PlayerPrefs.SetString(PrefKeys.EMAIL, loginEmail.text);
        PlayerPrefs.SetString(PrefKeys.PASSWORD, loginPassword.text);

        playFabStats.GetStats();

        sceneController.MainMenu(true);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        // Save for faster access
        PlayerPrefs.SetString(PrefKeys.USERNAME, registerUsername.text);
        PlayerPrefs.SetString(PrefKeys.EMAIL, registerEmail.text);
        PlayerPrefs.SetString(PrefKeys.PASSWORD, registerPassword.text);

        // Set the players display name
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = registerUsername.text
        }, null, OnRegisterFailure);

        sceneController.MainMenu(true);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        InvalidCredentials(error.ErrorMessage);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        InvalidCredentials(error.ErrorMessage);
    }

    private bool IsValidLogin()
    {
        // Don't accept empty input
        if (loginEmail.text.Length == 0 || loginPassword.text.Length == 0)
        {
            InvalidCredentials("All fields are required.");
            return false;
        }

        return true;
    }

    private bool IsValidRegister()
    {
        // Don't accept empty input
        if (
            registerUsername.text.Length == 0 ||
            registerEmail.text.Length == 0 ||
            registerPassword.text.Length == 0 ||
            registerConfirmPassword.text.Length == 0
        )
        {
            InvalidCredentials("All fields are required.");
            return false;
        }

        // Primitive email validation
        if (registerEmail.text.IndexOf("@") == -1)
        {
            InvalidCredentials("Please enter a valid email address.");
            return false;
        }

        if (registerPassword.text != registerConfirmPassword.text)
        {
            InvalidCredentials("Passwords don't match.");
            return false;
        }

        return true;
    }

    private void InvalidCredentials(string message, string title = "Error")
    {
        // Display a popup with a given error message
        popup.ShowPopup(title, message);
    }
}