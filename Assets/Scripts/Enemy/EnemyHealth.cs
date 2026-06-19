using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public AudioClip spawnClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        //Menapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Set current health
        currentHealth = startingHealth;

        // Play spawn sound
        if (spawnClip != null)
        {
            enemyAudio.clip = spawnClip;
            enemyAudio.Play();
        }
    }


    void Update()
    {
        //Check jika sinking
        if (isSinking)
        {
            //memindahkan object kebawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if (isDead)
            return;

        //play audio
        enemyAudio.Play();

        //kurangi health
        currentHealth -= amount;

        //Ganti posisi particle
        if (hitParticles != null)
        {
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        //Dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        // Add score immediately on death — does not rely on animation event
        ScoreManager.score += scoreValue;
        ScoreManager.AddKill();

        if (capsuleCollider) capsuleCollider.isTrigger = true;

        if (anim != null) anim.SetTrigger("Dead");

        if (deathClip != null)
        {
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        // Start sinking immediately if no animator to call StartSinking
        if (anim == null)
            StartSinking();
    }

    public void StartSinking()
    {
        var nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav) nav.enabled = false;
        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}