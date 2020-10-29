using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 3f;

    [Header("Lanes")]
    [SerializeField] private float laneOffset = 3f;

    private Vector3 moveDirection;
    private int currentLane = 1;

    private const int MIN_LANE = 0;
    private const int MAX_LANE = 2;

    void Start()
    {

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

        Move();
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

    private enum Direction
    {
        LEFT = -1, RIGHT = 1
    }
}
