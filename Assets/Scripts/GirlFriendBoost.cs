using UnityEngine;

public class GirlfriendBoost : MonoBehaviour
{
    [Header("Boost Settings")]
    public float floatSpeedX = 0.5f;      // very slow right drift
    public float floatSpeedY = 250f;      // strong upward drift
    public float daydreamDuration = 10f;  // how long the float lasts

    private bool isBoosting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isBoosting)
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                isBoosting = true;

                // tell player to start daydream mode
                pc.StartDaydream();

                // (Optional) disable girlfriend sprite after collision
                // gameObject.SetActive(false);
            }
        }
    }
}
