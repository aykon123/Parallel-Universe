using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damageAmount = 10;
    public float projectileSpeed = 10f;

    private Rigidbody projectileRigidbody;

    private void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
        projectileRigidbody.velocity = transform.forward * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
