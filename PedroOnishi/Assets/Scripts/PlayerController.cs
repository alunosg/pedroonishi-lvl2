using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rig;
    public Collider2D col;
    public float speed = 10;
    public float jumpForce = 10;
    public LayerMask floorLayer;
    public Animator anim;
    private Vector2 moveInput;
    private bool grounded = false;
    private bool locked = false;
    

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        anim.SetBool("IsWalking",moveInput.x !=0);
        transform.localScale = new(
            moveInput.x > 0 ? 1 : moveInput.x < 0 ? -1 : transform.localScale.x, 1);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.started && grounded)
        {
            rig.linearVelocity = new Vector2
            (
                rig.linearVelocity.x,
                jumpForce
            );
        }
    }

    private void DetectGround()
    {
        Vector2 pLeft = new Vector2(col.bounds.min.x, col.bounds.max.y);
        Vector2 pRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
        if(Physics2D.Raycast(pLeft, Vector2.down, col.bounds.size.y * 1.1f, floorLayer) ||
            Physics2D.Raycast(pRight, Vector2.down, col.bounds.size.y * 1.1f, floorLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        anim.SetBool("IsGrounded", grounded);
    }

    private void FixedUpdate()
    {
        DetectGround();

        rig.linearVelocity = new Vector2
        (
            speed * moveInput.x,
            rig.linearVelocity.y
        );
    }

}
