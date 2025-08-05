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

    private void OnEnable()
    {
        playerDeviceInput.OnMove += playerMovement.Move;
        playerDeviceInput.OnLook += playerCamera.Look;
        playerDeviceInput.OnStartJump += playerMovement.StartJump;
        playerDeviceInput.OnStopJump += playerMovement.StopJump;
    }

    private void OnDisable()
    {
        playerDeviceInput.OnMove -= playerMovement.Move;
        playerDeviceInput.OnLook -= playerCamera.Look;
        playerDeviceInput.OnStartJump -= playerMovement.StartJump;
        playerDeviceInput.OnStopJump -= playerMovement.StopJump;
    }
}
