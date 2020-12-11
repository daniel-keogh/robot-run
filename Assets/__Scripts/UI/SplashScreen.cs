using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Utilities;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float splashDuration = 2f;

    private SceneController sc;

    void Start()
    {
        sc = FindObjectOfType<SceneController>();

        StartCoroutine(ChooseMenu());
    }

    private IEnumerator ChooseMenu()
    {
        yield return new WaitForSeconds(splashDuration);

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
    }

    private void OnLoginSuccess(LoginResult result)
    {
        sc.MainMenu(true);
    }
    private void OnLoginFailure(PlayFabError error)
    {
        sc.AuthMenu(true);
    }
}
