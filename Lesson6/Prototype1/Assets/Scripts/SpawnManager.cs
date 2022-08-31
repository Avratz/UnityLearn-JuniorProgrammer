using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    private float yBoundary = 12.0f;
    private float xSpawn = 30.0f;
    private float spawnRate = 2.0f;

    private void Start() {
        InvokeRepeating("SpawnRandomEnemy", 1, spawnRate);
    }

    private void SpawnRandomEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemies.Count);
        GameObject randomEnemy = enemies[randomEnemyIndex];
        Vector3 randomPosition = GenerateRandomPosition();
        
        Instantiate(randomEnemy, randomPosition, randomEnemy.transform.rotation);
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomY = Random.Range(-yBoundary, yBoundary);
        Vector3 randomPosition = new Vector3(xSpawn, randomY, 0);

        return randomPosition;
    }
}
