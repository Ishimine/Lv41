using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pu_Gravedad : MonoBehaviour {

    public Sprite icono;

    public Vector2 dirGrav = Vector2.up;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PowerUps pu = col.gameObject.GetComponent<PowerUps>();
            pu.CambiarPowerUp(2);
            pu.icono.CambiarIcono(icono);
            pu.CambiarDirGravedad(dirGrav);

            //Animar
        }
    }

    public void Animar()
    {

    }
}
