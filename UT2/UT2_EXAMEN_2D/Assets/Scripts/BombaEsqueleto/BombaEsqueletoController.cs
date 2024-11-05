using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaEsqueletoController : MonoBehaviour
{
    public float timeToExplode = 3;
    public float fuerzaExplosion = 3;
    public GameObject humoPrefab;
    public GameObject explosionPrefab;

    Animator animator;
    CircleCollider2D circleCollider2D;

    float cooldownToExplode;
    bool activa;
    bool jugadorAlAlcance;
    public bool JugadorAlAlcance { set { jugadorAlAlcance = value; }}
    
    void Start() {
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        cooldownToExplode = timeToExplode;
        activa = false;
        jugadorAlAlcance = false;
    }

    void Update() {
        if (activa) {
            cooldownToExplode -= Time.deltaTime;
            if (cooldownToExplode <= 0) {
                animator.SetBool("explotar", true);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                if (jugadorAlAlcance) DañarJugador();
                Destroy(gameObject);
            }
        }
    }

    public void ActivarBomba() {
        if (!activa) {
            activa = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Instantiate(humoPrefab, transform.position, Quaternion.identity);
            
            // Cambio la animación y colliders
            animator.SetBool("activa", true);
            circleCollider2D.offset = new Vector2(circleCollider2D.offset.x, -0.25f);
            transform.Find("Patas").gameObject.SetActive(true);
        }
    }

    void DañarJugador() {
        GameObject player = GameObject.Find("--- Jugador ---").gameObject;

        // Aplico la fuerza de la explosión
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Vector2 direccion = player.transform.position - transform.position;
        playerRb.AddForce(direccion * fuerzaExplosion, ForceMode2D.Impulse);

        // Quito una vida al jugador
        GameManager.instancia.QuitarVidaJugador();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bala")) {
            Instantiate(humoPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
