using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger on enemy detected.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision on enemy detected.");
    }
}
