using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject leaderboardPanel;
    public GameObject difficultyPanel;

    [Header("Buttons")]
    public Button continueButton;

    [Header("Difficulty")]
    public Button easyButton;
    public Button hardButton;

    public static string selectedDifficulty = "Normal";
    public const string SAVE_EXISTS_KEY = "SaveExists";

    void Start()
    {
        mainPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        if (difficultyPanel) difficultyPanel.SetActive(false);

        // Grey out Continue if no saved session exists
        if (continueButton)
            continueButton.interactable = PlayerPrefs.GetInt(SAVE_EXISTS_KEY, 0) == 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt(SAVE_EXISTS_KEY, 0) == 1)
            SceneManager.LoadScene("Level_01");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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
