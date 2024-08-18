using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    [SerializeField] private EnemyHealth[] enemyPrefabs;

    private int currentNumberOfEnemies;
    [SerializeField] private FloatReference totalKills;

    private void Start()
    {
        TrySpawnEnemy();
        totalKills.Value = 0;
    }

    private float counter;
    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            TrySpawnEnemy();
            counter = 4;
        }
    }

    private void TrySpawnEnemy()
    {
        if (currentNumberOfEnemies < maxEnemies)
        {
            EnemyHealth enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range((int)0, (int)enemyPrefabs.Length)], GetRandomWorldPointOffScreen(2), Quaternion.identity);
            enemy.OnEnemyDied += EnemyDied;
            currentNumberOfEnemies++;
        }
    }

    private void EnemyDied()
    {
        currentNumberOfEnemies--;
        totalKills.Value++;
    }

    public Vector3 GetRandomWorldPointOffScreen(float distanceOffScreen) // ChatGPT code
    {
        // ToDo fix spawning out of bounds
        Camera camera = Camera.main;

        // Get the camera's viewport boundaries
        Vector3[] screenCorners = new Vector3[4];
        screenCorners[0] = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)); // Bottom left
        screenCorners[1] = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane)); // Bottom right
        screenCorners[2] = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane)); // Top left
        screenCorners[3] = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane)); // Top right

        // Get the bounds of the screen in world space
        Vector3 min = new Vector3(Mathf.Min(screenCorners[0].x, screenCorners[1].x),
                                   Mathf.Min(screenCorners[0].y, screenCorners[2].y),
                                   Mathf.Min(screenCorners[0].z, screenCorners[1].z));

        Vector3 max = new Vector3(Mathf.Max(screenCorners[1].x, screenCorners[3].x),
                                   Mathf.Max(screenCorners[2].y, screenCorners[3].y),
                                   Mathf.Max(screenCorners[1].z, screenCorners[3].z));

        // Randomly choose a side of the screen to generate the off-screen point
        int side = UnityEngine.Random.Range(0, 4);
        Vector3 randomPoint = Vector3.zero;

        switch (side)
        {
            case 0: // Left
                randomPoint = new Vector3(min.x - distanceOffScreen, UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
                break;
            case 1: // Right
                randomPoint = new Vector3(max.x + distanceOffScreen, UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
                break;
            case 2: // Bottom
                randomPoint = new Vector3(UnityEngine.Random.Range(min.x, max.x), min.y - distanceOffScreen, UnityEngine.Random.Range(min.z, max.z));
                break;
            case 3: // Top
                randomPoint = new Vector3(UnityEngine.Random.Range(min.x, max.x), max.y + distanceOffScreen, UnityEngine.Random.Range(min.z, max.z));
                break;
        }
        randomPoint.z = 0;
        return randomPoint;
    }
}
