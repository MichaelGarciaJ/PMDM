using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumoController : MonoBehaviour
{
    void Start() {
        StartCoroutine(Destruir());
    }

    private IEnumerator Destruir() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
