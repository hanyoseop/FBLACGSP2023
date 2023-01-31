using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectButtonScript : MonoBehaviour
{
    [SerializeField] private Sprite[] maps;
    [SerializeField] private string[] titles;
    [SerializeField] private string[] descriptions;
    
    [SerializeField] private GameObject playTab;
    private TMP_Text titleText;
    private TMP_Text descriptionText;
    private Image imageComponent;

    void Awake() {
        titleText = playTab.transform.GetChild(1).GetComponent<TMP_Text>();
        descriptionText = playTab.transform.GetChild(4).GetComponent<TMP_Text>();
        imageComponent = playTab.transform.GetChild(2).GetComponent<Image>();
    }

    public void SetUpPlayTab(int level) {
        playTab.SetActive(true);
        titleText.text = titles[level];
        descriptionText.text = descriptions[level];

        // Check if the level is unlocked
        if (CheckIfCleared(level)) {
            imageComponent.sprite = maps[level + 1];
        } else {
            imageComponent.sprite = maps[0];
        }
    }

    bool CheckIfCleared(int level) {
        // If it's Tokyo
        if (level == 0) {
            return true;
        }
        if (PlayerPrefs.GetInt(level.ToString() + "cleared", 0) == 1) {
            return true;
        } else {
            return false;
        }
    }
}