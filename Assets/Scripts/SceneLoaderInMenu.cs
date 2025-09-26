using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Pause Menu UI")]
    public GameObject pauseMenuUI; // assign your Pause Menu Canvas here

    // Load Main Game from Main Menu
    public void LoadMainGame()
    {
        Time.timeScale = 1f; // ensure game is running
        SceneManager.LoadScene("MainGame");
    }

    // Load Game Over from Main Game
    public void LoadGameOver()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("GameOver");
    }

    // Load Main Menu from Game Over or anywhere
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Restart the current scene
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();
    }

    // ---------------- Pause / Continue ----------------

    // Pause the game
    public void PauseGame()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);  // show Pause Menu

        Time.timeScale = 0f;            // stop gameplay
    }

    // Continue the game
    public void ContinueGame()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false); // hide Pause Menu

        Time.timeScale = 1f;            // resume gameplay
    }
}
