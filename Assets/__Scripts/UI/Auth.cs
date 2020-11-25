using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Auth : MonoBehaviour
{
    [Header("Register")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerConfirmPassword;

    [Header("Login")]
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;

    [Header("Popup")]
    [SerializeField] private PopupBox popup;

    private SceneController sceneController;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    public void Login()
    {
        // If the login is valid go to the main menu
        if (!IsValidLogin())
        {
            return;
        }

        sceneController.MainMenu(true);
    }

    public void Register()
    {
        // If registration is valid go to the main menu
        if (!IsValidRegister())
        {
            return;
        }

        sceneController.MainMenu(true);
    }

    private bool IsValidLogin()
    {
        // Don't accept empty input
        if (loginUsername.text.Length == 0 || loginPassword.text.Length == 0)
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
