using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))
            SCManager.instance.LoadScene("Game");
    }
}
