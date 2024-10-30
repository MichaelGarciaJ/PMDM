using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaController : MonoBehaviour
{

    public int valor = 1;
    public GameManager gameManager;

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Jugador")){
            gameManager.SumarMonedas(valor);
            Destroy(gameObject);
        }
    }

}
