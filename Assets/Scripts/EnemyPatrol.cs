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
    public Transform stopPosition; // Stop position for chasing
    public float stopDistanceThreshold = 0.1f; // Distance threshold to stop chasing

    private bool hasReachedStopPosition; // Flag to track if the enemy has reached the stop position
    private bool returningToPatrol; // Flag to track if the enemy is returning to the patrol route

    private void Update()
    {
        if (isChasing)
        {
            if (Vector2.Distance(transform.position, stopPosition.position) <= stopDistanceThreshold)
            {
                isChasing = false; // Stop chasing if within stop distance threshold
                hasReachedStopPosition = true;
                returningToPatrol = true;
                return;
            }

            float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
            transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

            // Flip the sprite based on the direction
            if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
            {
                FlipSprite();
            }
        }
        else if (returningToPatrol)
        {
            Transform destination = patrolPoints[0]; // Return to patrol point 0

            // Move towards the patrol point
            transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);

            // Check if reached the patrol point
            if (Vector2.Distance(transform.position, destination.position) < 0.2f)
            {
                returningToPatrol = false; // Stop returning to patrol
                hasReachedStopPosition = false; // Reset the flag for the next patrol destination
            }
            else
            {
                // Flip the sprite based on the movement direction
                float direction = Mathf.Sign(destination.position.x - transform.position.x);
                if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
                {
                    FlipSprite();
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
                hasReachedStopPosition = false;
            }
            else if (hasReachedStopPosition) // Return to patrol if not chasing and has reached the stop position
            {
                returningToPatrol = true;
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
                float direction = Mathf.Sign(currentDestination.position.x - transform.position.x);
                transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

                // Flip the sprite based on the direction
                if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
                {
                    FlipSprite();
                }

                // Check if reached the patrol destination
                if (Vector2.Distance(transform.position, currentDestination.position) < 0.2f)
                {
                    // Switch to the next patrol destination
                    patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
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

