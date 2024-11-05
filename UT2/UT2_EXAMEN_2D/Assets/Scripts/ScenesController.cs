using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesController : MonoBehaviour
{
    void Start() {
        Button jugar = GameObject.Find("Jugar").gameObject.GetComponent<Button>();
        Button salir = GameObject.Find("Salir").gameObject.GetComponent<Button>();

        jugar.onClick.AddListener(() => GameManager.instancia.CambiarEscena("Nivel_1"));
        salir.onClick.AddListener(GameManager.instancia.SalirJuego);
    }
}
