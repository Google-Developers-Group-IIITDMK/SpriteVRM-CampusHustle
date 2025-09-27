using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Text finalScoreText;

    public void Start()
    {
        // Show score only if references exist
        if (finalScoreText != null && GameManager.Instance != null)
        {
            finalScoreText.text = "Score: " + GameManager.Instance.score;
        }
    }

    public void Retry()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResetForNewGame();

        SceneManager.LoadScene("MainGame");
    }

    public void BackToMenu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResetForNewGame();

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed!"); // Useful for testing in Editor

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the Editor
#else
        Application.Quit(); // Quits the game in a build
#endif
    }
}
