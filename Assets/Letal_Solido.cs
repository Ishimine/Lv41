using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letal_Solido : MonoBehaviour {
    

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            print("Jugador");
            other.gameObject.GetComponent<EsferaJugador>().MuerteExplosiva();
        }
    }
}
