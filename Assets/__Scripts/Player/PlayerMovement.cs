using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float speedIncrementor = 0.5f;

    [Header("Lanes")]
    [SerializeField] private float laneOffset = 3f;

    private Vector3 moveDirection;
    private int currentLane = 1;
    private bool isJumping = false;

    private SwipeInputController swipeInput;
    private Rigidbody rb;
    private GameController gc;
    private int prevScore = 0;

    private const int MIN_LANE = 0;
    private const int MAX_LANE = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        swipeInput = Controller.Find<SwipeInputController>();
        gc = Controller.Find<GameController>();
    }

    void Update()
    {
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
        // Prevent jumping while airborne
        isJumping = false;
    }

    private void Move()
    {
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
    }

    private void SwitchLane(Direction direction)
    {
        int targetLane = currentLane + (int)direction;

        if (targetLane < MIN_LANE || targetLane > MAX_LANE)
            return;

        currentLane = targetLane;
        moveDirection = new Vector3((currentLane - 1) * laneOffset, 0f, 0f);
    }

    private bool IsInPlayMode()
    {
        return !(PauseMenu.IsPaused || LevelMessage.IsShowing);
    }

    private void IncreaseSpeed()
    {
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
