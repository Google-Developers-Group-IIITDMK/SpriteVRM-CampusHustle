using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float fallY = -10f;   // Y position at which player is considered fallen
    private bool triggered = false;

    private SceneLoader sceneLoader;

    void Start()
    {
        // Use new Unity API instead of deprecated FindObjectOfType
        sceneLoader = FindFirstObjectByType<SceneLoader>();
    }

    void Update()
    {
        if (triggered) return;

        if (transform.position.y < fallY)
        {
            triggered = true;

            if (sceneLoader != null)
                sceneLoader.LoadGameOver();
            else
                Debug.LogError("⚠️ SceneLoader not found in the scene!");
        }
    }
}
