using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Componentes
    Rigidbody2D rb;
    Animator animator;
    BoxCollider2D boxCollider;

    // Customizables
    public float velocidad = 20f;
    public float jumpForce = 25f;
    public float boxCastDistance = 0.1f;
    public float intervaloDisparos = 0.5f;

    // Hay que definir
    public LayerMask groundLayer;
    public GameObject prefabBala;
    public AudioClip recibeDanio;
    public AudioClip saltar;
    public AudioClip disparar;

    // Privadas
    bool puedeMoverse;
    float puedeDisparar;
    bool jugadorVivo;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        puedeMoverse = true;
        puedeDisparar = 0;
        jugadorVivo = true;
    }

    void Update() {
        if (jugadorVivo) {
            float inputMovimiento = Input.GetAxis("Horizontal");
            gestionarGiro(inputMovimiento);
            if (puedeMoverse) gestionarMovimiento(inputMovimiento);
            gestionarSalto();
            gestionarDisparo();
        } else rb.velocity = new Vector2(0,0);
    }

    void gestionarGiro(float inputMovimiento) {
        if (inputMovimiento > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (inputMovimiento < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void gestionarMovimiento(float inputMovimiento) {
        
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);
        animator.SetFloat("inputMovimiento", inputMovimiento);
    }

    public void DesactivarMovimiento(float duration) {
        StartCoroutine(DisableMovementCoroutine(duration));
    }

    private IEnumerator DisableMovementCoroutine(float duration) {
        puedeMoverse = false;
        yield return new WaitForSeconds(duration);
        puedeMoverse = true;
    }

    void gestionarSalto() {
        
        if (boxCollider != null) {
            Vector2 boxSize = new Vector2(boxCollider.size.x, 0.1f);
            Vector2 origin = (Vector2)transform.position + Vector2.down * (boxCollider.size.y / 2 + 0.05f);
            RaycastHit2D hit = Physics2D.BoxCast(origin, boxSize, 0, Vector2.down, boxCastDistance, groundLayer);

            if (hit.collider != null && Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(Saltar());
            }
        }
    }

    private IEnumerator Saltar() {
        animator.SetBool("saltando", true);
        AudioManager.instancia.ReproducirSonido(saltar);
        yield return new WaitForSeconds(0.15f);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("saltando", false);
    }

    void gestionarDisparo() {
        puedeDisparar -= Time.deltaTime;
        if (puedeDisparar <= 0 && Input.GetKeyDown(KeyCode.Mouse0)) {
            DispararBala(DireccionClick());
            AudioManager.instancia.ReproducirSonido(disparar);
            StartCoroutine(ResetDisparando());
            puedeDisparar = intervaloDisparos;
        }
    }

    Vector2 DireccionClick() {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        // Calcula el vector dirección hacia el clic desde la posición del jugador
        Vector2 direccion = clickPosition - transform.position;

        // Calcula el ángulo en grados de la dirección
        float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Clasifica el ángulo en una de las 8 direcciones
        if (angle >= -22.5f && angle < 22.5f)
            return Vector2.right;
        else if (angle >= 22.5f && angle < 67.5f)
            return new Vector2(1, 1);
        else if (angle >= 67.5f && angle < 112.5f)
            return Vector2.up;
        else if (angle >= 112.5f && angle < 157.5f)
            return new Vector2(-1, 1);
        else if ((angle >= 157.5f && angle <= 180f) || (angle >= -180f && angle < -157.5f))
            return Vector2.left;
        else if (angle >= -157.5f && angle < -112.5f)
            return new Vector2(-1, -1);
        else if (angle >= -112.5f && angle < -67.5f)
            return Vector2.down;
        else if (angle >= -67.5f && angle < -22.5f)
            return new Vector2(1, -1);
        return Vector2.zero;
    }

    void DispararBala(Vector2 direccion) {
        gestionarGiro(direccion.x);
        animator.SetFloat("shootX", direccion.x);
        animator.SetFloat("shootY", direccion.y);
        animator.SetBool("disparando", true);
        
        // Calcula el ángulo en grados entre la dirección actual y el eje X
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        
        // Aplica la rotación a la bala
        GameObject bala = Instantiate(prefabBala, PosicionInicialBala(direccion), Quaternion.Euler(0, 0, angulo));
        bala.GetComponent<BalaController>().EstablecerDireccion(direccion);
    }

    Vector3 PosicionInicialBala(Vector2 direccion) {
        Transform posiciones = transform.Find("-- PosicionesDisparos --").transform;
        if (direccion.x == 1  && direccion.y == -1
                || direccion.x == -1  && direccion.y == -1) return posiciones.Find("D").transform.position;
        if (direccion.x == 1  && direccion.y == 0
                || direccion.x == -1  && direccion.y == 0 ) return posiciones.Find("F").transform.position;
        if (direccion.x == 1  && direccion.y == 1
                || direccion.x == -1  && direccion.y == 1 ) return posiciones.Find("FU").transform.position;
        if (direccion.x == 0  && direccion.y == 1 ) return posiciones.Find("U").transform.position;
        return Vector3.zero;
    }

    private IEnumerator ResetDisparando() {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("disparando", false);
    }

    private IEnumerator ResetHerido() {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("herido", false);
    }

    public void JugadorHerido() {
        animator.SetBool("herido", true);
        AudioManager.instancia.ReproducirSonido(recibeDanio);
        StartCoroutine(ResetHerido());
    }
    
    public void JugadorMuerto() {
        animator.SetTrigger("muerto");
        jugadorVivo = false;
    }
}
