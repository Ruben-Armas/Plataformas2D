using TMPro;
using UnityEngine;

public class CollisionSelfSound : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica si el objeto con el que ha colisionado tiene el tag "Player"
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            AudioManager.instance.PlaySFX("CollectCoin");  // Llama la clase AudioManager para reproducir un efecto de sonido

            // Destruye el objeto actual
            //Destroy(collision.gameObject);
        }
    }
}
