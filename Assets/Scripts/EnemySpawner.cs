using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject enemyPrefab;
    public int[] enemyCounts = new int[4] { 5, 10, 15, 20 };

    private int currentWave = 0;
    private int activeEnemies = 0;
    private int spawnIndex = 0;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (activeEnemies <= 0)
        {
            currentWave++;
            if (currentWave < enemyCounts.Length)
            {
                StartWave();
            }
        }
    }

    void StartWave()
    {
        activeEnemies = enemyCounts[currentWave];

        for (int i = 0; i < activeEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Count == 0) return;

        Vector3 spawnPos = spawnPoints[spawnIndex].transform.position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Initialize(this);  // 적 오브젝트에 이벤트 연결

        spawnIndex = (spawnIndex + 1) % spawnPoints.Count;
    }

    public void OnEnemyDestroyed()
    {
        activeEnemies--;
    }
}
