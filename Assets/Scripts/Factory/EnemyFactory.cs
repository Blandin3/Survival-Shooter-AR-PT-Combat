using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] enemyPrefab;
    public Transform[] spawnPoints;
    [SerializeField]
    float navMeshSampleDistance = 5f;

    public GameObject FactoryMethod(int tag)
    {
        int index = Mathf.Clamp(tag, 0, Mathf.Min(enemyPrefab.Length, spawnPoints.Length) - 1);
        Vector3 spawnPosition = spawnPoints[index].position;
        if (NavMesh.SamplePosition(spawnPosition, out NavMeshHit hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            spawnPosition = hit.position;
        }
        else
        {
            Debug.LogWarning($"Enemy spawn point {spawnPoints[index].name} is not close enough to the NavMesh. Spawning at the original position may fail for NavMeshAgent.");
        }

        GameObject enemy = Instantiate(enemyPrefab[index], spawnPosition, spawnPoints[index].rotation);
        return enemy;
    }
}
