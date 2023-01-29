using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public ImageLoaderScript imageloader;
    
    // (x-coord, y-coord, direction)
    // 1 - North, 2 - East, 3 - South, 4 - West
    private Dictionary<Vector3, int> wordMappingData;

    void Start() {
        ArrangeWordSetup();
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
                           wordMappingData[new Vector3(x, y, 1)] = counter; 
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
                           wordMappingData[new Vector3(x, y, 2)] = counter; 
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

    public Dictionary<Vector3, int> GetWordMappingData() {
        return wordMappingData;
    }
}
