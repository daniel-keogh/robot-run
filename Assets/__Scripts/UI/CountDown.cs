using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CountDown : MonoBehaviour
{
    private Animator animator;

    private const string UNPAUSE_TRIGGER = "Unpause";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginCountDown() => animator.SetTrigger(UNPAUSE_TRIGGER);

    public void OnCountDownEnd() => FindObjectOfType<PauseMenu>()?.Resume();
}
