using UnityEngine;

public class EnemyShooterMovement : MonoBehaviour
{
    public float stoppingDistance = 8f;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (enemyHealth.currentHealth <= 0 || playerHealth.currentHealth <= 0)
        {
            nav.enabled = false;
            return;
        }

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > stoppingDistance)
            nav.SetDestination(player.position);
        else
        {
            nav.SetDestination(transform.position); // stop in place
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }
}
