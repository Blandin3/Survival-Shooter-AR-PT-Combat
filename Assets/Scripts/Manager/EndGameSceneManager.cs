using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EndGameSceneManager : MonoBehaviour
{
    const string KEY_COUNT = "LB_Count";
    const string KEY_SCORE = "LB_Score_";
    const string KEY_ENEMIES = "LB_Enemies_";
    const string KEY_TIME = "LB_Time_";

    TMP_Text scoreText;
    TMP_Text enemiesText;
    TMP_Text timeText;

    void Start()
    {
        scoreText = FindText("ScoreText");
        enemiesText = FindText("EnemiesText");
        timeText = FindText("Time Text");

        RefreshResults();
    }

    void RefreshResults()
    {
        int count = PlayerPrefs.GetInt(KEY_COUNT, 0);
        int score = ScoreManager.score;
        int enemies = ScoreManager.enemiesDefeated;
        float time = ScoreManager.timeSurvived;

        if (count > 0)
        {
            int lastIndex = count - 1;
            score = PlayerPrefs.GetInt(KEY_SCORE + lastIndex, score);
            enemies = PlayerPrefs.GetInt(KEY_ENEMIES + lastIndex, enemies);
            time = PlayerPrefs.GetFloat(KEY_TIME + lastIndex, time);
        }

        if (scoreText) scoreText.text = "Score: " + score;
        if (enemiesText) enemiesText.text = "Kill Count: " + enemies;
        if (timeText) timeText.text = "Time: " + Mathf.FloorToInt(time) + "s";
    }

    TMP_Text FindText(string objectName)
    {
        GameObject textObject = GameObject.Find(objectName);
        if (textObject == null)
            return null;

        return textObject.GetComponent<TMP_Text>();
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_01");
    }

    public void BackToMainMenu()
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
}
