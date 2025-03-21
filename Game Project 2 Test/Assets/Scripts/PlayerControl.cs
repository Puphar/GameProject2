using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public float speed;
    public float jumpForce;
    public float rotationSpeed;
    public bool isGround = true; // Flag to check if the player is grounded

    private float _gravity = -9.8f; // Default gravity
    public float gravityMultiplier = 2.0f; // Custom gravity multiplier for fall speed

    private bool jumpInput = false;
    private float timeSinceLastAttack = 0f; // To track time since last attack
    public float attackCooldown = 1f; // Attack cooldown in seconds

    [SerializeField] private AudioClip[] jumpClip;
    public GameObject slaah;

    private void FixedUpdate()
    {
        ApplyGravity();
        HandleJump();
    }

    private void Update()
    {
        MovePlayer();
        PlayerAttack();

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            jumpInput = true;
        }
    }

    private void MovePlayer()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove).normalized;
        float magnitude = Mathf.Clamp01(moveDirection.magnitude);

        animator.SetBool("isRun", magnitude > 0);

        // Move the player
        Vector3 newPosition = rb.position + moveDirection * magnitude * speed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Smoothly rotate player towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        if (jumpInput && isGround)
        {
            SoundManager.instance.PlayRandomSFXClip(jumpClip, transform, 1f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false; // Set isGround to false when jump is pressed
            jumpInput = false;
            animator.SetBool("isJump", true);
            animator.SetBool("isFall", false);
        }
    }

    private void ApplyGravity()
    {
        // Apply custom gravity only when the player is in the air (not grounded)
        if (!isGround)
        {
            Vector3 velocity = rb.velocity;

            // Apply gravity to the Y-axis, but don't touch the X and Z axes
            if (velocity.y > 0)
            {
                // Player is going upwards (jumping)
                velocity.y += _gravity * gravityMultiplier * Time.deltaTime;
            }
            else if (velocity.y < 0)
            {
                // Player is falling downwards
                velocity.y += _gravity * gravityMultiplier * Time.deltaTime;
            }

            rb.velocity = velocity;

            // Update animation states based on velocity
            if (velocity.y > 0)
            {
                animator.SetBool("isJump", true);
                animator.SetBool("isFall", false);
            }
            else if (velocity.y < 0)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isFall", true);
            }
        }
    }

    private void PlayerAttack()
    {
        if (timeSinceLastAttack <= 0f && Input.GetMouseButtonDown(0)) // Attack if cooldown has passed
        {
            animator.SetTrigger("Player_ATK");
            timeSinceLastAttack = attackCooldown; // Reset attack timer after attack
        }

        // Update cooldown timer
        if (timeSinceLastAttack > 0f)
        {
            timeSinceLastAttack -= Time.deltaTime;
        }
    }

    // This method detects when the player collides with something, such as the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;  // Set isGround to true when colliding with the ground
            animator.SetBool("isFall", false);  // Stop fall animation when grounded
        }
    }

    // This method detects when the player exits a collision, such as leaving the ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;  // Set isGround to false when no longer colliding with the ground
            animator.SetBool("isJump", false);  // Stop jump animation when in the air
            animator.SetBool("isFall", true);  // Start fall animation when leaving the ground
        }
    }

    public void JumpForce(float force)
    {
        if (isGround)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            isGround = false;
            animator.SetBool("isJump", true);
        }
    }
}
