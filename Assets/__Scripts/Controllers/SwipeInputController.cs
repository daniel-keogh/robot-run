using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles swipe input
// Source: N3K EN - https://www.youtube.com/watch?v=sCgAb2cy6BY
public class SwipeInputController : SingletonController
{
    [Tooltip("Determines how much you have to swipe for it to count")]
    [SerializeField] private float deadzone = 100f;

    private bool tap;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;

    private Vector2 swipeDelta;     // The current position minus startTouch
    private Vector2 startTouch;     // Where the drag started

    public bool Tap { get => tap; }
    public bool SwipeLeft { get => swipeLeft; }
    public bool SwipeRight { get => swipeRight; }
    public bool SwipeUp { get => swipeUp; }
    public bool SwipeDown { get => swipeDown; }
    public Vector2 SwipeDelta { get => swipeDelta; }

    void Update()
    {
        // Reset everything
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        // Check for desktop input
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // If releasing the mouse click, reset the input
            startTouch = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
        #endregion

        // Check for mobile input
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            // Get the phase of the first touch
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                // If the touch has ended, reset the input
                startTouch = Vector2.zero;
                swipeDelta = Vector2.zero;
            }
        }
        #endregion

        // Calculate distance between current position and the start touch
        swipeDelta = Vector2.zero;

        // A touch has occurred
        if (startTouch != Vector2.zero)
        {
            // Check with mobile
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            // Check with standalone
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // Check if we're beyond the deadzone (determines whether the input was strong enough to be considered a swipe)
        if (swipeDelta.magnitude > deadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            // Check if swipe was vertical or horizontal in direction
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or Right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                // Up or Down
                // Left or Right
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startTouch = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
    }
}
