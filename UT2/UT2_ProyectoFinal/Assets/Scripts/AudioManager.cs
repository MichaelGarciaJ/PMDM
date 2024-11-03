using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource;
    public static AudioManager instancia;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void reproducirSonido(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
