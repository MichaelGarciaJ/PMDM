using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasControllers : MonoBehaviour
{

    private MeshCollider meshCollider;
    private bool isActivado = false;

    void Start()
    {

        meshCollider = GetComponent<MeshCollider>();
    }

    // Método en el que si el jugador choca con la puerta se pondrá trigger para poder pasarla.
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivado)
        {
            if(meshCollider != null){
                meshCollider.isTrigger = true;
                isActivado = true;
            }
        }
    }

    // Método en el que si el jugador sale de la puerta se dejará de activar trigger para que no pueda pasar de nuevo.
    private void OnTriggerExit(Collider other){

        if(other.gameObject.CompareTag("Player") && isActivado){

            if(meshCollider != null){
                meshCollider.isTrigger = false;
            }
        }
    }

}
