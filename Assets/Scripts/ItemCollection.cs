using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public int shards = 0;

    [SerializeField] private Text shardsText;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
       if (collision.gameObject.CompareTag("Shard"))
        {
            Destroy(collision.gameObject);
            shards++;
            shardsText.text = "Portal Shards: " + shards;
        }
    }

}
