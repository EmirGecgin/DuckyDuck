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
    private Rigidbody playerRigidbody;
    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;

    void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
    }
    void Start()
    {
        
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
}
