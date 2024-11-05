using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float velocidad = 30f;
    private Vector2 direccion;

    void Start() {
        GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
    }

    public void EstablecerDireccion(Vector2 nuevaDireccion) {
        direccion = nuevaDireccion.normalized;
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
