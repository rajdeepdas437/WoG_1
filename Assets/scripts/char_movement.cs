using UnityEngine;
using UnityEngine.InputSystem;

public class char_movement : MonoBehaviour
{
    private float movespeed = 5f;
    public float sprintMultiplier = 2f;
    public float walkspeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    private Animator animator;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        bool Sprint = value.isPressed;
        movespeed = Sprint?(walkspeed*sprintMultiplier):walkspeed;
    }

    void Start()
    {
        animator=GetComponent<Animator>();
        movespeed=walkspeed;
    }

    void Update()
    {
        
       Debug.Log(moveInput);
       
       transform.position += (Vector3)moveInput * movespeed * Time.deltaTime; 
        
        bool IsMoving = moveInput.sqrMagnitude>0.01f;

        animator.SetBool("IsMoving", IsMoving);

        if(IsMoving)
        {
            animator.SetFloat("X", moveInput.x);
            animator.SetFloat("Y", moveInput.y);
        }
    }
}
