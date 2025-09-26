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

    [Header("Daydream (Girlfriend) Settings")]
    [SerializeField] private float floatSpeedX = 0.5f;     // very slow right drift
    [SerializeField] private float floatSpeedY = 250f;     // super high upward drift
    [SerializeField] private float daydreamDuration = 10f; // float for 10 seconds
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
        if (!isDaydreaming)
        {
            AutoRun();
        }
        else
        {
            // Dreamy float motion: slow right, very high up
            rb.linearVelocity = new Vector2(floatSpeedX, floatSpeedY);
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
        {
            extraJumpsRemaining = extraJumpsAllowed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
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

    public void StartDaydream()
    {
        isDaydreaming = true;
        daydreamTimer = daydreamDuration;

        rb.gravityScale = 0f;   // disable gravity
        rb.linearVelocity = Vector2.zero;

        // 👉 Optional: trigger dreamy animation/particles here
    }

    private void EndDaydream()
    {
        isDaydreaming = false;
        rb.gravityScale = defaultGravity;  // restore gravity
    }

    // Public getter for camera
    public bool IsDaydreaming()
    {
        return isDaydreaming;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
