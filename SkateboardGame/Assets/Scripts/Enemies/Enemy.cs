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
            collider.GetComponent<SkaturtleLogic>().Respawn();
        }
        else if (collider.CompareTag("Shell"))
        {
            Destroy(gameObject);
        }
    }
}
