using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{

    public float velocidad;
    public float fuerzaSalto;
    private Rigidbody rb;
    Animator animator;
    public bool isRunning { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        procesarMovimiento();
    }

    // Método en el que gestionamos el movimiento y salto.
    void procesarMovimiento()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = Camera.main.transform.right * horizontal
        + Camera.main.transform.forward * vertical;

        direccionMovimiento.y = 0; // Evita movimiento vertical
        rb.AddForce(direccionMovimiento.normalized * velocidad);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }


        gestionarMovimiento(horizontal, vertical);

    }

    // Método en el que se verifica si el jugador esta quieto o no (Para la animación).
    void gestionarMovimiento(float inputMovimientoH, float inputMovimientoV)
    {

        if (inputMovimientoH != 0 || inputMovimientoV != 0) // Está en movimiento.
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


}
