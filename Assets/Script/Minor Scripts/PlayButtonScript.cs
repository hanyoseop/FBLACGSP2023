using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] private GameManagerScript gameManager;

    [SerializeField] private TMP_Text title;

    public void PlayLevel() {
        if (title.text == "tokyo") {
            gameManager.PlayLevel(1);
        } else if (title.text == "suzhou") {
            gameManager.PlayLevel(2);
        } else if (title.text == "marrakesh") {
            gameManager.PlayLevel(3);
        } else if (title.text == "paris") {
            gameManager.PlayLevel(4);
        } else if (title.text == "los angeles") {
            gameManager.PlayLevel(5);
        } else {
            gameManager.PlayLevel(6);
        }
    }
}
