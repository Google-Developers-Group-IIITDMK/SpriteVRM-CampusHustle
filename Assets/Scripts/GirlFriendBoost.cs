using UnityEngine;

public class GirlfriendBoost : MonoBehaviour
{
    [Header("Boost Settings")]
    public float floatSpeedX = 0.2f;        // very tiny right drift
    public float floatSpeedY = 4f;          // strong upward drift
    public float minDuration = 7f;
    public float maxDuration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                // tell player to start daydream mode with settings from this object
                float randomDuration = Random.Range(minDuration, maxDuration);
                pc.StartDaydream(floatSpeedX, floatSpeedY, randomDuration);

                // (Optional) disable girlfriend sprite after collision
                // gameObject.SetActive(false);

                // (Optional) destroy girlfriend object
                // Destroy(gameObject);
            }
        }
    }
}
