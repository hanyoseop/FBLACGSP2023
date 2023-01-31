using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitButtonScript : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameField;
    [SerializeField] private TMP_Text alertText;
    [SerializeField] private ScoreScript scoreManager;
    
    private bool submitted = false;
    
    public void GetAndSaveScore() {
        if(!submitted) {
            scoreManager.SaveScore(usernameField.text);
            alertText.gameObject.SetActive(true);
            submitted = true;
        } else {
            alertText.text = "already submitted!";
            alertText.color = new Color(1, 0, 0, 1);
        }
    }

    
}
