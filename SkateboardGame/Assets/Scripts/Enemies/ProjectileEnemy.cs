using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    [SerializeField] Transform target;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRateInSeconds;
    [SerializeField] float fireRange;

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
        Vector2 projectileDistance = target.position - transform.position;

        if (projectileDistance.magnitude < fireRange)
        {
            Vector2 projectileDirection = projectileDistance.normalized;
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.FireProjectile(projectileDirection);
        }
    }
}
