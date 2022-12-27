using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initialize(_enemyPrefab);
        StartCoroutine(SpawnEnemy());
    }

    private void SetEnemy(GameObject enemy, Vector3 spawnPoint)
    {
        enemy.SetActive(true);
        enemy.transform.position = spawnPoint;
    }

    private IEnumerator SpawnEnemy()
    {
        while(Time.timeScale != 0)
        {
            if (TryGetObject(out GameObject enemy))
            {
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                SetEnemy(enemy, _spawnPoints[spawnPointNumber].position);
                yield return new WaitForSecondsRealtime(_secondsBetweenSpawn);
            }
        }
    }
}
