using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private SceneController sc;

    void Start()
    {
        sc = FindObjectOfType<SceneController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Only respond if an Obstacle was hit
        if (other.collider.GetComponent<Obstacle>() != null)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        sc.GameOver(true);
    }
}
