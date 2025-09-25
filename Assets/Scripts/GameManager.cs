using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   // Singleton

    public int score = 0;
    public int health = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // stays across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Show Game Over screen
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    public void ResetForNewGame()
    {
        score = 0;
        health = 3;
    }
}
