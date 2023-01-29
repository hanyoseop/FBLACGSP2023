using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject highlight;

    public void Init(Color color) {
        renderer.color = color;
    }

    public void Init(Sprite letterSprite) {
        renderer.sprite = letterSprite;
        tile.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
        highlight.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
        highlight.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.314f);
    }

    public void Expose() {
        tile.SetActive(true);
    }

    public void Hide() {
        tile.SetActive(false);
    }

    void OnMouseEnter() {
        highlight.SetActive(true);
    }

    void OnMouseExit() {
        highlight.SetActive(false);
    }
}
