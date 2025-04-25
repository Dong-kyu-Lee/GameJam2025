using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject enemyPrefab;
    private int[] enemyCounts = new int[5] { 5, 10, 15, 20, 25 };

    private int currentWave = 0;
    private int activeEnemies = 0;
    private int spawnIndex = 0;
    private bool isGameOver = false;

    void Start()
    {
        // StartWave();
    }

    void Update()
    {
        if(isGameOver) return;

        if (activeEnemies <= 0)
        {
            if (currentWave < 5)
            {
                StartWave();
            }
            else
            {
                // game clear
                GameManager.Instance.GameClear();
                isGameOver = true;
            }
        }
    }

    void StartWave()
    {
        activeEnemies = enemyCounts[currentWave];
        Debug.Log(currentWave + ", " + enemyCounts.Length);
        for (int i = 0; i < activeEnemies; i++)
        {
            SpawnEnemy();
        }
        currentWave++;
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
