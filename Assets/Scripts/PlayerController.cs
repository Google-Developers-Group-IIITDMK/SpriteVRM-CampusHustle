using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;   
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Double Jump Settings")]
    [SerializeField] private int extraJumpsAllowed = 1;
    private int extraJumpsRemaining;

    [Header("Daydream (Hallucination) Settings")]
    [SerializeField] private float floatSpeedX = 0.5f;
    [SerializeField] private float driftSpeedX = 0.8f;
    [SerializeField] private float floatSpeedY = 0.2f;
    [SerializeField] private float minDaydreamDuration = 7f;
    [SerializeField] private float maxDaydreamDuration = 10f;
    [SerializeField] private float wobbleFrequencyX = 2f;
    [SerializeField] private float wobbleFrequencyY = 2f;
    [SerializeField] private float tiltAngleAmount = 5f;

    private bool isDaydreaming = false;
    private float daydreamTimer;

    private Rigidbody2D rb;
    private float defaultGravity;

    private FollowCamera followCamera;

    public bool IsGrounded { get; private set; }  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        defaultGravity = rb.gravityScale;

        followCamera = Camera.main.GetComponent<FollowCamera>();
    }

    private void Update()
    {
        if (!isDaydreaming)
            HandleJump();

        HandleDaydreamTimer();
    }

    private void FixedUpdate()
    {
        GroundUpdate();

        if (isDaydreaming)
            HandleDaydreamMovement();
        else
            AutoRun();
    }

    private void AutoRun()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (IsGrounded)
            extraJumpsRemaining = extraJumpsAllowed;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded)
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
                EndDaydream();
        }
    }

    private void HandleDaydreamMovement()
    {
        float driftX = driftSpeedX;
        float wobbleX = floatSpeedX * Mathf.Sin(Time.time * wobbleFrequencyX);

        float baseUp = floatSpeedY;
        float wobbleY = floatSpeedY * 0.5f * Mathf.Sin(Time.time * wobbleFrequencyY);

        float dizzyX = driftX + wobbleX;
        float dizzyY = baseUp + wobbleY;

        rb.linearVelocity = new Vector2(dizzyX, dizzyY);
        rb.rotation = tiltAngleAmount * Mathf.Sin(Time.time * wobbleFrequencyX);
    }

    public void StartDaydream()
    {
        isDaydreaming = true;
        daydreamTimer = Random.Range(minDaydreamDuration, maxDaydreamDuration);

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        if (followCamera != null)
            followCamera.isDaydreaming = true;
    }

    public void StartDaydream(float fx, float fy, float duration)
    {
        isDaydreaming = true;
        daydreamTimer = duration;

        floatSpeedX = fx;
        floatSpeedY = fy;

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        if (followCamera != null)
            followCamera.isDaydreaming = true;
    }

    private void EndDaydream()
    {
        isDaydreaming = false;
        rb.gravityScale = defaultGravity;
        rb.rotation = 0f;

        if (followCamera != null)
            followCamera.isDaydreaming = false;
    }

    private void GroundUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public bool IsDaydreaming()
    {
        return isDaydreaming;
    }
}
