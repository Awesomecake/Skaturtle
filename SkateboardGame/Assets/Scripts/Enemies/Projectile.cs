using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Enemy
{
    [SerializeField] float projectileSpeed = 2;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float projectileLifetime = 10;

    private void OnEnable()
    {
        SkaturtleLogic.Instance.OnRespawn.AddListener(DestroySelf);
    }
    private void Awake()
    {
        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        Invoke(nameof(DestroySelf), projectileLifetime);
    }

    public void FireProjectile(Vector2 direction)
    {
        _rigidbody.velocity = direction * projectileSpeed;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
