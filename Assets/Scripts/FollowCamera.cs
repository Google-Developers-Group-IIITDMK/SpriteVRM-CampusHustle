using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Follow Settings")]
    [SerializeField] private float followSpeed = 8f; // higher = snappier
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);

    [Header("Daydream Y Follow")]
    public bool isDaydreaming = false;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = transform.position;

        // Always follow player's X
        targetPos.x = player.position.x + offset.x;

        // Follow Y only while daydreaming
        if (isDaydreaming)
            targetPos.y = player.position.y + offset.y;

        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
