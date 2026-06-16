using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public int damage = 20;

    float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifetime)
            BulletPool.Instance.Return(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
            enemy.TakeDamage(damage, transform.position);

        BulletPool.Instance.Return(gameObject);
    }
}
