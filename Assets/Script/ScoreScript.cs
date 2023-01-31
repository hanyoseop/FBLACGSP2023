using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public float totalScore;

    public void AddScore(float score) {
        totalScore += score;
    }

    public void SaveScore(string username) {
        if (totalScore >= PlayerPrefs.GetFloat("firstplace")) {
            // Move second to third
            PlayerPrefs.SetFloat("thirdplace", PlayerPrefs.GetFloat("secondplace"));
            PlayerPrefs.SetFloat("thirdplaceU", PlayerPrefs.GetFloat("secondplaceU"));
            // Move first place down to second
            PlayerPrefs.SetFloat("secondplace", PlayerPrefs.GetFloat("firstplace"));
            PlayerPrefs.SetFloat("secondplaceU", PlayerPrefs.GetFloat("firstplaceU"));
            // New first place
            PlayerPrefs.SetFloat("firstplace", totalScore);
            PlayerPrefs.SetString("firstplaceU", username);
        } else if (totalScore >= PlayerPrefs.GetFloat("secondplace")) {
            // Move second to third
            PlayerPrefs.SetFloat("thirdplace", PlayerPrefs.GetFloat("secondplace"));
            PlayerPrefs.SetFloat("thirdplaceU", PlayerPrefs.GetFloat("secondplaceU"));
            // New second place
            PlayerPrefs.SetFloat("secondplace", totalScore);
            PlayerPrefs.SetString("secondplaceU", username);
        } else if (totalScore >= PlayerPrefs.GetFloat("thirdplace")) {
            PlayerPrefs.SetFloat("thirdplace", totalScore);
            PlayerPrefs.SetString("thirdplaceU", username);
        } 
    }
}
