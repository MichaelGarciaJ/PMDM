using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int monedas;
    public int Monedas { get { return monedas; } }
    private int vidasJugador;
    public int VidasJugador { get { return vidasJugador; } }
    private int vidasTotales = 3;
    public int VidasTotales { get { return vidasTotales; } }
    public static GameManager instancia { get; private set; }
    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    GameObject botonPausa;
    GameObject botonContinuar;


    void Start()
    {
        vidasJugador = vidasTotales;
    }

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Nivel_1" || scene.name == "Nivel_2")
        {
            asignarCorazones();
            actualizarCorazones();

            GameObject botonesContenedor = GameObject.Find("--- Botonoes ---");
            botonPausa = botonesContenedor.transform.Find("Pausa").gameObject;
            botonContinuar = botonesContenedor.transform.Find("Continuar").gameObject;
        }
    }

    // Método en el que se encarga de pausar el juego.
    public void pausarJuego(){

        botonPausa.SetActive(false);
        botonContinuar.SetActive(true);
        Time.timeScale = 0f;
    }

    // Método en el que se encarga de renaudar el juego.
    public void renaudarJuego(){

        botonPausa.SetActive(true);
        botonContinuar.SetActive(false);
        Time.timeScale = 1f;
    }

    // Método en el que se encarga de reiniciar la escena.
    public void reiniciarEscena(){

        Time.timeScale = 1;

        vidasJugador = vidasTotales;
        monedas = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Método que se encarga de salir del juego.
    public void salirJuego() {

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else 
            Application.Quit().
        
        #endif
    }

    // Método que suma las monedas que recoge.
    public void SumarMonedas(int monedasAsumar)
    {
        monedas += monedasAsumar;

        if(SceneManager.GetActiveScene().name == "Nivel_1" && monedas == 10){

            SceneManager.LoadScene("Nivel_2");
        }
    }

    // Método que quita vida al jugador;
    public void quitarVidaJugador()
    {

        vidasJugador--;
        actualizarCorazones();

        if (vidasJugador <= 0)
        {
            GameObject hud = GameObject.Find("--- HUD ---");
            GameObject panelMuerte = hud.transform.Find("Muerte").gameObject;
            panelMuerte.SetActive(true);

            // Asocio las funciones Reiniciar y Salir a cada bóton.
            Button reiniciar = panelMuerte.transform.Find("Reiniciar").GetComponent<Button>();
            Button salir = panelMuerte.transform.Find("Salir").GetComponent<Button>();
            reiniciar.onClick.AddListener(GameManager.instancia.reiniciarEscena);
            salir.onClick.AddListener(GameManager.instancia.salirJuego);

            Time.timeScale = 0f;

            // Desactivamos los botones de Pausa y Continuar.
            botonPausa.SetActive(false);
            botonContinuar.SetActive(false);

        }
    }

    // Método que actualiza los corazones del jugador.
    void actualizarCorazones()
    {

        for (int i = 0; i < corazones.Length; i++)
        {

            if (i < vidasJugador)
            {
                corazones[i].sprite = corazonLleno;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }
        }
    }

    // Método en el que se asigna los corazones.
    void asignarCorazones(){

        GameObject[] corazonesObjetos = GameObject.FindGameObjectsWithTag("Corazon");

        corazones = new Image[corazonesObjetos.Length];

        for (int i = 0; i < corazonesObjetos.Length; i++)
        {
            corazones[i] = corazonesObjetos[i].GetComponent<Image>();
        }

    }

    // Método que te carga la escena que indiques.
    public void cambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
