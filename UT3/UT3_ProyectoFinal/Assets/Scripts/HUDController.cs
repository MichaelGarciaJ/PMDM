using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI monedas;

    void Start()
    {
        if (GameManager.instancia != null)
        {

            Button botonPausa = GameObject.Find("Pausa").GetComponent<Button>();
            botonPausa.onClick.AddListener(GameManager.instancia.pausarJuego);

            GameObject botonesContenedor = GameObject.Find("--- Botones ---");
            Button botonContinuar = botonesContenedor.transform.Find("Continuar").GetComponent<Button>();
            botonContinuar.onClick.AddListener(GameManager.instancia.renaudarJuego);

        }
        else
        {

            Debug.LogError("GameManger no encontrado");
        }

    }

    void Update()
    {
        monedas.text = GameManager.instancia.Monedas.ToString();
    }
}
