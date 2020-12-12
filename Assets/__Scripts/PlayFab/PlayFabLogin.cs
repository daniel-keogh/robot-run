using System.Collections;
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
    private const int MIN_PASSWORD_LEN = 6;

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

        // Try and login with the email and password
        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmail.text,
            Password = loginPassword.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnAuthFailure);
    }

    public void OnRegister()
    {
        if (!IsValidRegister())
            return;

        // Try and register the user
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = registerEmail.text,
            Password = registerPassword.text,
            Username = registerUsername.text,
            DisplayName = registerUsername.text
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnAuthFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        // Save locally for faster access
        PlayerPrefs.SetString(PrefKeys.EMAIL, loginEmail.text);
        PlayerPrefs.SetString(PrefKeys.PASSWORD, loginPassword.text);
        PlayerPrefs.SetString(PrefKeys.USERNAME, result.InfoResultPayload.PlayerProfile.DisplayName);

        // Fetch the users stats
        playFabStats.GetStats();

        sceneController.MainMenu(true);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        // Save locally for faster access
        PlayerPrefs.SetString(PrefKeys.EMAIL, registerEmail.text);
        PlayerPrefs.SetString(PrefKeys.PASSWORD, registerPassword.text);
        PlayerPrefs.SetString(PrefKeys.USERNAME, registerUsername.text);

        sceneController.MainMenu(true);
    }

    private void OnAuthFailure(PlayFabError error)
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

        // PlayFab complains if passwords are under 6 characters
        if (registerPassword.text.Length < MIN_PASSWORD_LEN)
        {
            InvalidCredentials($"Password must contain at least {MIN_PASSWORD_LEN} characters.");
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
