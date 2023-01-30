using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GUIManager guiManager;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab)) {
            LoadMainMenu();
        }
        if(Input.GetKey(KeyCode.Escape)) {
            Quit();
        }
    }

    public void NextLevel() {
        SceneManager.LoadScene(GetCurrentLevel() + 1);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayLevel(int level) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public int GetCurrentLevel() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void EndGame() {
        guiManager.LevelCleared();
    }

    public void Quit() {
        Debug.Log("Qutting...");
        Application.Quit();
    }
}
