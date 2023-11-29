using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    void Update() {

        // Movimiento a la derecha
        if (Input.GetAxisRaw("Horizontal") == 1) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        // Movimiento a la izquierda
        if (Input.GetAxisRaw("Horizontal") == -1) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        // Movimiento a la horizontal
        if (Input.GetAxisRaw("Horizontal") == 0) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
        // Salto
        if (Input.GetButtonDown("Fire1")) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
