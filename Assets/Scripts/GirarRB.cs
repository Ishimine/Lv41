using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarRB : MonoBehaviour {

    public bool invertir;
    public float velocidadGiro = 180;


    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(invertir)
            rb.angularVelocity = velocidadGiro;
        else
            rb.angularVelocity = -velocidadGiro;
    }



    public void FixedUpdate()
    {
    }


}
