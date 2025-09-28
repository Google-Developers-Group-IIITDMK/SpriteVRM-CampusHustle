using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController; // reference to player script

    [Header("Follow Settings")]
    [SerializeField] private float smoothTime = 0.15f; // smaller = snappier
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);

    [Header("Daydream Y Follow")]
    public bool isDaydreaming = false;

    private bool followY = false;   // helper flag
    private Vector3 velocity = Vector3.zero; // SmoothDamp velocity tracker

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = transform.position;

        // --- Always follow player's X ---
        targetPos.x = player.position.x + offset.x;

        // --- Handle Y follow ---
        if (isDaydreaming)
            followY = true;

        if (followY)
        {
            targetPos.y = player.position.y + offset.y;

            if (!isDaydreaming && playerController != null && playerController.IsGrounded)
            {
                followY = false;
                targetPos.y = offset.y;
            }
        }

        // --- Extra: Boost responsiveness if player is falling fast ---
        float effectiveSmoothTime = smoothTime;

        if (followY && player.TryGetComponent(out Rigidbody2D rb))
        {
            if (rb.linearVelocity.y < -5f) // falling faster than -5
                effectiveSmoothTime *= 0.5f; // snap faster
        }

        // --- Smoothly move camera ---
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, effectiveSmoothTime);
    }

    // Optional reset method for scene reloads
    public void ResetFollowY()
    {
        followY = false;
        isDaydreaming = false;
        transform.position = new Vector3(player.position.x + offset.x, offset.y, offset.z);
    }
}
