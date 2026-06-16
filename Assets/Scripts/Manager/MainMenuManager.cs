using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject leaderboardPanel;
    public GameObject difficultyPanel;

    [Header("Difficulty")]
    public Button easyButton;
    public Button hardButton;

    public static string selectedDifficulty = "Normal";

    void Start()
    {
        mainPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        if (difficultyPanel) difficultyPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void OpenLeaderboard()
    {
        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenDifficulty()
    {
        if (difficultyPanel) difficultyPanel.SetActive(true);
    }

    public void SetEasy()
    {
        selectedDifficulty = "Easy";
        if (difficultyPanel) difficultyPanel.SetActive(false);
    }

    public void SetHard()
    {
        selectedDifficulty = "Hard";
        if (difficultyPanel) difficultyPanel.SetActive(false);
    }
}
