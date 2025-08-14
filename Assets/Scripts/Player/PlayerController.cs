using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerDeviceInput playerDeviceInput;
    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;
    private PlayerInteraction playerInteraction;
    [HideInInspector] public PlayerCharacterManager CharacterManager;

    private Camera mainCamera;

    private bool canObserve;

    private void Awake()
    {
        playerDeviceInput = GetComponent<PlayerDeviceInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerInteraction = GetComponent<PlayerInteraction>();
        CharacterManager = GetComponent<PlayerCharacterManager>();

        mainCamera = Camera.main;
    }

    public void EnableMoveInput()
    {
        canObserve = true;

        playerDeviceInput.OnMove += playerMovement.Move;
        playerDeviceInput.OnLook += playerCamera.Look;

        playerDeviceInput.OnStartSprint += playerMovement.StartSprint;
        playerDeviceInput.OnStopSprint += playerMovement.StopSprint;

        playerDeviceInput.OnStartJump += playerMovement.StartJump;
        playerDeviceInput.OnStopJump += playerMovement.StopJump;

        playerDeviceInput.OnStartCrouch += playerMovement.StartCrouch;
        playerDeviceInput.OnStopCrouch += playerMovement.StopCrouch;

        playerDeviceInput.OnInteract += playerInteraction.TryInteract;
    }

    public void DisableMoveInput()
    {
        canObserve = false;

        playerDeviceInput.OnMove -= playerMovement.Move;
        playerDeviceInput.OnLook -= playerCamera.Look;

        playerDeviceInput.OnStartSprint -= playerMovement.StartSprint;
        playerDeviceInput.OnStopSprint -= playerMovement.StopSprint;

        playerDeviceInput.OnStartJump -= playerMovement.StartJump;
        playerDeviceInput.OnStopJump -= playerMovement.StopJump;

        playerDeviceInput.OnStartCrouch -= playerMovement.StartCrouch;
        playerDeviceInput.OnStopCrouch -= playerMovement.StopCrouch;

        playerDeviceInput.OnInteract -= playerInteraction.TryInteract;
    }

    public void SetMainCameraActive(bool active)
    {
        mainCamera.gameObject.SetActive(active);
    }

    private void OnEnable()
    {
        EnableMoveInput();
    }

    private void OnDisable()
    {
        DisableMoveInput();
    }

    private void Update()
    {
        if (canObserve) playerInteraction.Observe();
    }
}
