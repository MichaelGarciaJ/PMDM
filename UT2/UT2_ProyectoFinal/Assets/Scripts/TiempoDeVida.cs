using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TiempoDeVida : MonoBehaviour
{

    public float tiempoVida;

    //El objeto se destruye cuando se acabe el tiempo definido.
    private void Start()
    {
        Destroy(gameObject, tiempoVida);
    }
}
