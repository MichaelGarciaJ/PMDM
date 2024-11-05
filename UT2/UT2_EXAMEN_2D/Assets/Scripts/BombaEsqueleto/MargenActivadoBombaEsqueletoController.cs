using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MargenActivadoBombaEsqueletoController : MonoBehaviour
{
    BombaEsqueletoController bombaEsqueletoController;

    void Start() {
        bombaEsqueletoController = transform.parent.GetComponent<BombaEsqueletoController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        bombaEsqueletoController.JugadorAlAlcance = true;
        if (other.CompareTag("Player")) {
            bombaEsqueletoController.ActivarBomba();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        bombaEsqueletoController.JugadorAlAlcance = false;
    }
}
