using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraMovil : MonoBehaviour {

    //public float velMov = 20;
  public  Rigidbody2D rb;
  public  GameObject cuerpo;
  public  Transform posA;
    public Transform posB;
    Vector3 posObj;
    bool ida = true;
    public float smooth = .2f;
    Vector2 dir;
    Vector3 vel;


    private void Awake()
    {
        posObj = posA.position;        
       // AplicarDireccion();
    }

    private void Update()
    {
        float dist = Vector2.Distance(posObj, cuerpo.transform.position);

        if(dist < .1f)
        {
            if(ida)            
                posObj = posB.position;            
            else            
                posObj = posA.position;
            ida = !ida;
        }
        cuerpo.transform.position = Vector3.SmoothDamp(cuerpo.transform.position, posObj,ref vel, smooth, 40, Time.deltaTime);
    }

    /*
    void CalcularDireccion()
    {
        dir = (posObj - cuerpo.transform.position).normalized * velMov; 
    }

    public void AplicarDireccion()
    {
        rb.velocity = (posObj - cuerpo.transform.position).normalized * velMov;
    }*/
    
}
