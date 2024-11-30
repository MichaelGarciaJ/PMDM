using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirSonidoScript : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Método que se llamará cuando un objeto colisione con otro.
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
    
}
