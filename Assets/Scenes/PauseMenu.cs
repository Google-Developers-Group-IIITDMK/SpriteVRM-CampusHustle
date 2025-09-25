using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Text finalScoreText;

    public void Start()
    {
        if (finalScoreText != null && GameManager.Instance != null)
            finalScoreText.text = "Score: " + GameManager.Instance.score;
    }

    public void Retry()
    {
        GameManager.Instance.ResetForNewGame();
        SceneManager.LoadScene("MainGame");
    }

    public void BackToMenu()
    {
        GameManager.Instance.ResetForNewGame();
        SceneManager.LoadScene("MainMenu");
    }
}


