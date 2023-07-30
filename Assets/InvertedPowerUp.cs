using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControlsPowerUp : MonoBehaviour
{
    public float powerUpDuration = 5f; // Duration of the Power Up effect in seconds

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerMovement script attached to the player GameObject
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Trigger the Power Up effect in the PlayerMovement script
                playerMovement.StartInvertedControlsEffect(powerUpDuration);

                // Disable the Power Up GameObject to hide it after being collected
                gameObject.SetActive(false);
            }
        }
    }
}
