using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarChange : MonoBehaviour
{

    public GameObject HealthBar;
    public GameObject HealthBarCyber;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            HealthBar.SetActive(false);
            HealthBarCyber.SetActive(true);
        }
        else
        {
            
            HealthBar.SetActive(true);
            HealthBarCyber.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
