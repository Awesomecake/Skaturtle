using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 2;
    [SerializeField] Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    public void FireProjectile(Vector2 direction)
    {
        _rigidbody.velocity = direction * projectileSpeed;
    }
}
