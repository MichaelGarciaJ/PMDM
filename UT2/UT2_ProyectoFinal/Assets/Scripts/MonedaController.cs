using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaController : MonoBehaviour
{
    public int valor = 1;
    public AudioClip sonidoMoneda;
   
   private void OnTriggerEnter2D(Collider2D other) {
    
    if(other.CompareTag("Player")){
        GameManager.instancia.SumarMonedas(valor);

        AudioManager.instancia.reproducirSonido(sonidoMoneda);
        Destroy(gameObject);
    }
   }

}
