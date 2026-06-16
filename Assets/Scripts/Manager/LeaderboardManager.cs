using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    const int MAX_ENTRIES = 5;
    const string KEY_COUNT = "LB_Count";
    const string KEY_SCORE = "LB_Score_";
    const string KEY_ENEMIES = "LB_Enemies_";
    const string KEY_TIME = "LB_Time_";

    public Transform entryContainer;
    public GameObject entryPrefab;

    public static void SaveSession(int score, int enemies, float time)
    {
        int count = PlayerPrefs.GetInt(KEY_COUNT, 0);

        // Shift entries if at max
        if (count >= MAX_ENTRIES)
        {
            for (int i = 0; i < MAX_ENTRIES - 1; i++)
            {
                PlayerPrefs.SetInt(KEY_SCORE + i, PlayerPrefs.GetInt(KEY_SCORE + (i + 1), 0));
                PlayerPrefs.SetInt(KEY_ENEMIES + i, PlayerPrefs.GetInt(KEY_ENEMIES + (i + 1), 0));
                PlayerPrefs.SetFloat(KEY_TIME + i, PlayerPrefs.GetFloat(KEY_TIME + (i + 1), 0f));
            }
            count = MAX_ENTRIES - 1;
        }

        PlayerPrefs.SetInt(KEY_SCORE + count, score);
        PlayerPrefs.SetInt(KEY_ENEMIES + count, enemies);
        PlayerPrefs.SetFloat(KEY_TIME + count, time);
        PlayerPrefs.SetInt(KEY_COUNT, count + 1);
        PlayerPrefs.Save();
    }

    void OnEnable()
    {
        PopulateEntries();
    }

    void PopulateEntries()
    {
        // Clear old entries
        foreach (Transform child in entryContainer)
            Destroy(child.gameObject);

        int count = PlayerPrefs.GetInt(KEY_COUNT, 0);
        for (int i = count - 1; i >= 0; i--)
        {
            int sc = PlayerPrefs.GetInt(KEY_SCORE + i, 0);
            int en = PlayerPrefs.GetInt(KEY_ENEMIES + i, 0);
            float ti = PlayerPrefs.GetFloat(KEY_TIME + i, 0f);

            GameObject entry = Instantiate(entryPrefab, entryContainer);
            Text[] texts = entry.GetComponentsInChildren<Text>();
            // expects 3 Text children: Score, Enemies, Time
            if (texts.Length >= 3)
            {
                texts[0].text = "Score: " + sc;
                texts[1].text = "Enemies: " + en;
                texts[2].text = "Time: " + Mathf.FloorToInt(ti) + "s";
            }
        }
    }
}
