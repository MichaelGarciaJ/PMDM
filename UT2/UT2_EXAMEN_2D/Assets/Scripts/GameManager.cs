using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public float tiempoInvulnerableTrasSufrir = 0.5f;
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    
    private int vidasTotales = 5;
    public int VidasTotales{ get { return vidasTotales;} }

    private int vidasJugador;
    public int VidasJugador{ get { return vidasJugador; }}

    GameObject botonPausa;
    GameObject botonContinuar;
    float cooldownInvulnerable;
    Image[] corazones;

    void Start() {
        cooldownInvulnerable = 0;
    }

    void Update() {
        cooldownInvulnerable -= Time.deltaTime;
    }

    /*******************************************************/
    /*******************************************************/
    /*********************   JUGADOR   *********************/
    /*******************************************************/
    /*******************************************************/
    public void QuitarVidaJugador() {
        if (cooldownInvulnerable <= 0) {
            vidasJugador--;
            
            ActualizarCorazones();
            cooldownInvulnerable = tiempoInvulnerableTrasSufrir;
            if (vidasJugador <= 0) {
                GameOver();
            } else {
                PlayerController playerController = GameObject.Find("--- Player ---").GetComponent<PlayerController>();
                playerController.DesactivarMovimiento(0.5f);
                playerController.JugadorHerido();
            }
        }
    }

    void ActualizarCorazones() {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidasJugador)
                corazones[i].sprite = corazonLleno;
            else
                corazones[i].sprite = corazonVacio;
        }
    }

    void AsignarCorazones() {
        GameObject[] corazonesObjetos = GameObject.FindGameObjectsWithTag("Corazon");
        
        corazones = new Image[corazonesObjetos.Length];
        for (int i = 0; i < corazonesObjetos.Length; i++) {
            corazones[i] = corazonesObjetos[i].GetComponent<Image>();
        }
    }

    /*******************************************************/
    /*******************************************************/
    /*********************   ESCENAS   *********************/
    /*******************************************************/
    /*******************************************************/
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Nivel_1") {
            vidasJugador = vidasTotales;
            AsignarCorazones();
            ActualizarCorazones();

            GameObject botonesContenedor = GameObject.Find("-- Botones --");
            botonPausa = botonesContenedor.transform.Find("Pausa").gameObject;
            botonContinuar = botonesContenedor.transform.Find("Continuar").gameObject;

        }
    }

    public void CambiarEscena(string nombre) {
        SceneManager.LoadScene(nombre);
    }

    public void PausarJuego() {
        botonPausa.SetActive(false);
        botonContinuar.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReanudarJuego() {
        botonPausa.SetActive(true);
        botonContinuar.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReiniciarEscena() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirJuego() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void GameOver() {
        botonPausa.SetActive(false);
        botonContinuar.SetActive(false);
        Time.timeScale = 0.25f;
        PlayerController playerController = GameObject.Find("--- Player ---").GetComponent<PlayerController>();
        playerController.JugadorMuerto();

        GameObject hud = GameObject.Find("--- HUD ---");
        GameObject panelMuerte = hud.transform.Find("Muerte").gameObject;
        panelMuerte.SetActive(true);
        StartCoroutine(MostrarGameOver());
    }

    private IEnumerator MostrarGameOver() {
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game_Over");
    }

    /*******************************************************/
    /*******************************************************/
    /********************   SINGLETON   ********************/
    /*******************************************************/
    /*******************************************************/
    void Awake() {
        if (instancia == null) {
            instancia = this;
            // Esto hace que el GameManager no se destruya al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else {
            // Si ya existe una instancia, destruye el duplicado
            Destroy(gameObject);
        }
    }
}
