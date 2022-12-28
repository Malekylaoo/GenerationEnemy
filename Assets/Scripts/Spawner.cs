using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy[] _enemyPrefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private int _capacity;

    private List<Enemy> _pool = new List<Enemy>();

    private void Start()
    {
        Initialize(_enemyPrefab);
        StartCoroutine(SpawnEnemy());
    }

    private void SetEnemy(Enemy enemy, Vector3 spawnPoint)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }

    private IEnumerator SpawnEnemy()
    {
        while(Time.timeScale != 0)
        {
            if (TryGetObject(out Enemy enemy))
            {
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                SetEnemy(enemy, _spawnPoints[spawnPointNumber].position);
                yield return new WaitForSecondsRealtime(_secondsBetweenSpawn);
            }
        }
    }

    private void Initialize(Enemy prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Enemy spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    private void Initialize(Enemy[] prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int prefabIndex = Random.Range(0, prefab.Length);
            var spawned = Instantiate(prefab[prefabIndex], _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    private bool TryGetObject(out Enemy result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
