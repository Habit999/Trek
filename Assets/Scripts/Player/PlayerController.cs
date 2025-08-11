using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerDeviceInput playerDeviceInput;
    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;

    private void Awake()
    {
        playerDeviceInput = GetComponent<PlayerDeviceInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
    }

    public void EnableMoveInput()
    {
        playerDeviceInput.OnMove += playerMovement.Move;
        playerDeviceInput.OnLook += playerCamera.Look;

        playerDeviceInput.OnStartSprint += playerMovement.StartSprint;
        playerDeviceInput.OnStopSprint += playerMovement.StopSprint;

        playerDeviceInput.OnStartJump += playerMovement.StartJump;
        playerDeviceInput.OnStopJump += playerMovement.StopJump;

        playerDeviceInput.OnStartCrouch += playerMovement.StartCrouch;
        playerDeviceInput.OnStopCrouch += playerMovement.StopCrouch;
    }

    public void DisableMoveInput()
    {
        playerDeviceInput.OnMove += playerMovement.Move;
        playerDeviceInput.OnLook += playerCamera.Look;

        playerDeviceInput.OnStartSprint += playerMovement.StartSprint;
        playerDeviceInput.OnStopSprint += playerMovement.StopSprint;

        playerDeviceInput.OnStartJump += playerMovement.StartJump;
        playerDeviceInput.OnStopJump += playerMovement.StopJump;

        playerDeviceInput.OnStartCrouch += playerMovement.StartCrouch;
        playerDeviceInput.OnStopCrouch += playerMovement.StopCrouch;
    }

    private void OnEnable()
    {
        EnableMoveInput();
    }

    private void OnDisable()
    {
        DisableMoveInput();
    }
}
