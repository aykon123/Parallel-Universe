using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.5f;
    private bool isKnockback = false;
    private float knockbackTimer = 0f;
    private Vector2 knockbackDirection;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
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
