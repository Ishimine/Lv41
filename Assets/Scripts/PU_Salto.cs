using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class PU_Salto : MonoBehaviour {

    public Sprite icono;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PowerUps pu = col.gameObject.GetComponent<PowerUps>();
            pu.CambiarPowerUp(1);
            pu.icono.CambiarIcono(icono);
            
            //Animar
        }
    }

    public void Animar()
    {

    }

}
