using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    [SerializeField] private float speed = 300f;
    private float speedIncreaseModifier = 10;
    private float maximumSpeed = 500f; // same as player without boost
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        
        enemyRb.AddForce(lookDirection * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

    public void IncrementSpeed(int waveCount)
    {
        if(speed < maximumSpeed){
            speed = speed + waveCount * speedIncreaseModifier;
        }
    }
}
