using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class DriveSkateboard : MonoBehaviour
{
    [SerializeField] private Rigidbody2D leftTireRB;
    [SerializeField] private Rigidbody2D rightTireRB;
    [SerializeField] public Rigidbody2D playerRB;
    [SerializeField] private Collider2D skateboardCollider;
    [SerializeField] private Collider2D grindCollider;


    [SerializeField] private float speed = 150f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpStrength = 1.5f;
    [SerializeField] private Animator animator;

    private float moveInput;
    private int gravityMult = 1;

    //booleans
    public bool canJump = true;
    public bool isGrounded = false;
    public bool isGrinding = false;

    [SerializeField] private float grindVelocity;
    [SerializeField] float grindRotationSpeed = 10;
    [SerializeField] Stack<Transform> grindWaypoints;
    [SerializeField] ParticleSystem grindSparks;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Texture2D normalColor;
    [SerializeField] private Texture2D jumplessColor;

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

        if (!isGrounded)
        {
            playerRB.AddTorque(moveInput * -rotationSpeed * Time.fixedDeltaTime);
        }

        if (isGrinding)
        {
            if (grindWaypoints.Count > 0)
            {
                //Debug.Log("Waypoints count: " + grindWaypoints.Count);
                //Debug.Log("Next Waypoint: " + grindWaypoints.Peek().ToString());

                Transform nextWaypoint = grindWaypoints.Peek();
                Vector2 nextWaypointPos = nextWaypoint.position;
                Vector2 direction = (nextWaypointPos - (Vector2)grindCollider.transform.position).normalized;

                playerRB.velocity = direction * grindVelocity;

                float angle = nextWaypoint.eulerAngles.z;

                float angleDifference = Mathf.Abs(playerRB.rotation - angle);

                //Debug.Log("Dot product of " + transform.up + " and " + direction + " = " + Vector2.Dot(transform.up, direction));
                Debug.Log("Angle difference: " + playerRB.rotation + " - " + angle + " = " + angleDifference);

                if (angleDifference > 90 && angleDifference < 270)
                {
                    Debug.Log("Player should be upsided down");
                    angle += 180;
                }

                if(angleDifference > 270)
                {
                    angle += 360;
                }

                Debug.Log("Target angle = " + angle);

                playerRB.MoveRotation(Mathf.Lerp(playerRB.rotation, angle, grindRotationSpeed * Time.deltaTime));

                float distanceToNextWaypoint = Vector2.Distance((Vector2)grindCollider.transform.position, nextWaypointPos);

                //Debug.Log("Distance to next waypoint: " + distanceToNextWaypoint);
                if (distanceToNextWaypoint < 1.5f)
                {
                    //Debug.Log("Popping waypoints stack");
                    grindWaypoints.Pop();
                }
            }
            else
            {
                //Stop Grinding
                StopGrind(true);
            }
        }
        sprite.material.SetTexture("_PaletteTex", canJump?normalColor:jumplessColor);

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
            if (isGrinding)
            {
                StopGrind();
            }

            animator.SetTrigger("Jump");
            float relativeHorizontalMovement = Vector3.Dot(playerRB.velocity, transform.right);
            float relativeForwardMovement = Vector3.Dot(playerRB.velocity, transform.up);

            Debug.Log(relativeHorizontalMovement + " " + relativeForwardMovement);

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

    public void ApplyLaunchPadForce(float force, Vector3 direction)
    {
        playerRB.AddForce(force * direction);
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

    public void ResetVelocity()
    {
        leftTireRB.velocity = Vector2.zero;
        rightTireRB.velocity = Vector2.zero;
        playerRB.velocity = Vector2.zero;

        leftTireRB.angularVelocity = 0;
        rightTireRB.angularVelocity = 0;
        playerRB.angularVelocity = 0;

        gravityMult = 1;
        leftTireRB.gravityScale = 1;
        rightTireRB.gravityScale = 1;
        playerRB.gravityScale = 1;
    }

    public void StartGrind(List<Transform> waypoints)
    {
        Debug.Log("Starting Grind");

        grindVelocity = playerRB.velocity.magnitude;
        if (grindVelocity < 10)
        {
            grindVelocity = 10;
        }
        //playerRB.SetRotation(playerRB.rotation % 360);
        NormalizeRigidbodyRotation();

        isGrinding = true;
        canJump = true;

        grindWaypoints = new Stack<Transform>(waypoints);

        grindSparks.Play();
    }

    public void StopGrind(bool applyExitLaunchForce = false)
    {
        Debug.Log("Stopping Grind");

        isGrinding = false;

        grindVelocity = 0;

        if (applyExitLaunchForce)
        {
            ApplyLaunchPadForce(100, playerRB.velocity.normalized);
        }

        if(grindWaypoints != null)
        {
            grindWaypoints.Clear();
        }
        

        grindSparks.Stop();

    }

    private void NormalizeRigidbodyRotation()
    {
        float angle = playerRB.rotation % 360;
        if (angle < 0)
        {
            angle += 360;
        }

        Debug.Log("Normalizing rotation from " + playerRB.rotation + " to " + angle);

        playerRB.SetRotation(angle);
    }
}
