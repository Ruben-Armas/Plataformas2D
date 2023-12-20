using TMPro;
using UnityEngine;

public class CollisionAddScore : MonoBehaviour {
    [SerializeField] int score;

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica si el objeto con el que ha colisionado tiene el tag "Player"
        if (collision != null && collision.gameObject.CompareTag("Star")) {
            score++;
            Debug.Log("Score");
            GameObject.Find("Score").GetComponent<TMP_Text>().text = "Score: "+ score.ToString();

            // Destruye el objeto actual
            Destroy(collision.gameObject);
        }
    }
}
