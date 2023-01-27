using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public ImageLoaderScript imageloader;
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private float scaleValue;

    void Start() {
        GenerateGrid();
    }

    void GenerateGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x * scaleValue - 2.38f, y * scaleValue - 4.048f), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init(imageloader.GetPixelData(new Vector2(x, y)));
            }
        }
    }
}
