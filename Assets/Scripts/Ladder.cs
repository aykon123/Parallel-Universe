using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float vertical; // Input value for vertical movement (up and down)
    private float speed = 8f; // Speed of climbing the ladder
    private bool isLadder; // Flag to indicate if the player is on a ladder
    private bool isClimbing; // Flag to indicate if the player is currently climbing

    [SerializeField] private Rigidbody2D rb; // Reference to the player's Rigidbody2D component

    // Update is called once per frame
    void Update()
    {
        // Get vertical input axis value
        vertical = Input.GetAxis("Vertical");

        // Check if the player is on a ladder and trying to move vertically
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            // Set the climbing flag to true if the player is trying to climb
            isClimbing = true;
        }
    }

    // FixedUpdate is called at a fixed interval (physics update)
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            // If the player is climbing, disable gravity and move the player vertically
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            // If the player is not climbing, enable gravity to fall normally
            rb.gravityScale = 3f;
        }
    }

    // This method is called automatically when the player enters a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with an object tagged as "Ladder"
        if (collision.CompareTag("Ladder"))
        {
            // Set the ladder flag to true when the player touches the ladder
            isLadder = true;
        }
    }

    // This method is called automatically when the player exits a trigger collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player is exiting from an object tagged as "Ladder"
        if (collision.CompareTag("Ladder"))
        {
            // Reset the ladder and climbing flags when the player leaves the ladder
            isLadder = false;
            isClimbing = false;
        }
    }
}
