using UnityEngine;

public class OnSystemMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3f;

    [Header("Look Parameters")]
    [SerializeField] private float mouseSensitivity = 0.1f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private OnSystemInputHandler playerInputHandler;

    private Vector3 currentMovement;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.movementInput.x, 0, playerInputHandler.movementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * walkSpeed;
        currentMovement.z = worldDirection.z * walkSpeed;


        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.rotationInput.x * mouseSensitivity;

        ApplyHorizontalRotation(mouseXRotation);
    }
     
}
