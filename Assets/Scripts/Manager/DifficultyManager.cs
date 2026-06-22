using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public PlayerHealth playerHealth;

    void Start()
    {
        string difficulty = MainMenuManager.selectedDifficulty;

        if (difficulty == "Easy")
        {
            enemyManager.spawnTime = 5f;
            playerHealth.startingHealth = 150;
            playerHealth.currentHealth = 150;
        }
        else if (difficulty == "Hard")
        {
            enemyManager.spawnTime = 1.5f;
            playerHealth.startingHealth = 75;
            playerHealth.currentHealth = 75;
        }
        else // Normal
        {
            enemyManager.spawnTime = 3f;
            playerHealth.startingHealth = 100;
            playerHealth.currentHealth = 100;
        }

        // Sync health slider
        if (playerHealth.healthSlider)
        {
            playerHealth.healthSlider.maxValue = playerHealth.startingHealth;
            playerHealth.healthSlider.value = playerHealth.currentHealth;
        }
    }
}
