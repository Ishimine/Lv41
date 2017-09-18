using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {


    public Vector2 direccion = Vector2.right;
    public float velocidad;
    public LayerMask mascaraImpacto;





    private void Update()
    {
        Avanzar();
    }


    public void Avanzar()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
    
}
