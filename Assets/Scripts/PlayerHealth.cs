using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Health properties
    public int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    void Start()
    {
        // Get references to components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Set initial health to maximum
        currentHealth = maxHealth;
    }

    // Method to apply damage to the player
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if the player's health has reached zero or below
        if (currentHealth <= 0)
        {
            // Player is dead
            Die();
        }
    }

    // Method to handle collision with a trap
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            // Player hit a trap and should die
            Die();
        }
    }

    // Method to handle player death
    private void Die()
    {
        // Play death sound effect
        deathSoundEffect.Play();

        // Log player's death
        UnityEngine.Debug.Log("Player has died");

        // Stop the player's movement by setting the Rigidbody type to Static
        rb.bodyType = RigidbodyType2D.Static;

        // Trigger death animation if any
        anim.SetTrigger("death");
    }

    // Method to restart the current level
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
