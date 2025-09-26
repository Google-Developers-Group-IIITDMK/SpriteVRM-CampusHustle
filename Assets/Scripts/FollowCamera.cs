using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;       

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 0.2f;   
    [SerializeField] private Vector3 offset = new Vector3(3f, 1f, -10f); 

    private PlayerController playerController;

    private void Awake()
    {
        if (target != null)
            playerController = target.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Smooth X follow
        float smoothedX = Mathf.Lerp(transform.position.x, desiredPosition.x, smoothSpeed);

        // Smooth Y follow, snap if daydreaming
        float smoothedY;
        if (playerController != null && playerController.IsDaydreaming())
        {
            smoothedY = desiredPosition.y; // snap instantly
        }
        else
        {
            smoothedY = Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeed);
        }

        // Apply final position
        transform.position = new Vector3(smoothedX, smoothedY, desiredPosition.z);
    }
}
