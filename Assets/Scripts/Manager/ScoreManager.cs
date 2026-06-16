using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int enemiesDefeated;
    public static float timeSurvived;

    public Text scoreText;
    public Text timerText;

    bool gameOver = false;

    void Awake()
    {
        score = 0;
        enemiesDefeated = 0;
        timeSurvived = 0f;
    }

    void Update()
    {
        if (!gameOver)
            timeSurvived += Time.deltaTime;

        if (scoreText) scoreText.text = "Score: " + score;
        if (timerText) timerText.text = "Time: " + Mathf.FloorToInt(timeSurvived) + "s";
    }

    public void StopTimer()
    {
        gameOver = true;
    }

    public static void AddKill()
    {
        enemiesDefeated++;
    }
}
