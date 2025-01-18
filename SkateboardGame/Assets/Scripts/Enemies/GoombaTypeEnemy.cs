using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaTypeEnemy : Enemy
{
    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] float moveSpeed = 2f; 
    [SerializeField] float edgeDetectionDistance = 0.5f; // Distance to check for ground ahead
    [SerializeField] float rayOriginHorizontalDistance = 1; //Distance ahead of the enemy to create raycast
    [SerializeField] LayerMask groundLayer; // Layer mask for the ground

    private bool movingRight = true;

    private void Start()
    {
        if(!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        // Move the enemy
        _rigidbody.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, _rigidbody.velocity.y);

        // Check for the edge
        Vector2 rayOrigin = movingRight
            ? new Vector2(transform.position.x + rayOriginHorizontalDistance, transform.position.y)
            : new Vector2(transform.position.x - rayOriginHorizontalDistance, transform.position.y);

        RaycastHit2D groundInfo = Physics2D.Raycast(rayOrigin, Vector2.down, edgeDetectionDistance, groundLayer);

        if (!groundInfo.collider)
        {
            // Turn around
            movingRight = !movingRight;
            transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1); // Flip sprite
        }
    }

    void OnDrawGizmos()
    {
        // Visualize raycast in the editor
        Gizmos.color = Color.red;
        Vector2 rayOrigin = movingRight
            ? new Vector2(transform.position.x + rayOriginHorizontalDistance, transform.position.y)
            : new Vector2(transform.position.x - rayOriginHorizontalDistance, transform.position.y);
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector2.down * edgeDetectionDistance);
    }
}
