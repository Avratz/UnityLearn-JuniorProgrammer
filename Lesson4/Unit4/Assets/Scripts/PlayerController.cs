using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private bool hasPowerup = false;
    [SerializeField]
    private GameObject powerupIndicator;

    private float powerupStength = 15.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = focalPoint.transform.forward;
        playerRb.AddForce(direction * (verticalInput * speed));

        powerupIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            GameObject enemy = other.gameObject;
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = enemy.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerupStength, ForceMode.Impulse);
            Debug.Log("Collide with: " + enemy.name + " with powerup "+ hasPowerup);
        }
    }
}
