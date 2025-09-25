using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;       // Player to follow

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 0.125f;   // smoothing factor
    [SerializeField] private Vector3 offset = new Vector3(3f, 1f, -10f); // camera offset from player
    [SerializeField] private bool lockY = true;             // lock Y axis if true

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        if (lockY)
        {
            desiredPosition.y = transform.position.y; // keep Y fixed
        }

        // Smoothly interpolate from current position to desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
