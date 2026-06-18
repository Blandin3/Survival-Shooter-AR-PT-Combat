using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] enemyPrefab;
    public Transform[] spawnPoints;

    public GameObject FactoryMethod(int tag)
    {
        int index = Mathf.Clamp(tag, 0, Mathf.Min(enemyPrefab.Length, spawnPoints.Length) - 1);
        GameObject enemy = Instantiate(enemyPrefab[index], spawnPoints[index].position, spawnPoints[index].rotation);
        return enemy;
    }
}
