using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class DriveSkateboard : MonoBehaviour
{
    [SerializeField] private Rigidbody2D leftTireRB;
    [SerializeField] private Rigidbody2D rightTireRB;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Collider2D skateboardCollider;


    [SerializeField] private float speed = 150f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpStrength = 1.5f;

    private float moveInput;
    private int gravityMult = 1;

    //booleans
    public bool canJump = true;
    public bool isGrounded = false;

    private void Update()
    {
        CheckIsGrounded();  
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        Debug.Log(c.gameObject);
    }

    private void FixedUpdate()
    {
        rightTireRB.AddTorque(-moveInput * gravityMult * speed * Time.fixedDeltaTime);
        leftTireRB.AddTorque(-moveInput * gravityMult * speed * Time.fixedDeltaTime);

        if(!isGrounded)
        {
            playerRB.AddTorque(moveInput * -rotationSpeed * Time.fixedDeltaTime);
        }
        
    }

    public void FlipGravity()
    {
        gravityMult *= -1;
        leftTireRB.gravityScale *= -1;
        rightTireRB.gravityScale *= -1;
        playerRB.gravityScale *= -1;
    }

    public void Jump()
    {
        if (canJump)
        {
            Vector3 velocity = playerRB.velocity;
            velocity.y = 0;
            playerRB.velocity = velocity;

            velocity = rightTireRB.velocity;
            velocity.y = 0;
            rightTireRB.velocity = velocity;

            velocity = leftTireRB.velocity;
            velocity.y = 0;
            leftTireRB.velocity = velocity;

            playerRB.AddRelativeForce(new Vector2(0, 1000 * jumpStrength));
            canJump = false;
        }
    }

    public void UpdateMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        moveInput = movementVector.x;
    }

    public void UpdateJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }

    public void CheckIsGrounded()
    {
        isGrounded = leftTireRB.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                     rightTireRB.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                     //playerRB.IsTouchingLayers(LayerMask.GetMask("Ground"))||
                     skateboardCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
