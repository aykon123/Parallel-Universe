using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int patrolDestination = 0;
    private bool facingRight = true; // Flag to track the direction

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    private void Update()
    {
        if (isChasing)
        {
            float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
            transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

            // Flip the sprite based on the direction
            if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
            {
                FlipSprite();
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                // Check if there are patrol points defined
                if (patrolPoints.Length == 0)
                {
                    UnityEngine.Debug.LogWarning("No patrol points defined for EnemyPatrol.");
                    return;
                }

                // Move towards the current patrol destination
                Transform currentDestination = patrolPoints[patrolDestination];
                transform.position = Vector2.MoveTowards(transform.position, currentDestination.position, moveSpeed * Time.deltaTime);

                // Check if reached the patrol destination
                if (Vector2.Distance(transform.position, currentDestination.position) < 0.2f)
                {
                    // Switch to the next patrol destination
                    patrolDestination = (patrolDestination + 1) % patrolPoints.Length;

                    // Flip the sprite horizontally
                    FlipSprite();
                }
            }
        }
    }

    private void FlipSprite()
    {
        // Flip the sprite horizontally
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
