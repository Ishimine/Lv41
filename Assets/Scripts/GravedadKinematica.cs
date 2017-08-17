using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody2D))]
public class GravedadKinematica : MonoBehaviour {

    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (Vector3)Physics2D.gravity*Time.fixedDeltaTime);
    }
}
