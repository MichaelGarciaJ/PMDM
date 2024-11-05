using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveController : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            GameObject hud = GameObject.Find("--- HUD ---");
            GameObject llave = hud.transform.Find("Llave").gameObject;
            llave.SetActive(true);

            GameObject puerta = GameObject.Find("Puerta");
            puerta.GetComponent<PuertaController>().AbrirPuerta();
            Destroy(gameObject);
        }
    }
}
