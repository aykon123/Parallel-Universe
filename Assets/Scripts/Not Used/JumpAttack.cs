using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a weakpoint
        if (collision.gameObject.CompareTag("Weakpoint"))
        {
            // Destroy both the weakpoint and its parent
            Destroy(gameObject);
        }
    }
}