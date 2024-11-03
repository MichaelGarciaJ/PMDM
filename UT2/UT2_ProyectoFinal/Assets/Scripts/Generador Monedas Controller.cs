using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorMonedasController : MonoBehaviour
{
    public GameObject moneda;
    private int contador = 0;
    private int maxMonedas = 3;
    private Collider2D generadorCollider;

    void Start()
    {
        generadorCollider = GetComponent<Collider2D>();
    }


    // Método que si tocas el cofre se genera monedas aleatoriamente dentro de un rango.
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player") && contador < maxMonedas)
        {
            Vector3 posAleatoria = new Vector3(Random.Range(generadorCollider.bounds.min.x, generadorCollider.bounds.max.x),
                    Random.Range(generadorCollider.bounds.min.y, generadorCollider.bounds.max.y), 0);

            Instantiate(moneda, posAleatoria, Quaternion.identity);
            contador++;
        }

        if (contador == maxMonedas)
        {
            Destroy(gameObject);
        }
    }

}
