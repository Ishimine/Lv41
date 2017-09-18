using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonAnimacionContinua : MonoBehaviour {

    public float tamanioObj = 1.5f;
    public float velocidadGiro = 90;
    public float velocidadTamanio = .5f;
    public bool girar = true;
    public bool tamanio = false;

   [SerializeField] public float anguloDeGiro = 90;
    float m;

    private void Awake()
    {
        m = anguloDeGiro / 2;
    }

    void Update ()
    {
        if(girar) transform.rotation = Quaternion.Euler(0f, 0,(Mathf.PingPong(Time.realtimeSinceStartup * velocidadGiro, anguloDeGiro) -m));

        if (tamanio)
        {
            float temp = Mathf.PingPong(Time.realtimeSinceStartup * velocidadTamanio, tamanioObj-1) + 1;
            transform.localScale = Vector3.one * temp;
        }
    }
}
