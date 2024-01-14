using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideSceneMap : MonoBehaviour {
    [SerializeField] bool isShowing;

    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            if (!isShowing) {
                // Pausar el juego
                Time.timeScale = 0f;
                isShowing = true;
                SCManager.instance.LoadSceneAdditive("Map");
            } else {
                // Despausar el juego
                Time.timeScale = 1f;
                isShowing = false;
                SCManager.instance.UploadSceneAdditive("Map");
            }
        }
    }
}
