using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Score System")]
    public int currentScore = 0;
    public int highScore = 0;
    public int scorePerPickup = 10;
    public int scorePerSecond = 1;
    private float scoreTimer = 0f;
    
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject pauseMenu;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverScoreText;
    
    [Header("Game State")]
    public bool isPaused = false;
    public bool isGameOver = false;
    
    private const string HIGH_SCORE_KEY = "HighScore";
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        LoadHighScore();
    }
    
    void Start()
    {
        UpdateScoreUI();
        UpdateHighScoreUI();
        
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }
    
    void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
        
        // Score over time when not paused
        if (!isPaused && !isGameOver)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1f)
            {
                AddScore(scorePerSecond);
                scoreTimer = 0f;
            }
        }
    }
    
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
        
        // Check if new high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            UpdateHighScoreUI();
            SaveHighScore();
        }
    }
    
    public void AddPickupScore()
    {
        AddScore(scorePerPickup);
    }
    
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore.ToString();
    }
    
    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
            
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
            
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        
        if (gameOverScoreText != null)
            gameOverScoreText.text = "Final Score: " + currentScore.ToString();
            
        if (finalScoreText != null)
            finalScoreText.text = "Score: " + currentScore.ToString();
        
        // Show game over UI
        Debug.Log("Game Over! Final Score: " + currentScore);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        isGameOver = false;
        currentScore = 0;
        scoreTimer = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        isGameOver = false;
        // Load main menu scene
        // SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.Save();
    }
    
    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }
    
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);
        PlayerPrefs.Save();
        UpdateHighScoreUI();
    }
}
