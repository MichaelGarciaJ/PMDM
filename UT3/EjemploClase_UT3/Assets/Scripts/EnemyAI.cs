using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform jugador;
    public float distanciaParaPerseguir = 100f;
    public float fuerzaSalto = 5f;
    public string obstaculoTag = "salto";
    public float alturaDeteccion = 1.5f;
    public float distanciaDeteccion = 2f;

    private Rigidbody rb;
    private bool enElAire = false;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distanciaAlJugador = Vector3.Distance(jugador.position, transform.position);

        if (distanciaAlJugador <= distanciaParaPerseguir && !enElAire)
        {
            if (DetectarObstaculo())
            {
                Saltar();
            }
            else
            {
                agent.SetDestination(jugador.position);
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    bool DetectarObstaculo()
    {
        RaycastHit hit;
        Vector3 origen = transform.position + Vector3.up * alturaDeteccion;

        // Lanza el Raycast hacia adelante desde la posición ajustada
        Debug.Log("Origen del Raycast: " + origen);
        if (Physics.Raycast(origen, transform.forward, out hit, distanciaDeteccion))
        {
            Debug.DrawRay(origen, transform.forward * distanciaDeteccion, Color.red); // Visualiza el Raycast
            Debug.Log("El Raycast alcanzó: " + hit.collider.name);

            if (hit.collider.CompareTag(obstaculoTag))
            {
                Debug.Log("Obstáculo detectado: " + hit.collider.name);
                return true;
            }
            else
            {
                Debug.Log("El objeto detectado no tiene la etiqueta 'salto': " + hit.collider.tag);
            }
        }
        else
        {
            Debug.Log("El Raycast no alcanzó ningún objeto.");
        }

        return false;
    }


    void Saltar()
    {
        enElAire = true;
        agent.enabled = false; // Desactiva el NavMeshAgent temporalmente
        rb.isKinematic = false; // Permite la física para el salto
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

        Debug.Log("Saltando!");

        StartCoroutine(TerminarSalto());
    }

    IEnumerator TerminarSalto()
    {
        // Espera un tiempo estimado para que el salto termine
        yield return new WaitForSeconds(1f);

        enElAire = false;
        rb.isKinematic = true; // Desactiva la física al aterrizar
        agent.enabled = true; // Reactiva el NavMeshAgent
    }

    // Método para visualizar el Raycast en la escena.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Origen del Raycast ajustado con alturaDeteccion
        Vector3 origen = transform.position + Vector3.up * alturaDeteccion;

        // Dirección del Raycast es hacia adelante
        Vector3 direccion = transform.forward;

        // Dibuja la línea para representar el Raycast
        Gizmos.DrawLine(origen, origen + direccion * distanciaDeteccion);
    }
}
