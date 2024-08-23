using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    [SerializeField] private EnemyHealth[] enemyPrefabs;
    [SerializeField] private DifficultyManager difficultyManager;
    [SerializeField] private FloatReference totalKills;
    [SerializeField] private FloatReference secondsPlayed;
    [SerializeField] private float[] enemyWeights; //used to regulate the spawn rate of each enemy type 

    private int currentNumberOfEnemies;
    private float spawnCounter;

    private void Start()
    {
        if (enemyWeights.Length != enemyPrefabs.Length) 
            throw new Exception("Need to have same number of weights as number of enemies");
        TrySpawnEnemy();
        totalKills.Value = 0;
        secondsPlayed.Value = 0;
    }

    private void Update()
    {
        if (spawnCounter > 0)
        {
            spawnCounter -= Time.deltaTime;
        }
        else
        {
            TrySpawnEnemy();
            spawnCounter = difficultyManager.SpawnRate;
            Debug.Log($"Time Between Spawns: {spawnCounter}");
        }

        secondsPlayed.Value += Time.deltaTime;
        
    }

    private void TrySpawnEnemy()
    {
        if (currentNumberOfEnemies < maxEnemies)
        {
            Debug.Log($"Enemy multiplier: {difficultyManager.EnemyMultiplier}");
            int enemyIndex = GetWeightedRandomIndex();
            EnemyHealth enemy = Instantiate(enemyPrefabs[enemyIndex], GetRandomWorldPointOffScreen(2), Quaternion.identity);
            enemy.OnEnemyDied += EnemyDied;
            currentNumberOfEnemies++;
        }
    }

    private int GetWeightedRandomIndex()
    {
        float totalWeight = 0;
        foreach (float weight in enemyWeights) //resiliance incase weights add up to more than 1
        {
            totalWeight += weight;
        }

        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        for (int i = 0; i < enemyWeights.Length; i++)
        {
            if (randomValue < enemyWeights[i])
            {
                return i;
            }
            randomValue -= enemyWeights[i];
        }

        return 0; // Fallback in case of rounding errors
    }

    private void EnemyDied()
    {
        currentNumberOfEnemies--;
        totalKills.Value++;
    }

    public Vector3 GetRandomWorldPointOffScreen(float distanceOffScreen) // ChatGPT code
    {
        Vector3 randomPoint = Vector3.zero;
        do
        {
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
        } while (!checkValid(randomPoint.x) || !checkValid(randomPoint.y)); //get new point if off the map

        randomPoint.z = 0;
        return randomPoint;
    }

    //Check if in bounds
    public bool checkValid(float val)
    {
        return val>-49.5 && val<49.5;
    }
}
