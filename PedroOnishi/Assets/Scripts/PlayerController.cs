using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rig;
    public Collider2D col;
    public float speed = 10;
    public float jumpForce = 10;
    public LayerMask floorLayer;
    private Vector2 moveInput;

    public void MoveInput(InputAction.CallbackContext context)
    {
       moveInput = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            ExecuteJump();
        }
    }

    private void ExecuteJump()
    {
        Vector2 pLeft = new Vector2(col.bounds.min.x, col.bounds.max.y);
        Vector2 pRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
        if(Physics.Raycast(pLeft, Vector2.down, col.bounds.size.y * 1.1f, floorLayer) ||
            Physics.Raycast(pRight, Vector2.down, col.bounds.size.y * 1.1f, floorLayer))
        {
            rig.linearVelocity = new Vector2
            (
                rig.linearVelocity.x,
                jumpForce
            );
        }
    }

    private void FixedUpdate()
    {
        rig.linearVelocity = new Vector2
        (
            speed * moveInput.x,
            rig.linearVelocity.y
        );
    }

}
