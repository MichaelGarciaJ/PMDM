using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaController : MonoBehaviour
{
    Transform jugador;

    public float velocidad = 7;
    public GameObject nubeDeHumoPrefab;
    public float fuerzaEmpuje = 10f;
    public float tiempoParalisis = 0.5f;
    
    void Start() {
        jugador = GameObject.Find("--- Player ---").GetComponent<Transform>();
    }

    void Update() {
        Vector3 direccion = Vector3.zero;
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bala")) {
            Instantiate(nubeDeHumoPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Player")) {
            Transform playerTransform = other.gameObject.GetComponent<Transform>();
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            
            Vector2 direccion = (playerTransform.position - transform.position).normalized;
            playerRb.AddForce(direccion * fuerzaEmpuje, ForceMode2D.Impulse);
            GameManager.instancia.QuitarVidaJugador();
        }
    }
}
