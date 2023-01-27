using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) {
            Quit();
        }
    }

    public void Quit() {
        Application.Quit();
    }
}
