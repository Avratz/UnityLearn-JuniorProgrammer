using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrebabs;
    private float spawnRangeX = 20;
    private float spawnRangeZ = 20;
    
    private float startDelay = 2f;
    private float spawnInterval = 1.5f;
    
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void SpawnRandomAnimal()
    {
        {
            int animalIndex = Random.Range(0, animalPrebabs.Length);
            Vector3 randomPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
            GameObject animalSelected = animalPrebabs[animalIndex];
            Instantiate(animalSelected, randomPosition, animalSelected.transform.rotation);
        }
    }
}
