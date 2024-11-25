using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{

    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    private Rigidbody rb;
    Animator animator;
    public bool isRunning { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        procesarMovimiento();
    }

    // Método en el que gestionamos el movimiento.
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

        gestionarMovimiento(horizontal);

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


}
