using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteUI; // Optional: UI popup

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete!"); // For testing

            // Show UI or trigger win sequence
            if (levelCompleteUI != null)
            {
                levelCompleteUI.SetActive(true);
            }

            // Stop player movement (optional)
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            other.GetComponent<PlayerController>().enabled = false;
        }
    }
}
