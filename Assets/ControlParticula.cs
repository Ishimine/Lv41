using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlParticula : MonoBehaviour {

    public ParticleSystem p;

    private void Awake()
    {
        if (p == null) p = GetComponent<ParticleSystem>();
    }

    public void CambiarCantidad(int x)
    {
        var main = p.emission;
        main.rateOverTime = x;
    }

    public void CambiarColorInicial(Color c)
    {
        var main = p.main;
        main.startColor = c;
    }

    public void CrearBurst(int cant)
    {
        p.Emit(cant);
    }
    public void ActivarEmission()
    {
        p.Play();
    }

    public void DesactivarEmission()
    {
        p.Play();
    }
}
