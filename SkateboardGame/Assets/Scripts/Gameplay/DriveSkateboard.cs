using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSkateboard : MonoBehaviour
{
    [SerializeField] private Rigidbody2D leftTireRB;
    [SerializeField] private Rigidbody2D rightTireRB;
    [SerializeField] private Rigidbody2D skateboardRB;

    [SerializeField] private float speed = 150f;
    [SerializeField] private float rotationSpeed;

    private float moveInput;
    private int gravityMult = 1;

    //booleans
    private bool canJump = true;

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }    
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        Debug.Log(c.gameObject);
    }

    private void FixedUpdate()
    {
        rightTireRB.AddTorque(-moveInput * gravityMult * speed * Time.fixedDeltaTime);
        leftTireRB.AddTorque(-moveInput * gravityMult * speed * Time.fixedDeltaTime);

        skateboardRB.AddTorque(moveInput * rotationSpeed * Time.fixedDeltaTime);
    }

    public void FlipGravity()
    {
        gravityMult *= -1;
        leftTireRB.gravityScale *= -1;
        rightTireRB.gravityScale *= -1;
        skateboardRB.gravityScale *= -1;
    }

    public void Jump()
    {
        skateboardRB.velocity = Vector3.zero;
        rightTireRB.velocity = Vector3.zero;
        leftTireRB.velocity = Vector3.zero;
        skateboardRB.AddRelativeForce(new Vector2(0, 1000));
    }
}
