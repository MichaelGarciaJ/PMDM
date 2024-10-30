using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class TestGameController : MonoBehaviour
{

    public void cambiarNivel(String nivel)
    {
        SceneManager.LoadScene(nivel);
    }
}
