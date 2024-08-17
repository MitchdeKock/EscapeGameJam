using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    [SerializeField] private EnemyHealth enemyPrefab;
    [SerializeField] private Collider2D spawnArea;

    private int currentNumberOfEnemies;

    private void Start()
    {
        TrySpawnEnemy();
    }

    private void Update()
    {

    }

    private void TrySpawnEnemy()
    {
        EnemyHealth enemy = Instantiate(enemyPrefab, GetRandomPointOffScreen(), Quaternion.identity);
        enemy.OnEnemyDied += TrySpawnEnemy;
    }

    private Vector3 GetRandomPointOffScreen()
    {
        float x = UnityEngine.Random.Range(-0.1f, 0.1f);
        float y = UnityEngine.Random.Range(-0.1f, 0.1f);
        if (x > 0)
        {
            x =+ 1;
        }
        if (y > 0)
        {
            y =+ 1;
        }
        Vector3 randomPoint = new(x, y);

        randomPoint.z = 10f;
        Debug.Log(randomPoint);
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(randomPoint);

        return worldPoint;
    }
}
