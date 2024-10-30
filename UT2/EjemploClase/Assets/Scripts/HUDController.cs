using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    public GameManager gameManager;
    public TextMeshProUGUI monedas;

    // Update is called once per frame
    void Update()
    {
     monedas.text = gameManager.Monedas.ToString();   
    }
}
