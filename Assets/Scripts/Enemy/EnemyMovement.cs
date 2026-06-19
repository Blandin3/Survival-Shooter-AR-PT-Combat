using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        //Cari game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Mendapatkan componen reference
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (nav.enabled && nav.isOnNavMesh)
                nav.SetDestination(player.position);
        }
        else
        {
            if (nav.enabled) nav.enabled = false;
        }
    }
}