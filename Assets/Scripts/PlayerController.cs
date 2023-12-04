using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpForce = 1f;

    [SerializeField] float lineLength = 1f;
    [SerializeField] float offset = 1f;

    [SerializeField] bool isJumping = false;

    void Update() {
        // Movimiento a la derecha
        /*if (Input.GetAxisRaw("Horizontal") == 1) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        // Movimiento a la izquierda
        if (Input.GetAxisRaw("Horizontal") == -1) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        // Movimiento a la horizontal
        if (Input.GetAxisRaw("Horizontal") == 0) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }*/

        // Movimiento a la derecha / izquierda / horizontal_0
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed * Input.GetAxisRaw("Horizontal"), GetComponent<Rigidbody2D>().velocity.y);

        // Girar el Sprite
        if (Input.GetAxisRaw("Horizontal") == 1) GetComponent<SpriteRenderer>().flipX = false;
        if (Input.GetAxisRaw("Horizontal") == -1) GetComponent<SpriteRenderer>().flipX = true;

        // Salto solo si no está en el aire
        if (Input.GetButtonDown("Fire1") && !isJumping) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Dibujamos la linea
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - offset);
        Vector2 target = new Vector2(transform.position.x, transform.position.y - offset - lineLength);
        Debug.DrawLine(origin, target, Color.black);

        // Raycast para detectar colisiones (origen, hacia dónde apunta, largo)
        RaycastHit2D raycast = Physics2D.Raycast(origin, Vector2.down, lineLength);

        // Detectamos colisiones con el Raycast
        // Si deberia colisionar pero no lo hace, cambiar el Composite Collider 2D -> Geometry Type --> Polygons
        if (raycast.collider == null) isJumping = true;
        else isJumping = false;
    }
}
