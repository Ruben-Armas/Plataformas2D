using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] float xMin;
    [SerializeField] float xMax;

    private void Start() {
        AudioManager.instance.PlayMusic("MainTheme");  // Llama la clase AudioManager para reproducirlamúsica principal
    }

    void Update() {
        // Mueve la cámara como si fuera hija del Player
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // Camara que sigue al personaje (NO en Y) y no sale del mapa
        

        transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x, xMin, xMax),   // Clamp -> valor entre un Min y un Max
            transform.position.y,   // La y, z se quedan igual
            transform.position.z);

    }
}
