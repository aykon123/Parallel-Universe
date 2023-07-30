using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostPowerUp : MonoBehaviour
{
    [SerializeField] private float jumpBoostHeight = 20f;
    [SerializeField] private float jumpBoostDuration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartJumpBoostEffect(jumpBoostHeight, jumpBoostDuration);
                // Play an audio or visual effect here if desired

                Destroy(gameObject); // Destroy the power-up after being collected
            }
        }
    }
}
