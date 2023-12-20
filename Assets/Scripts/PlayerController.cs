using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpForce = 1f;

    [SerializeField] float lineLength = 1f;
    [SerializeField] float offset = 1f;

    [SerializeField] bool isJumping = false;

    // Obtenemos la referencia al sistema de partículas
    [SerializeField] ParticleSystem jumpParticles;

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
        //if (Input.GetButtonDown("Fire1") && !isJumping) {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
                jumpParticles.Play();// Iniciamos la producción de partículas
            AudioManager.instance.PlaySFX("Jump");  // Llama la clase AudioManager para reproducir un efecto de sonido
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




        // ------------------------------------------------------------------------------------------
        // APLICACIÓN AL JUEGO DE PLATAFORMAS QUE UTILIZA RAYCAST PARA DETECTAR QUE ESTÁ EN EL SUELO
        // ------------------------------------------------------------------------------------------
        // Si el raycast no toca con nada el personaje está en el aire
        if (raycast.collider == null) {
            SetAnimation("jump");
        }
        else {
            // Si está sobre una superficie pero se mueve lateralmente
            if (GetComponent<Rigidbody2D>().velocity.x != 0) SetAnimation("run");
            else SetAnimation("idle"); // Si está sobre una superficie pero no se mueve
        }
    }



    // -----------------------------------------------------------------------------
    // Método que desactiva todos los parámetros del Animator y activa uno concreto
    // -----------------------------------------------------------------------------
    void SetAnimation(string name) {

        // Obtenemos todos los parámetros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;

        // Recorremos todos los parámetros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name, false);

        // Activamos el pasado por parámetro
        GetComponent<Animator>().SetBool(name, true);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            if (collision.collider.CompareTag("Enemy")) {
                Debug.Log("Enemy");
                AudioManager.instance.PlaySFX("Hit");
                AudioManager.instance.PlayMusic("LoseALife");

                SCManager.instance.LoadScene("GameOver");
            }
        }
    }
}
