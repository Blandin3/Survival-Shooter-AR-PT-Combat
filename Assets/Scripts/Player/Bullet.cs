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
        {
            if (EnemyBulletPool.Instance != null)
                EnemyBulletPool.Instance.Return(gameObject);
            else
                gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
            player.TakeDamage(damage);

        if (EnemyBulletPool.Instance != null)
            EnemyBulletPool.Instance.Return(gameObject);
        else
            gameObject.SetActive(false);
    }
}
