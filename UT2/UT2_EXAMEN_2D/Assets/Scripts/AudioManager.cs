using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioManager instancia;
    void Awake() {
        if (instancia == null) {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            // Si ya existe una instancia, destruye el duplicado
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip audio) {
        audioSource.PlayOneShot(audio);
    }
}
