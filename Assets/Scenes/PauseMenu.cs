using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;   // Assign Pause Menu panel in Inspector
    public static bool GameIsPaused = false;

    void Update()
    {
        // Toggle pause with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    // Resume Game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;   // Resume time
        GameIsPaused = false;
    }

    // Pause Game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;   // Freeze time
        GameIsPaused = true;
    }

    // Back to Main Menu
    public void BackToMenu()
    {
        Time.timeScale = 1f;  // Ensure time runs again
        SceneManager.LoadScene("MainMenu");
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Quit button pressed!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Editor
#else
        Application.Quit(); // Quits the game in build
#endif
    }
}
