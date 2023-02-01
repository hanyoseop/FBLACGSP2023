using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] TextMeshPro textMesh;
    [SerializeField] GameObject scorePopupObject;

    Transform scorePopupTransform;

    private float disappearTimer = 1f;

    void Awake() {
        scorePopupTransform = scorePopupObject.transform;
    }

    public void Generate(int scoreAmount) {
        textMesh.SetText(scoreAmount.ToString());
    }

    private void Update() {
        Debug.Log("Im here");
        float moveYSpeed = 1f;
        scorePopupTransform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            Destroy(scorePopupObject);
        }
    }
}
