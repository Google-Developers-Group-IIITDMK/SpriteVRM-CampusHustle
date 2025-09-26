using UnityEngine;
using UnityEngine.SceneManagement; // for scene reload or GameOver scene

public class RaggingSenior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Caught by Ragging Senior! Game Over!");

            // Option 1: Reload current level
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Option 2: Load a separate Game Over scene
            SceneManager.LoadScene("GameOver");

            // Option 3: If you have a GameManager, trigger it:
            // GameManager.Instance.GameOver();
        }
    }
}
