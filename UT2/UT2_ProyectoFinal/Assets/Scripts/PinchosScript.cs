using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchosScript : MonoBehaviour
{

    public float knockBackForce;
    public float knockBackDuration;

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
