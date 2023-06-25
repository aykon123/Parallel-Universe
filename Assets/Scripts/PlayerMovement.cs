using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.5f;
    private bool isKnockback = false;
    private float knockbackTimer = 0f;
    private Vector2 knockbackDirection;
    private Animator animator;

    [SerializeField] private bool enableSlide = true;
    [SerializeField] private float slideForce = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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

        // Set IsRunning parameter to false if the player is not running
        if (!isRunning)
        {
            animator.SetBool("IsRunning", false);
        }

        // Set IsJumping parameter in the animator based on vertical movement
        bool isJumping = rb.velocity.y > 0;
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
        if (enableSlide && isRunning && isGrounded())
        {
            rb.AddForce(new Vector2(dirX * slideForce, 0f), ForceMode2D.Force);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 12f);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
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

    private void OnCollisionStay2D(Collision2D collision)
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
}
