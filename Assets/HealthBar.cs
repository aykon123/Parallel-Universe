using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    
    public void SetMaxHealth()
    {
        slider.maxValue = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().maxHealth;
    }

    public void setHealth(int health)
    {
        slider.value = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().currentHealth;
    }

    void Update()
    {
        setHealth(GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().currentHealth);
    }
}
