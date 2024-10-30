using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorMonedasController : MonoBehaviour
{

    public GameObject monedaPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Jugador"))
        {
            Vector3 posicionAleatoria = new Vector3(Random.Range(-6f, 0), Random.Range(1f, 4), 0);

            Instantiate(monedaPrefab, posicionAleatoria, Quaternion.identity);
        }
    }
}
