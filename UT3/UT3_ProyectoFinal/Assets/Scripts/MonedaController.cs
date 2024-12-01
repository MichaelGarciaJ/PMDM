using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaController : MonoBehaviour
{
    public int valor = 1;
    private AudioSource audioSource;

   private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instancia.SumarMonedas(valor);
            StartCoroutine(DestruirDespuesDeSonido());
        }
    }

    private IEnumerator DestruirDespuesDeSonido()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); 
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        Destroy(gameObject);
    }
   
}
