using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Text warningText;

    [Header("End Game UI")]
    public GameObject endGamePanel;
    public Text finalScoreText;
    public Text enemiesDefeatedText;
    public Text timeSurvivedText;

    Animator anim;
    bool gameOverTriggered = false;
    ScoreManager scoreManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        if (endGamePanel) endGamePanel.SetActive(false);
    }

    void Update()
    {
        if (!gameOverTriggered && playerHealth.currentHealth <= 0)
        {
            gameOverTriggered = true;
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        anim.SetTrigger("GameOver");

        if (scoreManager) scoreManager.StopTimer();

        // Wipe all enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        // Save to leaderboard
        LeaderboardManager.SaveSession(ScoreManager.score, ScoreManager.enemiesDefeated, ScoreManager.timeSurvived);
        PlayerPrefs.SetInt(MainMenuManager.SAVE_EXISTS_KEY, 1);
        PlayerPrefs.Save();

        // Show end game UI
        if (endGamePanel)
        {
            endGamePanel.SetActive(true);
            if (finalScoreText) finalScoreText.text = "Score: " + ScoreManager.score;
            if (enemiesDefeatedText) enemiesDefeatedText.text = "Enemies Defeated: " + ScoreManager.enemiesDefeated;
            if (timeSurvivedText) timeSurvivedText.text = "Time Survived: " + Mathf.FloorToInt(ScoreManager.timeSurvived) + "s";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowWarning(float enemyDistance)
    {
        if (warningText) warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }
}
