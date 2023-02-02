using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSetUpScript : MonoBehaviour
{
    [SerializeField] private GameObject[] levelMarkers;

    public void SetUpMarkers() {
        for (int i = 0; i < levelMarkers.Length; i++) {
            if (CheckIfCleared(i + 1)) {
                levelMarkers[i].SetActive(true);
                if (!(i == 5)) {
                    levelMarkers[i + 1].SetActive(true);
                }
            } else {
                if (i + 1 == 1) {
                    // Tokyo always active
                    levelMarkers[i].SetActive(true);
                } else {
                    if (!levelMarkers[i].activeSelf) {
                        levelMarkers[i].SetActive(false);
                    }
                }
            }
        }
    }

    bool CheckIfCleared(int level) {
        if (PlayerPrefs.GetInt(level.ToString() + "cleared", 0) == 1) {
            return true;
        } else {
            return false;
        }
    }

}
