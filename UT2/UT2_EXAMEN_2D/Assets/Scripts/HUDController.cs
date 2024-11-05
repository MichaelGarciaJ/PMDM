using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private void Start() {
        if (GameManager.instancia != null) {
            Button botonPausa = GameObject.Find("Pausa").GetComponent<Button>();
            botonPausa.onClick.AddListener(GameManager.instancia.PausarJuego);

            GameObject botonesContenedor = GameObject.Find("-- Botones --");
            Button botonContinuar = botonesContenedor.transform.Find("Continuar").GetComponent<Button>();
            botonContinuar.onClick.AddListener(GameManager.instancia.ReanudarJuego);
        }
        else Debug.LogError("GameManager no encontrado");
    }
}
