using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonController : MonoBehaviour
{
    void Awake()
    {
        // Automatically set-up a singleton for any subclasses
        SetupSingleton();
    }

    protected void SetupSingleton()
    {
        // Check for any other objects of the same type
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // Destroy the current object
            Destroy(gameObject);
        }
        else
        {
            // Persist across scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}