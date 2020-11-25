using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [Tooltip("A forwardValue of 0f means the player is idle. 1f means they are running. Values in between denote walking speed")]
    [SerializeField] [Range(0, 1)] private float forwardValue = 1f;

    [Header("Damping")]
    [SerializeField] private float forwardDampTime = 0.1f;
    [SerializeField] private float jumpDampTime = 0.3f;

    private Animator animator;
    private Rigidbody rb;

    // Animator parameter strings
    private const string ANIM_FORWARD = "Forward";
    private const string ANIM_JUMP = "Jump";
    private const string ANIM_GROUNDED = "IsGrounded";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateAnimationState(bool isJumping)
    {
        // Update the animator parameters
        animator.SetFloat(ANIM_FORWARD, forwardValue, forwardDampTime, Time.deltaTime);
        animator.SetBool(ANIM_GROUNDED, !isJumping);

        if (isJumping)
        {
            // Update the Jump parameter using the force currently being applied to the
            // player's Rigidbody component
            animator.SetFloat(ANIM_JUMP, rb.velocity.y, jumpDampTime, Time.deltaTime);
        }
    }
}
