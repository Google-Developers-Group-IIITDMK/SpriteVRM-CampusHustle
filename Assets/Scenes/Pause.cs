using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public void LoadPauseMenu()
    {
        SceneManager.LoadScene("Pause Menu");
    } }
