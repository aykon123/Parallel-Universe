using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] public int ReqShards;
    [SerializeField] public bool lastLevel = false;
    private bool portalEntered = false;
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !portalEntered)
        { 
             ItemCollection itemCollection = collision.GetComponent<ItemCollection>();
            if (itemCollection != null && itemCollection.shards >= ReqShards) 
                {
                    Debug.Log("Player has enough Shards to enter Portal");
                    portalEntered = true;
                    Invoke("CompleteLevel", 2f);
                }
            else
                {
                    Debug.Log("Not Enough Shards");
                }
        
            }
        
    }

    private void CompleteLevel()
    {
        if(lastLevel){
         SceneManager.LoadScene(0);

        } else{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
