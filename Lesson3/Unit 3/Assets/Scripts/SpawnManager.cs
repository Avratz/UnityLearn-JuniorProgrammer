using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 3;
    private PlayerController playerControllerScript;
    
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle () 
    {
        if (!playerControllerScript.gameOver && playerControllerScript.gameStarted)
        {
            GameObject obstaclePrefab = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            if (obstaclePrefab.CompareTag("ObstacleStackable"))
            {
                Vector3 spawnPosTop = new Vector3(25, 1.5f, 0);
                Instantiate(obstaclePrefab, spawnPosTop, obstaclePrefab.transform.rotation);
            }
        }
    }
}
