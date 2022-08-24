using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRigidbody;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -4;
    private GameManager gameManager;
    private MouseController mouseController;
    private bool mouseDown = false;

    public ParticleSystem explosionParticle;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();

        targetRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        targetRigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mouseController = GameObject.Find("Game Manager").GetComponent<MouseController>();
    }

    private void OnMouseOver() {
        if (gameManager.isGameActive && mouseController.isMouseDown)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(-1);

            if (gameManager.lives == 0)
            {
                gameManager.GameOver();
            }
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
