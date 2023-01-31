using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoaderScript : MonoBehaviour
{
    [SerializeField] private Texture2D sourceImage;
    [SerializeField] private Texture2D mappingImage;

    private Dictionary<Vector2, Color> pixelData;
    private Dictionary<Vector2, bool> mappingData;

    // Start is called before the first frame update
    void Awake()
    {
        LoadImageData();
        LoadMappingData();
    }

    void LoadImageData() {
        pixelData = new Dictionary<Vector2, Color>();
        Color[] pixels = sourceImage.GetPixels(0, 0, 1500, 1500);
        int pointer = 0;
        // Read color of each pixel of the loaded picture
        for(int y = 0; y < 15; y++) {
            // Go to the next row
            pointer = y * 150000;
            for(int x = 0; x < 15; x++) {
                // Read pixel every 100 pixels
                // Store each data into a dictionary
                pixelData[new Vector2(x, y)] = pixels[x * 100 + pointer];
            }
        }
    }

    void LoadMappingData() {
        mappingData = new Dictionary<Vector2, bool>();
        Color[] rawMappingData = mappingImage.GetPixels(0, 0, 15, 15);
        int pointer = 0;
        for(int y = 0; y < 15; y++) {
            pointer = y * 15;
            for(int x = 0; x < 15; x++) {
                if (rawMappingData[x + pointer] == Color.black) {
                    // Set true if yes black
                    mappingData[new Vector2(x, y)] = true;
                } else {
                    // Set false if not black
                    mappingData[new Vector2(x, y)] = false;
                }
            }
        }
    }

    public Color GetPixelData(Vector2 position) {
        if (pixelData.TryGetValue(position, out Color pixel)) {
            return pixel;
        }
        // Return transparent pixel if error happens
        return new Color(1, 0, 0, 0);
    }

    public bool GetMappingData(Vector2 position) {
        if (mappingData.TryGetValue(position, out bool data)) {
            return data;
        }
        return false;
    }

    public void RestoreMappingData(Vector2 position) {
        mappingData[position] = true;
    }

    public void RemoveMappingData(Vector2 position) {
        mappingData[position] = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
