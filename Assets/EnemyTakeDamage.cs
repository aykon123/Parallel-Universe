using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D enemyRb;

    [SerializeField] private AudioSource enemyHeadJumpSoundEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's Rigidbody2D component
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Check if the player is moving downward (i.e., jumping on the enemy)
            if (playerRb.velocity.y <= 0)
            {
                // Destroy the enemy object if health reaches 0
                if (currentHealth <= 0)
                {
                    enemyHeadJumpSoundEffect.Play();
                    anim.SetTrigger("enemy_death");

                    // Disable the enemy's Rigidbody2D component to detach it from the player
                    enemyRb.simulated = false;
                    Invoke("DestroyEnemy", 0.5f);
                }
                else
                {
                    // Apply damage and bounce force to the enemy
                    currentHealth -= 1;
                    enemyRb.velocity = new Vector2(enemyRb.velocity.x, bounceForce);
                }

                // Apply a bounce force to the player in the opposite vertical direction
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }
        }
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        UnityEngine.Debug.Log("damage taken!");
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
