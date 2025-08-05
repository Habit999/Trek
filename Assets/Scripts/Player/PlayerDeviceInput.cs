using System;
using UnityEngine;

public class PlayerDeviceInput : MonoBehaviour
{
    public event Action<Vector2, bool> OnMove;
    public event Action<Vector2> OnLook;

    public event Action OnStartJump;
    public event Action OnStopJump;

    public event Action OnStartCrouch;
    public event Action OnStopCrouch;

    public event Action OnInteract;

    [SerializeField] private ControlScheme controlScheme = new ControlScheme();
    [Space(10)]
    [SerializeField] private float mouseSensitivity;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Move
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(moveInput.magnitude > 0) OnMove?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), Input.GetKey(KeyCode.LeftShift));

        // Look
        Vector2 lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;
        if (lookInput.magnitude > 0) OnLook?.Invoke(lookInput);

        // Jumping
        if(Input.GetKeyDown(controlScheme.Jump))
            OnStartJump?.Invoke();
        if(Input.GetKeyUp(controlScheme.Jump))
            OnStopJump?.Invoke();

        // Crouching
        if(Input.GetKeyDown(controlScheme.Crouch))
            OnStartCrouch?.Invoke();
        if (Input.GetKeyUp(controlScheme.Crouch))
            OnStopCrouch?.Invoke();
        // Interact
        if (Input.GetKeyDown(controlScheme.Interact))
            OnInteract?.Invoke();
    }

}

public class ControlScheme
{
    public KeyCode Interact = KeyCode.E;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Crouch = KeyCode.C;
}
