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

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rightTireRB.AddTorque(-moveInput * speed * Time.fixedDeltaTime);
        leftTireRB.AddTorque(-moveInput * speed * Time.fixedDeltaTime);

        skateboardRB.AddTorque(moveInput * rotationSpeed * Time.fixedDeltaTime);
    }
}
