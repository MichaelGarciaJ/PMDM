using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public void cambiarEscena(string nombreEscena){
        SceneManager.LoadScene(nombreEscena);
    }
}
