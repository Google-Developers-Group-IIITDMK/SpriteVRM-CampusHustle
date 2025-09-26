using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;   // Auto-run speed
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Double Jump Settings")]
    [SerializeField] private int extraJumpsAllowed = 1;  
    private int extraJumpsRemaining;

    [Header("Daydream (Hallucination) Settings")]
    [SerializeField] private float floatSpeedX = 0.5f;       // tiny horizontal sway
    [SerializeField] private float floatSpeedY = 0.2f;         // slow upward drift
    [SerializeField] private float minDaydreamDuration = 7f;
    [SerializeField] private float maxDaydreamDuration = 10f;
    [SerializeField] private float wobbleFrequencyX = 2f;
    [SerializeField] private float wobbleFrequencyY = 2f;
    [SerializeField] private float tiltAngleAmount = 5f;   // rotation wobble

    private float daydreamStartY;
    [SerializeField] private float maxFloatHeight = 0.5f; // max upward drift from start


    private bool isDaydreaming = false;
    private float daydreamTimer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float defaultGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        defaultGravity = rb.gravityScale;
    }

    private void Update()
    {
        if (!isDaydreaming)
            HandleJump();

        HandleDaydreamTimer();
    }

    private void FixedUpdate()
    {
        if (isDaydreaming)
        {
            // Horizontal wobble
            float dizzyX = floatSpeedX * Mathf.Sin(Time.time * wobbleFrequencyX);

            // Vertical motion: only allow small upward drift
            float targetY = daydreamStartY + maxFloatHeight;
            float dizzyY = Mathf.Min(floatSpeedY + floatSpeedY * 0.3f * Mathf.Sin(Time.time * wobbleFrequencyY),
                                    targetY - transform.position.y);

            rb.linearVelocity = new Vector2(dizzyX, dizzyY);

            // Gentle rotation
            rb.rotation = tiltAngleAmount * Mathf.Sin(Time.time * wobbleFrequencyX);
        }

        else
        {
            AutoRun();
        }

    }

    private void AutoRun()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
            extraJumpsRemaining = extraJumpsAllowed;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (extraJumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumpsRemaining--;
            }
        }
    }

    private void HandleDaydreamTimer()
    {
        if (isDaydreaming)
        {
            daydreamTimer -= Time.deltaTime;
            if (daydreamTimer <= 0f)
            {
                EndDaydream();
            }
        }
    }

    // Default daydream using inspector values
    public void StartDaydream()
    {
        isDaydreaming = true;
        daydreamTimer = Random.Range(minDaydreamDuration, maxDaydreamDuration);

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        daydreamStartY = transform.position.y; // record start height
    }


    // Configurable daydream (used by GirlfriendBoost)
    public void StartDaydream(float fx, float fy, float duration)
    {
        isDaydreaming = true;
        daydreamTimer = duration;

        floatSpeedX = fx;
        floatSpeedY = fy;

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    private void EndDaydream()
    {
        isDaydreaming = false;
        rb.gravityScale = defaultGravity;
        rb.rotation = 0f; // reset rotation after daydream
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    // Optional getter for other scripts
    public bool IsDaydreaming()
    {
        return isDaydreaming;
    }
}
