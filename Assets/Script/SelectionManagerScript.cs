using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerScript : MonoBehaviour
{
    public GameLogicManager gameLogicManager;
    private List<Tile> selectedTilesList = new List<Tile>();
    private string selectedLetters; 

    public void AddToSelected(Tile tile) {
        if (!selectedTilesList.Contains(tile)) {
            selectedTilesList.Add(tile);
            selectedLetters += tile.GetLetter();
        }
    }

    public void ClearLists() {
        selectedTilesList = new List<Tile>();
        selectedLetters = null;
    }

    public void DeselectAllTiles() {
        foreach(Tile tile in selectedTilesList) {
            tile.Dehighlight();
        }
    }

    public void CheckSelection() {
        gameLogicManager.CheckAnswer(selectedLetters, selectedTilesList);
    }

    public List<Tile> GetSelectedTilesList() {
        return selectedTilesList;
    }

    public string GetSelectedLetters() {
        return selectedLetters;
    }
}
