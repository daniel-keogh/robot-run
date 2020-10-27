using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;

    [Header("Smoothening")]
    [SerializeField] private float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Move the player character
        //
        // Reference: Third Person Movement in Unity - Brackeys
        // https://youtu.be/4HpC--2iowE
        float horizontal = Input.GetAxisRaw("Horizontal");

        var direction = new Vector3(horizontal * horizontalSpeed, 0, forwardSpeed);

        if (direction.magnitude >= Mathf.Epsilon)
        {
            // Rotate player to face the direction they're moving towards
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime
            );

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            character.Move(direction * Time.deltaTime);
        }
    }
}
