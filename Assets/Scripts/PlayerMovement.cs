using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    [Header("Movement")]
    public float moveSpeed = 5f;
    [Header("Jumping")]
    public float jumpPower = 10f;
    [Header("GroundCheck")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    private float horizontalMovement;
    [Header("AttackPointLocation")]
    public Transform attackPoint;
    private Vector2 attackPointStartPosition;
    void Start()
    {
        attackPointStartPosition = attackPoint.localPosition;
    }
    void Update()
    {
        if (GetComponent<PlayerKnockback>().isKnockedBack) return;
        if (horizontalMovement > 0)
        {
            attackPoint.localPosition = new Vector2(Mathf.Abs(attackPointStartPosition.x),attackPointStartPosition.y);
        }
        else if (horizontalMovement < 0)
        {
            attackPoint.localPosition = new Vector2(-Mathf.Abs(attackPointStartPosition.x),attackPointStartPosition.y);
        }
        body.linearVelocity = new Vector2(horizontalMovement * moveSpeed, body.linearVelocity.y);
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            if (context.performed)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }
}
