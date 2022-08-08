
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed;
    private PlayerController playerControllerScript;
    private float leftBoundary = -15;
    private bool incrementedScore = false;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = 10;
        if (!playerControllerScript.gameOver && playerControllerScript.gameStarted)
        {
            if (playerControllerScript.isRunning)
            {
                speed += 5;
            } 
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
        }
        if (gameObject.CompareTag("Obstacle") || gameObject.CompareTag("ObstacleStackable"))
        {
            if (transform.position.x < leftBoundary)
            {
                Destroy(gameObject);
                incrementedScore = false;
            }

            if (transform.position.x < playerControllerScript.transform.position.x && !playerControllerScript.gameOver && !incrementedScore)
            {
                playerControllerScript.IncrementScore();
                incrementedScore = true;
            }
        }
    }
}
