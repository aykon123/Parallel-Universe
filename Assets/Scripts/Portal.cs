using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private int requiredShards; // Renamed to be more descriptive
    [SerializeField] private bool isLastLevel = false; // Renamed to be more descriptive
    private bool portalEntered = false;

    [SerializeField] private GameObject notEnoughShardsText;
    [SerializeField] private GameObject enoughShardsMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has collided with the portal and has not entered it yet
        if (collision.gameObject.name == "Player" && !portalEntered)
        {
            ItemCollection itemCollection = collision.GetComponent<ItemCollection>();
            if (itemCollection != null && itemCollection.shards >= requiredShards)
            {
                // Player has enough shards to enter the portal
                UnityEngine.Debug.Log("Player has enough shards to enter the portal");
                portalEntered = true;

                // Show the enough shards message and hide it after 2 seconds
                enoughShardsMessage.SetActive(true);
                Invoke("HideEnoughShardsMessage", 2f);

                // Load the next level or the first level depending on the setting
                Invoke("CompleteLevel", 2f);
            }
            else
            {
                // Player does not have enough shards to enter the portal
                UnityEngine.Debug.Log("Not enough shards to enter the portal");
                notEnoughShardsText.SetActive(true);
                Invoke("HideNotEnoughShardsMessage", 2f);
            }
        }
    }

    private void CompleteLevel()
    {
        // Load the next level or the first level depending on the setting
        int nextLevelIndex = isLastLevel ? 0 : SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevelIndex);
    }

    private void HideNotEnoughShardsMessage()
    {
        // Hide the "Not Enough Shards" message
        notEnoughShardsText.SetActive(false);
    }

    private void HideEnoughShardsMessage()
    {
        // Hide the "Enough Shards" message
        enoughShardsMessage.SetActive(false);
    }
}
