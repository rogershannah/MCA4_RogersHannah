using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public static bool isDead = false;
    public Slider healthSlider;

    int currentHealth;
    void Start()
    {
        if (healthSlider == null)
        {
                healthSlider = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Slider>();
        }
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            PlayerDies();
        }

    }

    void PlayerDies()
    {
        isDead = true;
        transform.Rotate(-90, 0, 0, Space.Self);
        FindObjectOfType<LevelManager>().LevelLost();
    }
}
