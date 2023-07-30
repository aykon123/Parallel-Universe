using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Damage amount to apply to the player on collision
    public int damageAmount = 10;

    // Speed of the projectile
    public float projectileSpeed = 10f;

    private Rigidbody projectileRigidbody;

    private void Start()
    {
        // Get the Rigidbody component attached to this projectile
        projectileRigidbody = GetComponent<Rigidbody>();

        // Set the initial velocity of the projectile to move it forward
        projectileRigidbody.velocity = transform.forward * projectileSpeed;
    }

    // This method is automatically called when the projectile collides with a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a PlayerHealth component
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        // If the collided object has a PlayerHealth component, apply damage and destroy the projectile
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
