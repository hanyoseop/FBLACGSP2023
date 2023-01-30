using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardScript : MonoBehaviour
{
    public TMP_Text Username_1, Score_1, Username_2, Score_2, Username_3, Score_3; 

    void Awake() {
        // Inital update
        updateScoreboard();
    }

    // Update scoreboard function
    void updateScoreboard() {
        Username_1.text = PlayerPrefs.GetString("firstplaceU", "-------");
        Username_2.text = PlayerPrefs.GetString("secondplaceU", "-------");
        Username_3.text = PlayerPrefs.GetString("thirdlaceU", "-------");
        Score_1.text = PlayerPrefs.GetFloat("firstplace", 0).ToString("0");
        Score_2.text = PlayerPrefs.GetFloat("secondplace", 0).ToString("0");
        Score_3.text = PlayerPrefs.GetFloat("thirdplace", 0).ToString("0");
    }

    // Update after reset
    public void resetScoreboard() {
        PlayerPrefs.SetString("firstplaceU", "------");
        PlayerPrefs.SetString("secondplaceU", "------");
        PlayerPrefs.SetString("thirdplaceU", "------");
        PlayerPrefs.SetFloat("firstplace", 0);
        PlayerPrefs.SetFloat("secondplace", 0);
        PlayerPrefs.SetFloat("thirdplace", 0);
        updateScoreboard();
    }
}
