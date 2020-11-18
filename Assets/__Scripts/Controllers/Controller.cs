using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public static T Find<T>() where T : MonoBehaviour
    {
        T instance = FindObjectOfType<T>();

        if (!instance)
        {
            Debug.LogWarning($"Missing {typeof(T)}");
        }

        return instance;
    }
}