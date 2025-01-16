using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCollision : MonoBehaviour
{
    [SerializeField] private DriveSkateboard skateboard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            skateboard.canJump = true;
        }
    }
}
