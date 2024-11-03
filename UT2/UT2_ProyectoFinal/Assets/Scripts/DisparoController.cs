using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DisparoController : MonoBehaviour
{

    public float velocidad;
    public float dano;

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    // Método que quita daño al enemigo.
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemigo"))
        {
            other.GetComponent<Enemigo1Controller>().tomarDano(dano);
            Destroy(gameObject);
        }
    }
}
