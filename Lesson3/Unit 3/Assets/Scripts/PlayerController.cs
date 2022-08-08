using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private AudioSource playerAudio;
    private AudioSource sceneAudio;

    private bool isOnGround = true;
    private bool keyUpSpace = true;
    private bool doubleJump = false;
    public bool gameOver = false;
    public bool gameStarted = false;
    public bool isRunning = false;
    public int score = 0;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravityModifier = 1.0f;
    private float slowWalkSpeed = 5f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        sceneAudio = Camera.main.GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {   if (transform.position.x < 6 && !gameStarted)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * slowWalkSpeed), Camera.main.transform);
        } else { 
            gameStarted = true;
        }

        if (gameStarted)
        {
            HandleInput();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("ObstacleStackable"))
        {
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            gameOver = true;
            Destroy(other.gameObject);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 0.7f);
            sceneAudio.Stop();
        }
    }

    public void IncrementScore() {
        if (isRunning)
        {
            score +=20;
        } else {
            score +=10;
        }
        Debug.Log("Score: " + score);
    }
    
    public void HandleInput(){
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            keyUpSpace = false;
            doubleJump = false;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            keyUpSpace = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && keyUpSpace && !doubleJump)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }
}
