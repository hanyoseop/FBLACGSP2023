using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public void AddScore(float score) {
        PlayerPrefs.SetFloat("TotalScore", PlayerPrefs.GetFloat("TotalScore") + score);
    }

    public float GetTotalScore() {
        return PlayerPrefs.GetFloat("TotalScore");
    }

    public void SaveScore(string username) {
        if (PlayerPrefs.GetFloat("TotalScore") >= PlayerPrefs.GetFloat("firstplace")) {
            // Move second to third
            PlayerPrefs.SetFloat("thirdplace", PlayerPrefs.GetFloat("secondplace"));
            PlayerPrefs.SetFloat("thirdplaceU", PlayerPrefs.GetFloat("secondplaceU"));
            // Move first place down to second
            PlayerPrefs.SetFloat("secondplace", PlayerPrefs.GetFloat("firstplace"));
            PlayerPrefs.SetFloat("secondplaceU", PlayerPrefs.GetFloat("firstplaceU"));
            // New first place
            PlayerPrefs.SetFloat("firstplace", PlayerPrefs.GetFloat("TotalScore"));
            PlayerPrefs.SetString("firstplaceU", username);
        } else if (PlayerPrefs.GetFloat("TotalScore") >= PlayerPrefs.GetFloat("secondplace")) {
            // Move second to third
            PlayerPrefs.SetFloat("thirdplace", PlayerPrefs.GetFloat("secondplace"));
            PlayerPrefs.SetFloat("thirdplaceU", PlayerPrefs.GetFloat("secondplaceU"));
            // New second place
            PlayerPrefs.SetFloat("secondplace", PlayerPrefs.GetFloat("TotalScore"));
            PlayerPrefs.SetString("secondplaceU", username);
        } else if (PlayerPrefs.GetFloat("TotalScore") >= PlayerPrefs.GetFloat("thirdplace")) {
            PlayerPrefs.SetFloat("thirdplace", PlayerPrefs.GetFloat("TotalScore"));
            PlayerPrefs.SetString("thirdplaceU", username);
        } 
    }

    
}
