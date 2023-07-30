// ItemCollection.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public int shards = 0;
    [SerializeField] private UnityEngine.UI.Text shardsText; // Specify UnityEngine.UI.Text
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shard"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            shards++;
            shardsText.text = "Portal Shards: " + shards;
        }
    }
}
