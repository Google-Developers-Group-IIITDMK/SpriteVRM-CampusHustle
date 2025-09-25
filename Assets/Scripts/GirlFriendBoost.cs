using UnityEngine;

public class GirlfriendBoost : MonoBehaviour
{
    public float boostForce = 10f;      // Upward force
    public float forwardForce = 5f;     // Horizontal distance
    private bool isBoosting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isBoosting)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                isBoosting = true;

                // Reset velocity (now linearVelocity in Unity 6+)
                rb.linearVelocity = Vector2.zero;

                // Apply projectile-like force
                rb.AddForce(new Vector2(forwardForce, boostForce), ForceMode2D.Impulse);

                // Start coroutine for landing after 1 second
                StartCoroutine(LandAfterTime(rb, 1.0f));
            }
        }
    }

    private System.Collections.IEnumerator LandAfterTime(Rigidbody2D rb, float time)
    {
        yield return new WaitForSeconds(time);

        // Stop vertical motion, keep running horizontally
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        isBoosting = false;
    }
}
