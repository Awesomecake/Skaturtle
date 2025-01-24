using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] Transform target;
    [SerializeField] float movementSpeed;
    [SerializeField] float aggroRange;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Rigidbody2D enemyRB;

    float initialScaleX;

    private void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        initialPosition = transform.position;
        initialScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget < aggroRange)
            {
                MoveEnemyToPoint(target.position);
            }
            else
            {
                float distanceToInitialPosition = Vector2.Distance(transform.position, initialPosition);
                if (distanceToInitialPosition > 1)
                {
                    MoveEnemyToPoint(initialPosition);
                }
            }
        }
    }

    void MoveEnemyToPoint(Vector3 destination)
    {
        enemyRB.velocity = movementSpeed * (destination - transform.position).normalized * Time.fixedDeltaTime;
        
        if(enemyRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
