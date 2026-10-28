using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float jumpResetTime = 1f;
    [SerializeField] private float playerHeight = 1f;
    [SerializeField] private LayerMask groundLayer = default;
    [SerializeField] private KeyCode slideKey = KeyCode.Q;
    [SerializeField] private KeyCode resetSlideKey = KeyCode.E;
    [SerializeField] private float slideSpeedMultiplier = 2f;
    [SerializeField] private float slideDrag = 1f;
    private Rigidbody playerRigidbody;
    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;
    private float normalDrag;
    private bool isSliding = false;

    void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        normalDrag = playerRigidbody.linearDamping;
    }
    void Update()
    {
        GetInput();
        if (Input.GetKeyDown(jumpKey) && isGrounded && IsGrounded())
        {
            isGrounded = false;
            Jump();
            Invoke("ResetJump", jumpResetTime);

        }
        else if (Input.GetKeyDown(slideKey) && !isSliding)
        {
            StartSlide();
        }
        else if (Input.GetKeyDown(resetSlideKey) && isSliding)
        {
            ResetSlide();
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    void MovePlayer()
    {
        moveDirection = horizontalInput * playerTransform.right 
        + verticalInput * playerTransform.forward;

        playerRigidbody.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
    void Jump()
    {
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void ResetJump()
    {
        isGrounded = true;
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    }
    void StartSlide()
    {
        isSliding = true;
        moveSpeed *= slideSpeedMultiplier;
        playerRigidbody.linearDamping = slideDrag;
    }
    void ResetSlide()
    {
        isSliding = false;
        moveSpeed /= slideSpeedMultiplier;
        playerRigidbody.linearDamping = normalDrag;
    }
}
