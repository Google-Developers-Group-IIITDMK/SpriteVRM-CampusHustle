using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("SampleScene"); // restart main game
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("main menu"); // go back to menu
    }
}


