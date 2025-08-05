using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // References
    private CharacterController characterController;

    // Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [Space(10)]
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpForce;
    [Space(10)]
    [SerializeField] private float gravityInfluence = 100;

    private float jumpCounter;
    private float jumpTimer;

    private bool isJumping;
    private bool jumpMaxed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();
        if (isJumping) Jumping();
    }

    public void Move(Vector2 moveInput, bool isSprinting)
    {
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 velocity = direction * (isSprinting ? sprintSpeed : walkSpeed) * Time.deltaTime * 10;
        characterController.Move(velocity);
    }

    public void StartJump()
    {
        if(characterController.isGrounded && !isJumping)
        {
            isJumping = true;
            jumpMaxed = false;
            jumpTimer = 0;
            jumpCounter = 0;
        }
        else if(!characterController.isGrounded && !isJumping && jumpCounter < 2)
        {
            isJumping = true;
            jumpTimer = 0;
            jumpCounter = 1;
        }

    }

    public void StopJump()
    {
        isJumping = false;

        if (jumpMaxed) jumpMaxed = false;
        else jumpCounter++;
        
    }

    private void Jumping()
    {
        characterController.Move(new Vector3(0, jumpForce, 0) * Time.deltaTime * 10);
        jumpTimer += Time.deltaTime;

        if(jumpTimer >= maxJumpTime)
        {
            jumpMaxed = true;
            StopJump();
        }
    }

    private void Gravity()
    {
        if(!characterController.isGrounded && !isJumping)
            characterController.Move(Physics.gravity * Time.deltaTime * gravityInfluence);
    }
}
