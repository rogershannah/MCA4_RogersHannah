using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip hitSFX;
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
        AudioSource.PlayClipAtPoint(hitSFX, transform.position);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AttackSpell"))
        {
            TakeDamage(10);
        }
    }
}
