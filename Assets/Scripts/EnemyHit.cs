using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject particleFX;
    public AudioClip hitSFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackSpell"))
        {
            AudioSource.PlayClipAtPoint(hitSFX, transform.position);
            DestroyEnemny();
            FindObjectOfType<LevelManager>().UpdateScore();
        }
    }

    void DestroyEnemny()
    {
        Instantiate(particleFX, transform.position, transform.rotation);

        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}