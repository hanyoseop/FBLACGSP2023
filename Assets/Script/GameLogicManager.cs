using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public ImageLoaderScript imageloader;
    public GridManager gridManager;
    public WordManagerScript wordManager;
    public GUIManager guiManager;
    public GameManagerScript gameManager;
    public ScoreScript scoreManager;

    public ScorePopup scorePopupPrefab;
    
    // (x-coord, y-coord, direction)
    // 1 - North, 2 - East, 3 - South, 4 - West
    private Dictionary<Vector3, int> wordMappingData;
    private List<string> wordBank = new List<string>();

    private int stageIndex = 1;

    void Start() {
        SetUp();
    }

    void SetUp() {
        ArrangeWordSetup();
        PlaceWords();
        guiManager.StartTimer();
        guiManager.SetUpWordBank(wordBank);
    }

    void PlaceWords() {
        foreach(KeyValuePair<Vector3, int> data in wordMappingData) {
            // Get necessary words
            string word = wordManager.LookForWords(data.Value);
            Debug.Log(word);
            // Debug.Log(data.Key);
            // Debug.Log(data.Value);
            wordBank.Add(word);
            for (int i = 0; i < data.Value; i++) {
                if (data.Key.z == 1) {
                    Tile targetTile = gridManager.GetTileAtPosition(1, new Vector2(data.Key.x, data.Key.y + i));
                    gridManager.SetLetterToTile(targetTile, word[i]);
                } else if (data.Key.z == 2) {
                    Tile targetTile = gridManager.GetTileAtPosition(1, new Vector2(data.Key.x + i, data.Key.y));
                    gridManager.SetLetterToTile(targetTile, word[i]);
                } else if (data.Key.z == 3) {
                    Tile targetTile = gridManager.GetTileAtPosition(1, new Vector2(data.Key.x, data.Key.y - i));
                    gridManager.SetLetterToTile(targetTile, word[i]);
                } else if (data.Key.z == 4) {
                    Tile targetTile = gridManager.GetTileAtPosition(1, new Vector2(data.Key.x - i, data.Key.y));
                    gridManager.SetLetterToTile(targetTile, word[i]);
                }
            }

        }
    }

    // Set up words on to the letter grid based on the following algorithm
    void ArrangeWordSetup() {
        wordMappingData = new Dictionary<Vector3, int>();
        for (int y = 0; y < 15; y++) {
            for (int x = 0; x < 15; x++) {
                // If black, search the surrounding tile
                if (imageloader.GetMappingData(new Vector2(x, y))) {
                    // Variable to check if something was there
                    bool hasSomethingAround = false;
                    if(imageloader.GetMappingData(new Vector2(x + 1, y))) {
                        // Check East
                        hasSomethingAround = true;
                        int counter = 0;
                        Vector2[] backup = new Vector2[15]; 
                        while (imageloader.GetMappingData(new Vector2(x + counter, y))) {
                            backup[counter] = new Vector2(x, y + counter);
                            imageloader.RemoveMappingData(new Vector2(x + counter, y));
                            counter++;
                        }
                        // If it's too short restore backup
                        if (counter < 3) {
                            foreach (Vector2 restore in backup) {
                                imageloader.RestoreMappingData(restore);
                            }
                        } else {
                            int currentLevel = gameManager.GetCurrentLevel();
                            if (currentLevel == 1 || currentLevel == 2) {
                                // Level 1 and 2 have no flip
                                wordMappingData[new Vector3(x, y, 2)] = counter;
                            } else if (currentLevel == 3 || currentLevel == 4) {
                                // Level 3 and 4 have horizontal flip
                                int flipRandom = Random.Range(0, 2);
                                // Horizontal
                                if (flipRandom == 0) {
                                    wordMappingData[new Vector3(x + counter - 1, y, 4)] = counter;
                                } else {
                                    wordMappingData[new Vector3(x, y, 2)] = counter; 
                                }
                            } else if (currentLevel == 5 || currentLevel == 6) {
                                // Level 5 and 6 have both flips
                                int flipRandom = Random.Range(0, 2);
                                // Horizontal
                                if (flipRandom == 0) {
                                    wordMappingData[new Vector3(x + counter - 1, y, 4)] = counter;
                                } else {
                                    wordMappingData[new Vector3(x, y, 2)] = counter; 
                                }
                            }
                        }
                    } 
                    if(imageloader.GetMappingData(new Vector2(x, y + 1))) {
                        // Check North
                        hasSomethingAround = true;
                        int counter = 0;
                        Vector2[] backup = new Vector2[15]; 
                        while (imageloader.GetMappingData(new Vector2(x, y + counter))) {
                            backup[counter] = new Vector2(x, y + counter);
                            imageloader.RemoveMappingData(new Vector2(x, y + counter));
                            counter++;
                        }
                        // If it's too short restore backup
                        if (counter < 3) {
                            foreach (Vector2 restore in backup) {
                                imageloader.RestoreMappingData(restore);
                            }
                        } else {
                            int currentLevel = gameManager.GetCurrentLevel();
                            if (currentLevel == 1 || currentLevel == 2) {
                                // Level 1 and 2 have no flip
                                wordMappingData[new Vector3(x, y + counter - 1, 3)] = counter;
                            } else if (currentLevel == 3 || currentLevel == 4) {
                                // Level 3 and 4 have horizontal flip
                                wordMappingData[new Vector3(x, y, 1)] = counter;
                            } else if (currentLevel == 5 || currentLevel == 6) {
                                // Level 5 and 6 have both flips
                                int flipRandom = Random.Range(0, 2);
                                // top to bottom
                                if (flipRandom == 0) {
                                    wordMappingData[new Vector3(x, y + counter - 1, 3)] = counter;
                                } else {
                                    wordMappingData[new Vector3(x, y, 1)] = counter; 
                                }
                            }
                        }
                    }
                    if (!hasSomethingAround) {
                        imageloader.RemoveMappingData(new Vector2(x, y));
                    } 
                }
            }
        }
    }

    public void CheckAnswer(string selectedLetters, List<Tile> selectedTilesList) {
        if (wordBank.Contains(selectedLetters)) {
            // To update wordbank
            wordBank.Remove(selectedLetters);
            guiManager.SetUpWordBank(wordBank);
            // Audio
            FindObjectOfType<AudioManager>().Play("Success");

            // When all words are found
            if (wordBank.Count == 0) {
                // Check if more stages are there
                FindObjectOfType<AudioManager>().Play("Clear");
                gridManager.Reveal();
                Invoke("EndOfStageManagement", 2f);
            }
            // To add score
            float scoreToAdd = guiManager.GetRemainingTime() / guiManager.GetStartingTime() * 10f * selectedTilesList.Count;
            GenerateScorePopup(selectedTilesList[0].GetPosition(), (int)scoreToAdd);
            scoreManager.AddScore(scoreToAdd);
            foreach(Tile tile in selectedTilesList) {
                tile.Hide();
                Tile pixelTile = gridManager.GetTileAtPosition(0, tile.GetCoordinate());
                pixelTile.Expose();
            }
        }
    }

    // Check wether the level has more than one stages. If it does, load next stage.
    void EndOfStageManagement() {
        if (imageloader.GetNumberOfImage() > 1 && stageIndex < imageloader.GetNumberOfImage()) {
            gridManager.HideAll();
            imageloader.NextImage();
            gridManager.LoadCurrentImage();
            SetUp();
            stageIndex += 1;
        } else {
            PlayerPrefs.SetInt(gameManager.GetCurrentLevel().ToString() + "cleared", 1);
            gameManager.EndGame(imageloader.GetNumberOfImage());
        }
    }

    void GenerateScorePopup(Vector3 position, int scoreAmount) {
        var spawnedScore = Instantiate(scorePopupPrefab, position, Quaternion.identity);
        spawnedScore.Generate(scoreAmount);
    }

    public Dictionary<Vector3, int> GetWordMappingData() {
        return wordMappingData;
    }
}
