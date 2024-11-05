using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaController : MonoBehaviour
{
    bool puertaAbierta;
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        puertaAbierta = false;
    }

    public void AbrirPuerta() {
        puertaAbierta = true;
        animator.SetTrigger("abrir");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && puertaAbierta) {
            
        }
    }
}
