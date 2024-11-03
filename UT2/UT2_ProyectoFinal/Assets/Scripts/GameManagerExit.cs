using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerExit : MonoBehaviour
{

    // Método que se encarga de salir del juego.
    public void salirJuego()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
            Application.Quit().
        
#endif
    }
}
