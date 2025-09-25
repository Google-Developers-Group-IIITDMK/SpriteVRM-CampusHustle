using UnityEngine;
using UnityEngine.SceneManagement; // needed for scene loading

public class RaggingSenior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Directly load GameOver scene
            SceneManager.LoadScene("GameOver");
        }
    }
}
