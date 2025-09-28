using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided
        if(other.CompareTag("Player"))
        {
            // Destroy this collectible
            Destroy(gameObject);
        }
    }
}
