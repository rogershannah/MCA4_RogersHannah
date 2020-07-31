using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject particleFX;
    public AudioClip hitSFX;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackSpell") || other.CompareTag("CeilingProb"))
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