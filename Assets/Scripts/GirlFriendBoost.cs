using UnityEngine;

public class GirlfriendBoost : MonoBehaviour
{
    [Header("Boost Settings")]
    public float floatSpeedX = 0.2f;        // very tiny right drift
    public float floatSpeedY = 4f;          // strong upward drift
    public float minDuration = 7f;
    public float maxDuration = 10f;

    [Header("Audio Settings")]
    public AudioSource musicSource;          // Assign the music AudioSource in Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                // Start daydream mode
                float randomDuration = Random.Range(minDuration, maxDuration);
                pc.StartDaydream(floatSpeedX, floatSpeedY, randomDuration);

                // Play music if assigned
                if (musicSource != null && !musicSource.isPlaying)
                {
                    musicSource.Play();
                }

                // Optional: disable or destroy girlfriend object
                // gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
