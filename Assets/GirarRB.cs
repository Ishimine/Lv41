using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarRB : MonoBehaviour {

    public float velocidadGiro = 500;

    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        rb.angularVelocity = velocidadGiro;
    }


}
