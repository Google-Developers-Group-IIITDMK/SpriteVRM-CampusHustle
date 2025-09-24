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
    [SerializeField] private int extraJumpsAllowed = 1;  // How many extra jumps in air
    private int extraJumpsRemaining;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        AutoRun();
    }

    private void AutoRun()
    {
        // Always move right automatically
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        // Detect ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset extra jumps when touching ground
        if (isGrounded)
        {
            extraJumpsRemaining = extraJumpsAllowed;
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded) // Normal jump
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (extraJumpsRemaining > 0) // Double jump
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset Y velocity for consistent height
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumpsRemaining--;
            }
        }
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
