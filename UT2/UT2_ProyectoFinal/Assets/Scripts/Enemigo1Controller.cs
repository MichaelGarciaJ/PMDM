using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemigo1Controller : MonoBehaviour
{
    public float vida;
    public float distanciaMovimiento;
    public float velocidad;
    public float tiempoQuieto;
    public float knockBackForce;
    public float knockBackDuration;

    private Vector2 posicionInical;
    private bool moviendoIzquierda;
    private float tiempoEsperaActual;
    private Animator animator;
    private bool estaMuerto;


    void Start()
    {
        animator = GetComponent<Animator>();
        posicionInical = transform.position;
        tiempoEsperaActual = tiempoQuieto;
        moviendoIzquierda = true;
    }

    void Update()
    {

        gestionarGiro();

        // Si esta esparando, avanza el temporizador.
        if (tiempoEsperaActual > 0)
        {
            tiempoEsperaActual -= Time.deltaTime;

            return;
        }

        // Define la posición de destino en función de la dirección.
        Vector2 destino = moviendoIzquierda
        ? posicionInical + Vector2.left * distanciaMovimiento
        : posicionInical + Vector2.right * distanciaMovimiento;

        // Mueve el enemigo hacia el destino.
        transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // Si el enemigo ha alcanzado el destino, cambia de dirección y empieza el tiempo de espera.
        if (Vector2.Distance(transform.position, destino) < 0.1f)
        {

            // Cambia de dirección.
            moviendoIzquierda = !moviendoIzquierda;

            // Reinicia el temporizador de espera.
            tiempoEsperaActual = tiempoQuieto;
        }
    }


    // Método en el que quita vida al enemigo.
    public void tomarDano(float dano)
    {

        vida -= dano;
        if (vida <= 0)
        {
            estaMuerto = true;
            StartCoroutine(muerte());
        }
    }

    // Método en el que el enemigo muere, mostrando la animación.
    private IEnumerator muerte()
    {

        animator.SetTrigger("isDead");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    // Método en el que gestionamos el giro del enemigo.
    void gestionarGiro()
    {

        if (tiempoEsperaActual <= 0)
        {
            animator.SetBool("enMovimiento", true);

            if (moviendoIzquierda)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("enMovimiento", false);
        }
    }

    // Método en el que si te chochas con el enemigo te empujara.
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {

                Vector2 collisionNormal = other.contacts[0].normal;

                Vector2 knockBackDirection = -collisionNormal;

                playerRb.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);

                PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
                if (playerController != null)
                {

                    playerController.desactivarMovimiento(knockBackDuration);
                }

                GameManager.instancia.quitarVidaJugador();

            }
        }
    }

}
