using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationHandler))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpSpeed = 3f;

    [Header("Lanes")]
    [SerializeField] private float laneOffset = 3f;

    private Vector3 moveDirection;
    private int currentLane = 1;
    private bool isJumping = false;

    private float maxSpeed;
    private float speedIncrementor;

    private SwipeInputController swipeInput;
    private Rigidbody rb;
    private PlayerAnimationHandler animationHandler;
    private GameController gc;
    private int prevScore = 0;

    private const int MIN_LANE = 0;
    private const int MAX_LANE = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animationHandler = GetComponent<PlayerAnimationHandler>();

        swipeInput = FindObjectOfType<SwipeInputController>();
        gc = FindObjectOfType<GameController>();

        maxSpeed = gc.CurrentLevelConfig.MaxSpeed;
        speedIncrementor = gc.CurrentLevelConfig.SpeedIncrementor;
    }

    void Update()
    {
        // Make sure controls are disabled if not in play mode
        if (IsInPlayMode())
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || swipeInput.SwipeLeft)
            {
                SwitchLane(Direction.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || swipeInput.SwipeRight)
            {
                SwitchLane(Direction.RIGHT);
            }

            if ((Input.GetKeyDown(KeyCode.Space) || swipeInput.SwipeUp) && !isJumping)
            {
                isJumping = true;
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }

            Move();
            IncreaseSpeed();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Prevents player from jumping while already airborne
        isJumping = false;
    }

    private void Move()
    {
        // Move forward along the z-axis
        var direction = new Vector3(
            moveDirection.x,
            transform.position.y,
            transform.position.z + (speed * Time.deltaTime)
        );

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            direction,
            1f
        );

        // Update the player's animator
        animationHandler.UpdateAnimationState(isJumping);
    }

    private void SwitchLane(Direction direction)
    {
        // Set where the player wants to move to
        int targetLane = currentLane + (int)direction;

        if (targetLane < MIN_LANE || targetLane > MAX_LANE)
            return; // already on the edge

        currentLane = targetLane;

        // Switch lanes using the value of laneOffset
        moveDirection = new Vector3((currentLane - 1) * laneOffset, 0f, 0f);
    }

    private bool IsInPlayMode()
    {
        return !(PauseMenu.IsPaused || LevelMessage.IsShowing);
    }

    private void IncreaseSpeed()
    {
        // Increase the speed whenever the player's score has increased
        // and as long as they're still slower than the maxSpeed
        if (gc.PlayerScore > prevScore && speed < maxSpeed)
        {
            speed += speedIncrementor;
            prevScore = gc.PlayerScore;
        }
    }

    private enum Direction
    {
        LEFT = -1, RIGHT = 1
    }
}
