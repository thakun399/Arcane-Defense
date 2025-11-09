using UnityEngine;
using System.Collections.Generic;
public class WaveController : MonoBehaviour
{
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 0f;
    
    private List<GameObject> aliveEnemies = new List<GameObject>(); 

    public bool AllEnemiesDead()
    {
        if (currentWave == null) return false; 

        aliveEnemies.RemoveAll(e => e == null);
        return enemiesSpawned >= currentWave.enemyCount && aliveEnemies.Count == 0;
    }


    public bool IsComplete()
    {
        return enemiesSpawned >= currentWave?.enemyCount;
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        enemiesSpawned = 0;
        nextSpawnTime = Time.time;
        aliveEnemies = new List<GameObject>(); 
    }

    void Update()
    {
        if (currentWave == null) return;

        if (enemiesSpawned < currentWave.enemyCount && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            enemiesSpawned++;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            currentWave.enemyPrefabs[enemyIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );

        aliveEnemies.Add(enemy);
    }

}