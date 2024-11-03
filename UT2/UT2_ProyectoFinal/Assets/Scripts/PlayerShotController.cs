using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerShotController : MonoBehaviour
{
    public GameObject bala;
    public Transform controladorDisparo;
    public float tiempoDisparo;
    public AudioClip sonidoDisparo;
    private float ultimoDisparo;
    Animator animator;
    private PlayerController playerController; 

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>(); 
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerController.isGrounded && playerController.isRunning) // Verifica si está en el suelo y no corriendo
        {
            disparar();
        }
    }

    // Método en el que genera la bala donde este la posición del objeto que contiene el disparo.
    // Tambíen la animación del jugador a la hora de disparar.
    private void disparar()
    {
        if (Time.time > ultimoDisparo + tiempoDisparo)
        {
            animator.SetTrigger("shoot");
            AudioManager.instancia.reproducirSonido(sonidoDisparo);
            Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);

            ultimoDisparo = Time.time;
        }
    }

}
