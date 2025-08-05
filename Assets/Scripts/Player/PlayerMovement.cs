using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // References
    private CharacterController characterController;

    // Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;
    [Space(10)]
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpForce;
    [Space(10)]
    [Range(0, 1)]
    [SerializeField] private float crouchHight;
    [Space(10)]
    [SerializeField] private float gravityInfluence = 100;

    private float jumpCounter;
    private float jumpTimer;

    private float standingHight;

    private bool isJumping;
    private bool isCrouching;
    private bool jumpMaxed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        standingHight = characterController.height;
    }

    private void Update()
    {
        Gravity();
        if (isJumping) Jumping();
    }

    public void Move(Vector2 moveInput, bool isSprinting)
    {
        float speed;
        if (isCrouching) speed = crouchSpeed;
        else speed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 velocity = direction * speed * Time.deltaTime * 10;
        characterController.Move(velocity);
    }

    #region Jumping Functions
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
    #endregion

    #region Crouch Functions
    public void StartCrouch()
    {
        isCrouching = true;
        characterController.height = standingHight * crouchHight;
    }

    public void StopCrouch()
    {
        isCrouching = false;
        characterController.height = standingHight;
    }
    #endregion

    private void Gravity()
    {
        characterController.Move(Physics.gravity * Time.deltaTime * gravityInfluence);
    }
}
