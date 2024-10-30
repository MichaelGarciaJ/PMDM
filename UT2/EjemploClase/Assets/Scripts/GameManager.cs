using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int monedas;
    public int Monedas{ get {return monedas;}}
    private int vidasJugador = 3;
    public int VidasJugador{get{return vidasJugador;}}

    public void SumarMonedas(int monedasSumar){

        monedas += monedasSumar;
        Debug.Log("Monedas totales: " + monedas);
    }

    public void QuitarVidaJugador(){
        vidasJugador--;
        if(vidasJugador <= 0){
            Debug.Log("TODO - JUGADOR MUERTO");
        }
    }
}
