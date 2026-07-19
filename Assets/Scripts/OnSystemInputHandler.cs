using UnityEngine;
using UnityEngine.InputSystem;
public class OnSystemInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset PlayerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "OnSystem";


    [Header("Action Name Reference")]
    [SerializeField] private string movement = "Move";
    [SerializeField] private string rotation = "Look";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string gunSelection = "GunSelection";

    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction attackAction;
    private InputAction interactAction;
    private InputAction gunSelectionAction;


    public Vector2 movementInput { get; private set; }
    public Vector2 rotationInput { get; private set; }
    public Vector2 gunSelectionInput { get; private set; }
    public bool attackTriggered{ get; private set; }
    public bool interactionTrigered { get; private set; }

    private void Awake()
    {
        InputActionMap mapReference = PlayerControls.FindActionMap(actionMapName);

        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        attackAction = mapReference.FindAction(attack);
        interactAction = mapReference.FindAction(interact);
        gunSelectionAction = mapReference.FindAction(gunSelection);

        SubscribeActionValuesToInputEvents();
    }

    private void Update()
    {
        if (gunSelectionInput != Vector2.zero)
        {
            Debug.Log(gunSelectionInput);
        }
        
    }
    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo => movementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => movementInput = Vector2.zero;

        rotationAction.performed += inputInfo => rotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => rotationInput = Vector2.zero;

        gunSelectionAction.performed += inputInfo => gunSelectionInput = inputInfo.ReadValue<Vector2>();
        gunSelectionAction.canceled += inputInfo => gunSelectionInput = Vector2.zero;

        attackAction.performed += inputInfo => attackTriggered = true;
        attackAction.canceled += inputInfo => attackTriggered = false;

        interactAction.performed += inputInfo => interactionTrigered = true;
        interactAction.canceled += inputInfo => interactionTrigered = false;


    }

    private void OnEnable()
    {
        PlayerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        PlayerControls.FindActionMap(actionMapName).Disable();
    }

    public void ConsumeAttack()
    {
        attackTriggered = false;
    }
}
