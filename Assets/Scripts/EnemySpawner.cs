using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnTime = 5;
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        if(!LevelManager.isGameOver)
        {
            Vector3 enemyPosition = transform.position;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;

            spawnedEnemy.transform.parent = gameObject.transform;

        }
    }
}
