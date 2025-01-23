using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Trigger on enemy detected.");

        //Add Collision Logic Here

        if (collider.CompareTag("Player"))
        {
            if(collider.GetComponent<DriveSkateboard>() != null && collider.GetComponent<DriveSkateboard>().playerRB.velocity.magnitude > 10)
            {
                Destroy(gameObject);
            }
            else
            {

                collider.GetComponent<SkaturtleLogic>().Respawn();
            }
        }
        //else if (collider.CompareTag("Shell"))
        //{
        //    Destroy(gameObject);
        //}
    }
}
