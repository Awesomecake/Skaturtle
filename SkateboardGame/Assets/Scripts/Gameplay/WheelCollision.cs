using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCollision : MonoBehaviour
{
    [SerializeField] private DriveSkateboard skateboard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If trigger touches the GROUND
        if (collision != null && collision.gameObject.layer == 3)
        {
            skateboard.canJump = true;
        }
    }
}
