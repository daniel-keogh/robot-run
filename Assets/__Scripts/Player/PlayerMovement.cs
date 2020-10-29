using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Jumping")]
    [SerializeField] private float jumpSpeed = 3f;

    [Header("Lanes")]
    [SerializeField] private float laneOffset = 3f;
    [SerializeField] private float switchSpeed = 50f;

    private Vector3 moveDirection;
    private int currentLane = 1;

    private const int MIN_LANE = 0;
    private const int MAX_LANE = 2;

    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchLane(Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchLane(Direction.RIGHT);
        }

        // Mobile
        if (Input.touchCount > 0)
        {

        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var direction = new Vector3(
            moveDirection.x,
            character.transform.localPosition.y,
            character.transform.localPosition.z
        );

        character.transform.localPosition = Vector3.MoveTowards(
            character.transform.localPosition,
            direction,
            switchSpeed * Time.fixedDeltaTime
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

    private enum Direction
    {
        LEFT = -1, RIGHT = 1
    }
}
