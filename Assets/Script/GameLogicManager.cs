using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public ImageLoaderScript imageloader;
    public GridManager gridManager;
    public WordManagerScript wordManager;
    
    // (x-coord, y-coord, direction)
    // 1 - North, 2 - East, 3 - South, 4 - West
    private Dictionary<Vector3, int> wordMappingData;
    private List<string> wordBank = new List<string>();

    void Start() {
        ArrangeWordSetup();
        PlaceWords();
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

    void ArrangeWordSetup() {
        wordMappingData = new Dictionary<Vector3, int>();
        for (int y = 0; y < 15; y++) {
            for (int x = 0; x < 15; x++) {
                // If black, search the surrounding tile
                if (imageloader.GetMappingData(new Vector2(x, y))) {
                    // Variable to check if something was there
                    bool hasSomethingAround = false;
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
                            // MODIFY IT SO IT DEPENDS ON LEVEL
                            int flipRandom = Random.Range(0, 2);
                            // top to bottom
                            if (flipRandom == 0) {
                                wordMappingData[new Vector3(x, y + counter - 1, 3)] = counter;
                            } else {
                                wordMappingData[new Vector3(x, y, 1)] = counter; 
                            }
                        }
                    }
                    if(imageloader.GetMappingData(new Vector2(x + 1, y))) {
                        // Check East
                        hasSomethingAround = true;
                        int counter = 0;
                        Vector2[] backup = new Vector2[15]; 
                        while (imageloader.GetMappingData(new Vector2(x + counter, y))) {
                            imageloader.RemoveMappingData(new Vector2(x + counter, y));
                            counter++;
                        }
                        // If it's too short restore backup
                        if (counter < 3) {
                            foreach (Vector2 restore in backup) {
                                imageloader.RestoreMappingData(restore);
                            }
                        } else {
                            // MODIFY IT SO IT DEPENDS ON LEVEL
                            int flipRandom = Random.Range(0, 2);
                            // Horizontal
                            if (flipRandom == 0) {
                                wordMappingData[new Vector3(x + counter - 1, y, 4)] = counter;
                            } else {
                                wordMappingData[new Vector3(x, y, 2)] = counter; 
                            }
                        }
                    } 
                    if(imageloader.GetMappingData(new Vector2(x, y - 1))) {
                        // Check South
                        hasSomethingAround = true;
                        int counter = 0;
                        Vector2[] backup = new Vector2[15]; 
                        while (imageloader.GetMappingData(new Vector2(x, y - counter))) {
                            imageloader.RemoveMappingData(new Vector2(x, y - counter));
                            counter++;
                        }
                        // If it's too short restore backup
                        if (counter < 3) {
                            foreach (Vector2 restore in backup) {
                                imageloader.RestoreMappingData(restore);
                            }
                        } else {
                           wordMappingData[new Vector3(x, y, 3)] = counter; 
                        }
                    } 
                    if(imageloader.GetMappingData(new Vector2(x - 1, y))) {
                        // Check West
                        hasSomethingAround = true;
                        int counter = 0;
                        Vector2[] backup = new Vector2[15]; 
                        while (imageloader.GetMappingData(new Vector2(x - counter, y))) {
                            imageloader.RemoveMappingData(new Vector2(x - counter, y));
                            counter++;
                        }
                        // If it's too short restore backup
                        if (counter < 3) {
                            foreach (Vector2 restore in backup) {
                                imageloader.RestoreMappingData(restore);
                            }
                        } else {
                           wordMappingData[new Vector3(x, y, 4)] = counter; 
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
        Debug.Log(wordBank.Contains(selectedLetters));
        Debug.Log(selectedLetters);
        Debug.Log(selectedLetters.Length);
        if (wordBank.Contains(selectedLetters)) {
            Debug.Log("It's here!");
            wordBank.Remove(selectedLetters);
            if (wordBank.Count == 0) {
                gridManager.Reveal();
            }
            // Add Score or smth
            foreach(Tile tile in selectedTilesList) {
                tile.Hide();
                Tile pixelTile = gridManager.GetTileAtPosition(0, tile.GetCoordinate());
                pixelTile.Expose();
            }
        }
    }

    public Dictionary<Vector3, int> GetWordMappingData() {
        return wordMappingData;
    }
}
