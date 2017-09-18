using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_Trampa : Proyectil {

    public BloqueCañon canion;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        print(other.name);
        if (mascaraImpacto == (mascaraImpacto | (1 << other.gameObject.layer)))
        {
            print("En Mascara");

            if (other.gameObject.tag == "Player")
            {
            print("Jugador");
                other.gameObject.GetComponent<EsferaJugador>().MuerteExplosiva();
            }
            gameObject.SetActive(false);            
        }
    }
}
