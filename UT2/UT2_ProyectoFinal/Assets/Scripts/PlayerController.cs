using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Se encarga el movimiento de derecha a izquierda. 
        procesarMovimiento_Giro_Animacion();

        // Se encarga el movimiento del salto.
        procesarSalto();

    }

    // Método en el que gestionamos el movimiento y giro del jugador. En nuestro caso lo vamos a hacer con velocity + GetAxis.
    void procesarMovimiento_Giro_Animacion()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);

        //Se encarga de gestionar el giro del jugador.
        gestionarGiro(inputMovimiento);
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);

        //Se encarga de gestionar si el jugador esta quieto o no.
        gestionarMovimiento(inputMovimiento);
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);
    }

    // Método en el que se encarga en el salto del jugador, también tenemos en cuenta la gravedad. 
    //En nuestro caso lo vamos a hacer con Raycast.
    void procesarSalto()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLenght, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (rb.velocity.y < 0) // Se encarga de aumentar la gravedad cuando el jugador esta cayendo.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) //Este caso sale cuando el jugador salta.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // Método en el que gestionamos el giro del jugador.
    void gestionarGiro(float inputMovimiento)
    {

        if (inputMovimiento > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputMovimiento < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Método en el que se verifica si el jugador esta quieto o no (Para la animación).
    void gestionarMovimiento(float inputMovimiento)
    {

        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);

        if (inputMovimiento != 0) // Está en movimiento.
        {
            animator.SetBool("enMovimiento", true);
        }
        else // Este caso sale cuando el jugaor esta quieto.
        {
            animator.SetBool("enMovimiento", false);
        }
    }

    // Método para visualizar el Raycast en la escena.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLenght);
    }

}
