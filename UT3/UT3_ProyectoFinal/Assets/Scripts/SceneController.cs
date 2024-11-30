using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    // Método que te carga la escena que indiques.
    public void cargarScena(string nameScene)
    {

        if (GameManager.instancia.VidasJugador > 0)
        {
            SceneManager.LoadScene(nameScene);
            GameManager.instancia.quitarVidaJugador();
        }
        else
        {
            GameManager.instancia.mostrarPantallaMuerte();
        }


    }

}
