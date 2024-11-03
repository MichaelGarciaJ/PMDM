using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public float velocidad;
    public float jumpForce;
    public LayerMask groundLayer;
    public float rayLenght;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    Animator animator;
    public UnityEvent loadNewScene;
    private bool puedeMoverse = true;
    public bool isGrounded { get; private set; }
    public bool isRunning { get; private set; }
    private float velocidadOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocidadOriginal = velocidad;
    }

    void Update()
    {
        if (puedeMoverse)
        {
            // Se encarga el movimiento de derecha a izquierda. 
            procesarMovimiento_Giro_Animacion();

            // Se encarga el movimiento del salto.
            procesarSalto();
        }

    }

    // Método en el que gestionamos el movimiento y giro del jugador. En nuestro caso lo vamos a hacer con velocity + GetAxis.
    void procesarMovimiento_Giro_Animacion()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);

        //Se encarga de gestionar el giro del jugador.
        gestionarGiro(inputMovimiento);

        //Se encarga de gestionar si el jugador esta quieto o no.
        gestionarMovimiento(inputMovimiento);
    }

    // Método en el que se encarga en el salto del jugador y su animación, también tenemos en cuenta la gravedad. 
    //En nuestro caso lo vamos a hacer con Raycast.
    void procesarSalto()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLenght, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isGrounded", false); // Activa la animación de salto
            isGrounded = false;
        }

        if (rb.velocity.y < 0) // Se encarga de aumentar la gravedad cuando el jugador esta cayendo.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) //Este caso sale cuando el jugador salta.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Si el Raycast detecta el suelo, indica que ha aterrizado
        if (hit.collider != null && rb.velocity.y <= 0)
        {
            animator.SetBool("isGrounded", true); // Desactiva la animación de salto al tocar el suelo
            isGrounded = true;
        }

    }

    // Método en el que gestionamos el giro del jugador con sus objetos que tenga.
    void gestionarGiro(float inputMovimiento)
    {

        if (inputMovimiento > 0)
        {
            transform.eulerAngles = new Vector3(1, 1, 1);
        }
        else if (inputMovimiento < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    // Método en el que se verifica si el jugador esta quieto o no (Para la animación).
    void gestionarMovimiento(float inputMovimiento)
    {

        if (inputMovimiento != 0) // Está en movimiento.
        {
            animator.SetBool("enMovimiento", true);
            isRunning = false;
        }
        else // Este caso sale cuando el jugaor esta quieto.
        {
            animator.SetBool("enMovimiento", false);
            isRunning = true;
        }

    }

    // Método que desactiva el movimiento por una cantidad de tiempo.
    public void desactivarMovimiento(float duration)
    {

        StartCoroutine(disableMovementCoroutine(duration));
    }

    // Método que corutina para desativar el movimiento y volverlo a habilitarlo.
    public IEnumerator disableMovementCoroutine(float duration)
    {

        puedeMoverse = false;

        // Espeara el tiempo del knockback
        yield return new WaitForSeconds(duration);
        puedeMoverse = true;
    }

    // Método que si tocas el objeto Collider2D saldrá la animación de muerte.
    // Tambíen si toca el arbutos el jugador ira más lento.
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Vacio_Muerte"))
        {

            animator.SetTrigger("Robot1_Dead");
        }


        if (other.gameObject.CompareTag("Arbusto"))
        {
            velocidad *= 0.5f;
        }

    }

    // Método en el que cuando salga fuera del collider restaura su velocidad.
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Arbusto"))
        {
            velocidad = velocidadOriginal;
        }
    }

    // Método que te carga la escena / muerte.
    public void ldScene()
    {

        loadNewScene.Invoke();
    }

    // Método para visualizar el Raycast en la escena.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLenght);
    }

}
