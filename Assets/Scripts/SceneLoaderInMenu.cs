using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // ---------------- Scene Loading ----------------

    // Load Main Game from Main Menu
    public void LoadMainGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainGame");
    }

    // Load Game Over from Main Game
    public void LoadGameOver()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("GameOver");
    }

    // Load Main Menu from anywhere
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

    // Open Pause Menu as a separate scene (additive)
    public void PauseGame()
    {
        Time.timeScale = 0f; // freeze gameplay

        // Prevent loading PauseMenu multiple times
        if (!SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }

    // Close Pause Menu and resume gameplay
    public void ContinueGame()
    {
        Time.timeScale = 1f; // resume gameplay

        // unload PauseMenu if it’s loaded
        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.UnloadSceneAsync("PauseMenu");
    }
}
