using UnityEngine;

public enum CollectibleType
{
    PositiveScore, // +10 Score
    NegativeScore, // -10 Score
    MessFood,      // -2 Health
    Biryani,       // +2 Health
    Rock           // -5 Health
}

public class Collectible : MonoBehaviour
{
    public CollectibleType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (GameManager.Instance != null)
        {
            switch (type)
            {
                case CollectibleType.PositiveScore:
                    GameManager.Instance.AddScore(10);
                    break;

                case CollectibleType.NegativeScore:
                    GameManager.Instance.AddScore(-10);
                    break;

                case CollectibleType.MessFood:
                    GameManager.Instance.TakeDamage(2);
                    break;

                case CollectibleType.Biryani:
                    GameManager.Instance.TakeDamage(-2); // heal
                    break;

                case CollectibleType.Rock:
                    GameManager.Instance.TakeDamage(5);
                    break;
            }
        }

        Destroy(gameObject); // remove collectible after pickup
    }
}
