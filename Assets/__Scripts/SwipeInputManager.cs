using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputManager : MonoBehaviour
{
    [SerializeField] private float deadzone = 100f;

    private bool tap;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;

    private Vector2 swipeDelta;
    private Vector2 startTouch;

    public bool Tap { get => tap; }
    public bool SwipeLeft { get => swipeLeft; }
    public bool SwipeRight { get => swipeRight; }
    public bool SwipeUp { get => swipeUp; }
    public bool SwipeDown { get => swipeDown; }
    public Vector2 SwipeDelta { get => swipeDelta; }

    void Awake()
    {
        SetupSingleton();
    }

    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = Vector2.zero;
                swipeDelta = Vector2.zero;
            }
        }
        #endregion

        // Calculate distance
        swipeDelta = Vector2.zero;

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

        // Check if we're beyond the deadzone
        if (swipeDelta.magnitude > deadzone)
        {
            // This is a confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

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

    private void SetupSingleton()
    {
        // Check for any other objects of the same type
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject); // destroy the current object
        }
        else
        {
            DontDestroyOnLoad(gameObject); // persist across scenes
        }
    }

    public static SwipeInputManager FindSwipeInputManager()
    {
        SwipeInputManager sw = FindObjectOfType<SwipeInputManager>();

        if (!sw)
        {
            Debug.LogWarning("Missing SwipeInputManager");
        }

        return sw;
    }
}
