using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoaderScript : MonoBehaviour
{
    public Texture2D sourceImage;

    private Dictionary<Vector2, Color> pixelData;

    // Start is called before the first frame update
    void Start()
    {
        pixelData = new Dictionary<Vector2, Color>();
        Color[] pixels = sourceImage.GetPixels(0, 0, 1500, 1500);
        int pointer = 0;
        // Read color of each pixel of the loaded picture
        for(int y = 0; y < 15; y++) {
            // Go to the next row
            pointer = y * 150000;
            for(int x = 0; x < 15; x++) {
                // Read pixel every 100 pixels
                Debug.Log(x * 100 + pointer);
                // Store each data into a dictionary
                pixelData[new Vector2(x, y)] = pixels[x * 100 + pointer];
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
