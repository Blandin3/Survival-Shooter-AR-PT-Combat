using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Text warningText;

    [Header("End Game UI")]
    public GameObject endGamePanel;
    public Text finalScoreText;
    public TMP_Text finalScoreTMPText;
    public Text enemiesDefeatedText;
    public TMP_Text enemiesDefeatedTMPText;
    public Text timeSurvivedText;
    public TMP_Text timeSurvivedTMPText;

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

        Time.timeScale = 1f;
        SceneManager.LoadScene("EndGameScene");
    }

    // Public notifier so other scripts (PlayerHealth) can trigger game over immediately
    public void OnPlayerDeath()
    {
        if (!gameOverTriggered)
        {
            gameOverTriggered = true;
            TriggerGameOver();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReplayGame()
    {
        RestartGame();
    }

    public void BackToMainMenu()
    {
        ReturnToMainMenuScene();
    }

    public void ReturnToMainMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuGScene");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowWarning(float enemyDistance)
    {
        if (warningText) warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }

    void SetLabel(Text legacyText, TMP_Text tmpText, string value)
    {
        if (legacyText) legacyText.text = value;
        if (tmpText) tmpText.text = value;
    }
}
