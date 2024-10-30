using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    public Transform jugador;
    float distanciaCamaraAlBorde;
    float distanciaCameraArriba;


    // Start is called before the first frame update
    void Start()
    {

        distanciaCamaraAlBorde = Camera.main.orthographicSize * Camera.main.aspect;

        distanciaCameraArriba = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        if (jugador.position.x > (transform.position.x + distanciaCamaraAlBorde))
        {

            transform.position += new Vector3(distanciaCamaraAlBorde * 2, 0, 0);

        }
        else if (jugador.position.x < (transform.position.x - distanciaCamaraAlBorde))
        {
            transform.position -= new Vector3(distanciaCamaraAlBorde * 2, 0, 0);
        }


        if (jugador.position.y > (transform.position.y + distanciaCameraArriba))
        {

            transform.position += new Vector3(distanciaCamaraAlBorde * 2, 0, 0);

        }
        else if (jugador.position.y < (transform.position.y - distanciaCameraArriba))
        {

            transform.position -= new Vector3(distanciaCamaraAlBorde * 2, 0, 0);
        }
    }
}
