using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // loads MainGame scene
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        // only works in a built game, not in editor
        Application.Quit();
        Debug.Log("Game Quit!"); 
    }
}
