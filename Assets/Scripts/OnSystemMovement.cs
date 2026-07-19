using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Image weaponImage;
    [SerializeField] private Sprite idleSprite, shootingSprite;


    [Header("Floats")]
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private float gunDamage = 10f;
    [SerializeField] private float spriteTimeOnScreen = 0.5f;
    [SerializeField] private float spriteTimeOnScreenOG;
    [SerializeField] private float shootFreq = 0.5f;
    [SerializeField] private float shootFreqOG;
   
    private Vector3 currentMovement;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentMovement.y = -50f;
        spriteTimeOnScreenOG = spriteTimeOnScreen;
        shootFreqOG = shootFreq;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        Shooting();
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

    private void Shooting()
    {
        shootFreq -= Time.deltaTime;
        if (playerInputHandler.attackTriggered)
        {
            playerInputHandler.ConsumeAttack();
           
            if (shootFreq <= 0)
            {
                shootFreq = shootFreqOG;
                weaponImage.sprite = shootingSprite;
                spriteTimeOnScreen = spriteTimeOnScreenOG;

                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, shootRange))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<OnSystemEnemyHealth>().HurtEnemy(gunDamage);

                    }
                }

            }  
        }
        else
        {
            if (spriteTimeOnScreen > 0)
            {
                spriteTimeOnScreen -= Time.deltaTime;
            }
            else
            {
                weaponImage.sprite = idleSprite;
            }
        }
        
    }
}
