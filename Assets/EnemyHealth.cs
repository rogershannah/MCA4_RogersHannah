using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    public static bool isDead = false;
    public Slider healthSlider;

    public int currentHealth;

    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
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
            //dead
        }
    }

    void PlayerDies()
    {
        isDead = true;
        Debug.Log("Player is dead...");
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }
    }
}
