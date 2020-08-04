using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnInitTime = 5;
    public int spawnRepeatTime = 8;
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnInitTime, spawnRepeatTime);
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
