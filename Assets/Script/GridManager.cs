using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public ImageLoaderScript imageloader;
    public Sprite[] letterSpriteArray;
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private float scaleValue;

    private Dictionary<Vector2, Tile> pixelTiles;
    private Dictionary<Vector2, Tile> letterTiles;

    void Awake() {
        LoadCurrentImage();
    }

    public void LoadCurrentImage() {
        LoadPictureInGrid();
        GenerateGridWithLetter();
    }

    // Generate random letter tiles and save them into a dictionary for later use. 
    void GenerateGridWithLetter() {
        letterTiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x * scaleValue - 2.38f, y * scaleValue - 4.048f), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.localScale = new Vector3(0.28933f, 0.28933f, 1);
                spawnedTile.Init(letterSpriteArray[Random.Range(0, 26)]);
                // spawnedTile.Hide();
                // Save tiles into a dictionary
                letterTiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }

    // Spawn picture in form of tiles and save reference data in a dictionary for later use.
    void LoadPictureInGrid() {
        pixelTiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x * scaleValue - 2.38f, y * scaleValue - 4.048f), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                // Set color of a tile based on the image data
                spawnedTile.Init(imageloader.GetPixelData(new Vector2(x, y)));
                spawnedTile.Hide();

                // Save tiles into a dictionary
                pixelTiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }
    
    // Reveal when game is cleared
    public void Reveal() {
        for (int y = 0; y < 15; y++) {
            for (int x = 0; x < 15; x++) {
                pixelTiles[new Vector2(x, y)].Expose();
                letterTiles[new Vector2(x, y)].Hide();
            }
        }
    }

    // Hide all picture tiles
    public void HideAll() {
        for (int y = 0; y < 15; y++) {
            for (int x = 0; x < 15; x++) {
                pixelTiles[new Vector2(x, y)].Hide();
            }
        }
    }

    // Set letter to a tile decided in Game Logic Manager
    public void SetLetterToTile(Tile tile, char letter) {
        tile.Expose();
        tile.ChangeLetter(letterSpriteArray[(int)letter - 97]);
    }

    // return tile reference based on type and position
    public Tile GetTileAtPosition(int type, Vector2 position) {
        if (type == 0) {
            if (pixelTiles.TryGetValue(position, out var tile)) {
                return tile;
            }    
        } else if (type == 1) {
            if (letterTiles.TryGetValue(position, out var tile)) {
                return tile;
            }    
        }
        return null;
    }
}
