using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorFantasmasController : MonoBehaviour
{
    public GameObject fantasmaPrefab;
    public GameObject nubeDeHumoPrefab;
    public float tiempoGeneracionFantasma = 10;

    float cooldownGenerarFantasma;

    void Start() {
        cooldownGenerarFantasma = 3;
    }

    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bala")) {
            Instantiate(nubeDeHumoPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
