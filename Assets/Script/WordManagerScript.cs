using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManagerScript : MonoBehaviour
{
    public TextAsset wordList;
    string[] words; 

    void Awake() {
        ProcessWordlist();
    }

    void ProcessWordlist() {
        string text = wordList.text;
        words = text.Split("\r\n");
    }

    public string LookForWords(int length) {
        List<string> correctLengthWord = new List<string>();
        foreach(string word in words) {
            if (word.Length == length) {
                correctLengthWord.Add(word);
            }
        }
        return correctLengthWord[Random.Range(0, correctLengthWord.Count)];
    }

}
