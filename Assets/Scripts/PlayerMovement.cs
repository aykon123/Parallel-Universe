using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Private variables for player movement and components
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround; // Layer mask for detecting jumpable ground

    // Variables for knockback effect
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.5f;
    private bool isKnockback = false;
    private float knockbackTimer = 0f;
    private Vector2 knockbackDirection;

    [SerializeField] private bool enableSlide = true; // Enable sliding effect
    [SerializeField] private float slideForce = 2f; // Force applied when sliding

    [SerializeField] private AudioSource jumpSoundEffect; // Sound effect for jumping
    [SerializeField] private float fallThreshold = -10f; // Fall threshold for player death
    [SerializeField] private float respawnDelay = 2f; // Delay before respawning after death
    private Vector2 respawnPosition; // Respawn position for the player

    private bool isGrounded = false; // Flag for checking if the player is grounded
    private bool canDoubleJump = false; // Flag for enabling double jump

    private bool isInvertedControls = false; // Flag for inverted controls effect
    private float invertedControlsDuration = 5f; // Duration of inverted controls effect
    private float invertedControlsTimer = 0f; // Timer for inverted controls effect

    [SerializeField] private bool disableDoubleJump = false; // Disable double jump if true

    [SerializeField] private AudioSource deathSoundEffect; // Sound effect for player death

    private bool isJumpBoosted = false; // Flag for jump boost effect
    private float jumpBoostHeight = 20f; // Height of jump boost
    private float jumpBoostDuration = 5f; // Default duration for the jump boost
    private float jumpBoostTimer = 0f; // Timer for the jump boost effect
    private bool isJumpingBoosted = false; // Flag for checking if the jump boost effect is active

    private void Start()
    {
        // Get references to the components
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Set the respawn position to the initial position of the player
        respawnPosition = transform.position;
    }

    private void Update()
    {
        if (isKnockback)
        {
            // Apply knockback force
            rb.velocity = knockbackDirection * knockbackForce;

            // Update knockback timer
            knockbackTimer += Time.deltaTime;

            if (knockbackTimer >= knockbackDuration)
            {
                isKnockback = false;
                knockbackTimer = 0f;
            }

            return;
        }

        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        // Set IsRunning parameter in the animator based on absolute horizontal movement
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0;
        animator.SetBool("IsRunning", isRunning);

        // Set IsJumping parameter in the animator based on vertical movement
        bool isJumping = rb.velocity.y > 0 && !isGrounded;
        animator.SetBool("IsJumping", isJumping);

        // Flip the sprite based on movement direction
        if (dirX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Apply sliding effect
        if (enableSlide && isRunning && isGrounded)
        {
            rb.AddForce(new Vector2(dirX * slideForce, 0f), ForceMode2D.Force);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 12f);
                if (!disableDoubleJump)
                {
                    canDoubleJump = true;
                }

                // Jump Sound Effect
                jumpSoundEffect.Play();
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 12f);
                canDoubleJump = false;

                // Play jump sound effect again for double jump
                jumpSoundEffect.Play();
            }
        }

        // Check if player falls below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            Die();
        }

        if (isInvertedControls)
        {
            // Invert horizontal movement
            float invertedDirX = -Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(invertedDirX * 7f, rb.velocity.y);

            // Update inverted controls timer
            invertedControlsTimer -= Time.deltaTime;
            if (invertedControlsTimer <= 0f)
            {
                isInvertedControls = false;
            }
        }

        // Check for the jump boost input and apply the effect
        if (Input.GetButtonDown("Jump") && isJumpBoosted && isGrounded && !isJumpingBoosted)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpBoostHeight);
            isJumpingBoosted = true;
        }

        // Check if the jump boost effect is active and update its timer
        if (isJumpingBoosted)
        {
            jumpBoostTimer -= Time.deltaTime;
            if (jumpBoostTimer <= 0f)
            {
                isJumpingBoosted = false;
                isJumpBoosted = false;
            }
        }
    }

    public void StartInvertedControlsEffect(float duration)
    {
        isInvertedControls = true;
        invertedControlsDuration = duration;
        invertedControlsTimer = duration;
    }

    public void StartJumpBoostEffect(float height, float duration)
    {
        // Apply the jump boost effect to the player
        isJumpBoosted = true;
        jumpBoostHeight = height;
        jumpBoostDuration = duration;
        jumpBoostTimer = duration;
    }

    private void EndJumpBoostEffect()
    {
        // End the jump boost effect
        isJumpBoosted = false;
        // Reset any changes to the player's jump here if needed
        // For example, reset the jump force or height to the original values.
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Start knockback effect
            isKnockback = true;
            knockbackTimer = 0f;

            // Calculate knockback direction
            Vector2 enemyPosition = collision.transform.position;
            Vector2 playerPosition = transform.position;
            knockbackDirection = (playerPosition - enemyPosition).normalized;
            knockbackDirection.y = Mathf.Abs(knockbackDirection.y);
        }
    }

    private void Die()
    {
        // Game over logic or player death animation here
        deathSoundEffect.Play();
        UnityEngine.Debug.Log("Player has died");

        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");

        // Reset player position to the respawn position
        transform.position = respawnPosition;

        // Delay before re-enabling player control
        StartCoroutine(EnablePlayerControl(respawnDelay));
    }

    private IEnumerator EnablePlayerControl(float delay)
    {
        // Disable player control during the delay
        enabled = false;

        yield return new WaitForSeconds(delay);

        // Enable player control after the delay
        enabled = true;
    }
}
