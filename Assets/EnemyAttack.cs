
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int maxAttackDamage = 15;
    public int minAttackDamage = 5;

   /* private void OnTriggerEnter(Collider other)
    {
        int damageAmount = Random.Range(minAttackDamage, maxAttackDamage);
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }*/
}
