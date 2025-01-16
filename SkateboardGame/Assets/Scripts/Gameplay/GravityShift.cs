using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            DriveSkateboard driveSkateboard = collision.GetComponent<DriveSkateboard>();
            if (driveSkateboard != null)
            {
                driveSkateboard.FlipGravity();
            }
        }
    }
}
