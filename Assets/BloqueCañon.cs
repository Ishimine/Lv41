using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueCañon : MonoBehaviour {


    public float tiempoTotal;
    public float tiempoPaso;
    public float velocidadproyectil;

    public SpriteRenderer[] marcas;
    public Proyectil_Trampa proyectil;


    private void Awake()
    {
        print("Awake");
        CalcularTiempo();
        AplicarAtributosMunicion();
    }

    private void Start()
    {
        print("Start");

        IniciarSecuencia();
    }

    void AplicarAtributosMunicion()
    {
        proyectil.direccion = this.transform.up;
        proyectil.velocidad = velocidadproyectil;
    }

    void CalcularTiempo()
    {
        tiempoPaso = tiempoTotal / 3;
    }

    void Disparo()
    {
        ResetearMunicion();
    }



    void ResetearMunicion()
    {
        proyectil.gameObject.transform.position = transform.position;
        proyectil.gameObject.SetActive(true);
    }


    void IniciarSecuencia()
    {
     //   print("Secuencia Inicial");
        StartCoroutine(Esperar(tiempoPaso,0));        
    }

    void AnimarPaso(int i)
    {
        //print("Paso " + i);

        marcas[i].gameObject.SetActive(true);
    }

    void AnimarFinal()
    {
       foreach(SpriteRenderer render in marcas)
        {
            render.gameObject.SetActive(false);
        }
    }



    IEnumerator Esperar(float t, int paso)
    {
        yield return new WaitForSeconds(t);
        if(paso < 3)
        {
            AnimarPaso(paso);
            StartCoroutine(Esperar(t,paso+1));
        }
        else
        {
            AnimarFinal();
            Disparo();
            IniciarSecuencia();
        }
    }
}
