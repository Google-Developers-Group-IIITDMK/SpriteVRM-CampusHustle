using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform; // Assign Main Camera here
    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset between background and camera
        offset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        // Keep background at the same offset relative to camera
        Vector3 newPos = cameraTransform.position + offset;
        newPos.z = transform.position.z; // Keep original Z
        transform.position = newPos;
    }
}
