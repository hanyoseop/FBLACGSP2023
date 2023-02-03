using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GUIManager guiManager;

    [SerializeField] private GameObject instructionBoard;

    [SerializeField] GameObject adminSetting;

    void Awake() {
        if (GetCurrentLevel() == 0) {
            ManageInstruction();
        }

        if (GetCurrentLevel() == 7) {
            FindObjectOfType<AudioManager>().Play("Game Clear");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab)) {
            LoadMainMenu();
        }
        if(Input.GetKey(KeyCode.KeypadMinus)) {
            adminSetting.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Page Flip");
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

    public void ResetClearData() {
        for (int i = 0; i < 6; i++) {
            int level = i + 1;
            PlayerPrefs.SetInt(level.ToString() + "cleared", 0);
        }
    }

    public void UnlockAllStages() {
        for (int i = 0; i < 6; i++) {
            int level = i + 1;
            PlayerPrefs.SetInt(level.ToString() + "cleared", 1);
        }
    }

    public void ManageInstruction() {
        if (PlayerPrefs.GetInt("firstime", 1) == 1) {
            instructionBoard.SetActive(true);
            PlayerPrefs.SetInt("firstime", 0);
        } else {
            instructionBoard.SetActive(false);
        }
    }

    public void EndGame(int numberOfStages) {
        guiManager.LevelCleared(numberOfStages);
    }

    public void Quit() {
        PlayerPrefs.SetInt("firstime", 1);
        Debug.Log("Qutting...");
        Application.Quit();
    }
}
