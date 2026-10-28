using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform chickVisual;
    [SerializeField] private float visualRotationSpeed = 10f;
    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;

    void Update()
    {
        GetInput();
        RotateVisual();
    }
    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    void RotateVisual()
    {
        moveDirection = horizontalInput * playerTransform.right
        + verticalInput * playerTransform.forward;

        if (moveDirection.sqrMagnitude < 0.0001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        chickVisual.rotation = Quaternion.Slerp(chickVisual.rotation, targetRotation, visualRotationSpeed * Time.deltaTime);
    }
}
