using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{

    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
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
    }
}
