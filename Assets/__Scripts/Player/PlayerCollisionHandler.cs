using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private SceneController sc;

    void Start()
    {
        sc = Controller.Find<SceneController>();
    }

    private void OnCollisionEnter(Collision other)
    {
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
