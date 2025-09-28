using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int score = 0;
    public int health = 3;
    public int maxHealth = 3;

    [Header("UI Elements")]
    public Text scoreText;
    public Slider healthBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called every time a scene loads
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            ResetForNewGame();
            UpdateUIReferences();
        }
    }

    private void UpdateUIReferences()
    {
        // Find current scene UI objects
        scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        healthBar = GameObject.Find("HealthBar")?.GetComponent<Slider>();

        if (scoreText == null)
            Debug.LogError("ScoreText not found in scene!");
        if (healthBar == null)
            Debug.LogError("HealthBar not found in scene!");

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateUI();

        if (health <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void ResetForNewGame()
    {
        score = 0;
        health = maxHealth;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
    }

    // Restart button should call this
    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
