using UnityEngine;

public class EnemyShooterAttack : MonoBehaviour
{
    public float shootInterval = 2f;
    public float shootRange = 10f;
    public Transform firePoint;
    public AudioClip shootClip;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    AudioSource audioSource;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        timer = shootInterval; // fire immediately on first opportunity
    }

    void Update()
    {
        if (enemyHealth.currentHealth <= 0 || playerHealth.currentHealth <= 0) return;

        timer += Time.deltaTime;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= shootRange && timer >= shootInterval)
        {
            timer = 0f;
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (EnemyBulletPool.Instance == null)
        {
            Debug.LogError("EnemyBulletPool not found in scene!");
            return;
        }

        Transform spawnAt = firePoint != null ? firePoint : transform;
        GameObject bullet = EnemyBulletPool.Instance.Get();
        Vector3 spawnPos = spawnAt.position + spawnAt.forward * 1.5f; // offset forward to avoid spawning inside enemy
        Vector3 dir = (new Vector3(player.position.x, spawnAt.position.y, player.position.z) - spawnPos).normalized;
        bullet.transform.position = spawnPos;
        bullet.transform.rotation = Quaternion.LookRotation(dir);

        if (audioSource != null && shootClip != null)
        {
            audioSource.clip = shootClip;
            audioSource.Play();
        }
    }
}
