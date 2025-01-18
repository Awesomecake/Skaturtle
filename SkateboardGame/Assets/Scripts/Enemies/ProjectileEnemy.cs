using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    [SerializeField] Transform target;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRateInSeconds;

    private void Start()
    {
        StartCoroutine(FireProjectileCoroutine());
    }

    IEnumerator FireProjectileCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRateInSeconds);

            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        Vector2 projectileDirection = target.position - transform.position;
        Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.FireProjectile(projectileDirection);
    }
}
