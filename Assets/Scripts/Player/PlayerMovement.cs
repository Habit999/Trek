using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // References
    private CharacterController characterController;

    // Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideSlowSpeed;
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

    private float moveSpeed;

    private bool isJumping;
    private bool isSprinting;
    private bool isCrouching;
    private bool isSliding;
    private bool isGrounded;
    private bool jumpMaxed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        standingHight = characterController.height;
    }

    private void Update()
    {
        Gravity();
        CheckGravity();
        if (isJumping) Jumping();
    }

    public void Move(Vector2 moveInput)
    {
        if (!isSliding)
        {
            if (isCrouching && isGrounded) moveSpeed = crouchSpeed;
            else moveSpeed = isSprinting ? sprintSpeed : walkSpeed;
        }
        if(isSliding) Sliding();

        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 velocity = direction * moveSpeed * Time.deltaTime * 10;
        characterController.Move(velocity);
    }

    #region Sprint Functions
    public void StartSprint()
    {
        isSprinting = true;
    }

    public void StopSprint()
    {
        isSprinting = false;
    }
    #endregion

    #region Jumping Functions
    public void StartJump()
    {
        if(isCrouching) StopCrouch();

        if (isGrounded && !isJumping)
        {
            isJumping = true;
            jumpMaxed = false;
            jumpTimer = 0;
            jumpCounter = 0;
        }
        else if(!isGrounded && !isJumping && jumpCounter < 2)
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

        if (isSprinting && !isSliding) StartSliding();
    }

    public void StopCrouch()
    {
        isCrouching = false;
        characterController.height = standingHight;

        if(isSliding)
        {
            StopSliding();
        }
    }
    #endregion

    #region Sliding Functions
    private void StartSliding()
    {
        isSliding = true;
        moveSpeed = slideSpeed;
    }

    private void StopSliding()
    {
        isSliding = false;
    }

    private void Sliding()
    {
        if (moveSpeed > crouchSpeed)
        {
            moveSpeed -= slideSlowSpeed * Time.deltaTime;
        }
        else StopSliding();
    }
    #endregion

    private void CheckGravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, characterController.height / 2 + 0.1f);
    }

    private void Gravity()
    {
        characterController.Move(Physics.gravity * Time.deltaTime * gravityInfluence);
    }
}
