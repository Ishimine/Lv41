using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public int CheckID;
    public bool activado = false;

    public Animator anim;

    public Vector3 direccion;



    void PuntoActivado()
    {
        //GAtillar animacion de activacio
        anim.SetBool("Activado", true);
        activado = true;
        ActivarColor();
    }

    public void PuntoDesactivado()
    {
        //GAtillar animacion de activacio
        anim.SetBool("Activado", false);
        activado = false;
        DesactivarColor();
    }

    void DesactivarColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void ActivarColor()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }


    public void Update()
    {
        if(activado)
            transform.Rotate(direccion * Time.deltaTime);
    }

    private void OnValidate()
    {
        name = "CP N°" + CheckID;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            CheckpointManager.ActualizarPunto(CheckID);
            PuntoActivado();
        }
    }

}
