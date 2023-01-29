using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool selected = false;

    private SelectionManagerScript selectionManager;

    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Tile tile;
    [SerializeField] private GameObject tileObject;
    [SerializeField] private GameObject highlight;

    void Awake() {
        selectionManager = FindObjectOfType<SelectionManagerScript>();
    }

    public void Init(Color color) {
        renderer.color = color;
    }

    public void Init(Sprite letterSprite) {
        renderer.sprite = letterSprite;
        tile.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
        highlight.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
        highlight.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.9f, 1f, 0.314f);
    }

    public void ChangeLetter(Sprite letterSprite) {
        renderer.sprite = letterSprite;
    }

    public string GetLetter() {
        return renderer.sprite.name;
    }

    public Vector2 GetCoordinate() {
        string[] nameSplited = tileObject.name.Split(" ");
        return new Vector2(int.Parse(nameSplited[1]), int.Parse(nameSplited[2]));
    }


    public void Expose() {
        tileObject.SetActive(true);
    }

    public void Hide() {
        tileObject.SetActive(false);
    }

    public void Dehighlight() {
        highlight.SetActive(false);
    }

    // Selecting Control
    void OnMouseDown() {
        selected = true;
        selectionManager.AddToSelected(tile);
    }

    void OnMouseEnter() {
        if (Input.GetMouseButton(0)) {
            selected = true;
            selectionManager.AddToSelected(tile);
        }

        highlight.SetActive(true);
    }

    void OnMouseExit() {
        if (Input.GetMouseButton(0)) {
            highlight.SetActive(true);
        } else {
            highlight.SetActive(false);
        }
    }

    void OnMouseUp() {
        selected = false;
        selectionManager.DeselectAllTiles();
        selectionManager.CheckSelection();
        selectionManager.ClearLists();
    }
}
