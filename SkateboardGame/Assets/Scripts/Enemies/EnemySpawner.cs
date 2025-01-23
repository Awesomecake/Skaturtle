using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] int enemyTypeToSpawnIndex = 0;
    [SerializeField] Enemy spawnedEnemy;

    private void OnEnable()
    {
        SkaturtleLogic.Instance.OnRespawn.AddListener(SpawnEnemy);
    }

    //private void OnDisable()
    //{
    //    SkaturtleLogic.Instance.OnRespawn.RemoveListener(SpawnEnemy);
    //}

    private void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (spawnedEnemy)
        {
            Destroy(spawnedEnemy.gameObject);
        }

        if (enemyTypeToSpawnIndex < enemyPrefabs.Count)
        {
            spawnedEnemy = Instantiate(enemyPrefabs[enemyTypeToSpawnIndex], transform.position, Quaternion.identity, transform).GetComponent<Enemy>();
        }
        else
        {
            Debug.LogError("Could not spawn enemy. Index out of range.");
        }
    }
}
