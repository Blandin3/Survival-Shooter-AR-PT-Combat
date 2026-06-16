using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 20;

    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject b = Instantiate(bulletPrefab, transform);
            b.SetActive(false);
            pool.Enqueue(b);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            GameObject b = pool.Dequeue();
            b.SetActive(true);
            return b;
        }
        // Pool exhausted — expand
        GameObject extra = Instantiate(bulletPrefab, transform);
        return extra;
    }

    public void Return(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}
