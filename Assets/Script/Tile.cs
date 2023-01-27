using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;

    public void Init(Color color) {
        renderer.color = color;
    }
}
