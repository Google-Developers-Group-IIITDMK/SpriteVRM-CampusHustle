using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("MainGame"); // restart main game
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // go back to menu
    }
}


