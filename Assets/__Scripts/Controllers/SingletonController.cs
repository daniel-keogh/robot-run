using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonController : Controller
{
    void Awake()
    {
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