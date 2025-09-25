using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load Main Game from Main Menu
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    // Load Game Over from Main Game
    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Load Main Menu from Game Over or anywhere
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Restart the current scene (Main Game or Game Over)
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // works in Editor
        Application.Quit();     // works in Build
    }
}
