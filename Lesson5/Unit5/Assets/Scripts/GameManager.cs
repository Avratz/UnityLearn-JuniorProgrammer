using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pausedText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject lightGameObject;
    public bool isGameActive;
    public int lives;
    
    private Color defaultColor = Color.white;
    private Color darkColor = Color.black;
    private Light directionalLight;
    private int score;
    private float spawnRate = 1.0f;
    private bool isGamePaused;
    private float lightChangeTime;

    private void Start() {
        directionalLight = lightGameObject.GetComponent<Light>();
        lightChangeTime = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
    }

    private void Update() {
        if (isGameActive && Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
            {
                ResumeGame();
            } else 
            {
                PauseGame();
            }
        }    
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lifeToAdd)
    {
        if (isGameActive)
        {
            lives += lifeToAdd;
            livesText.text = "Lives: " + lives;
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        isGamePaused = false;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;
        
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.SetActive(false);
    }

    void PauseGame ()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        pausedText.gameObject.SetActive(true);
        directionalLight.color = Color.Lerp(darkColor, defaultColor, lightChangeTime);
    }
    void ResumeGame ()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        pausedText.gameObject.SetActive(false);
        directionalLight.color = Color.Lerp(defaultColor, darkColor, lightChangeTime);
    }
}
