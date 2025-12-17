using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class char_movement : MonoBehaviour
{
    private float movespeed = 5f;
    public float sprintMultiplier = 2f;
    public float walkspeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;


    private bool isDashing = false;
    public float dashTimer = 0f;
    private Vector2 dashDirection;

    public Transform player;
    public bool isCrashed = false;
    public float rayLength=0.5f;
    public LayerMask wallCrashLayer;


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnDash(InputValue value)
    {
        if (value.isPressed)
        {
            Dash();
        }
    }
    
    void endAttack()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("SpecialAttack", false);
    }

    void Dash()
    {
        if (isDashing)
        {
            return;
        }
        if (dashTimer > 0f)
        {
            return;
        }
        if (moveInput.sqrMagnitude > 0.0001f)
        {
            isDashing = true;
            dashTimer = dashCooldown;
            dashDirection = moveInput.normalized;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        movespeed = walkspeed;
    }

    void FixedUpdate()
    {
         RaycastHit2D playerHit = Physics2D.Raycast(player.position, moveInput.normalized, rayLength, wallCrashLayer);

        if (playerHit.collider != null)
        {
            isCrashed = true;
        }
        else    
        {
            isCrashed = false;
        }

        if (isDashing && !isCrashed)
        {
            rb.linearVelocity = dashDirection * dashSpeed;

            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer < 0f)
            {
                isDashing = false;
                dashTimer = 0;
            }
        }
        else
        {

            rb.linearVelocity = moveInput * movespeed;

            bool IsMoving = moveInput.sqrMagnitude > 0.01f;

            animator.SetBool("IsMoving", IsMoving);

            if (IsMoving)
            {
                animator.SetFloat("X", moveInput.x);
                animator.SetFloat("Y", moveInput.y);
            }
        }
    }
}
