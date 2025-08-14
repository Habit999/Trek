using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    private Transform playerBody;

    private float pitch;
    private float yaw;

    private void Awake()
    {
        playerBody = transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Look(Vector2 lookInput)
    {
        if (playerCam == null) return;

        yaw += lookInput.x * Time.deltaTime * 10;
        pitch -= lookInput.y * Time.deltaTime * 10;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        playerCam.rotation = Quaternion.Euler(pitch, yaw, 0);
        playerBody.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
