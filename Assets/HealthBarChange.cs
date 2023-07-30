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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            HealthBar.SetActive(true);
            HealthBarCyber.SetActive(false);
        }
        else
        {
            HealthBar.SetActive(false);
            HealthBarCyber.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
