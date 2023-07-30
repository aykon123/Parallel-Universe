using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;

    // This method will be called automatically when the enemy collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the health component of the collided object
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        // Check if the collided object has a PlayerHealth component
        if (playerHealth != null)
        {
            // If the collided object has a PlayerHealth component, apply damage to it
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
