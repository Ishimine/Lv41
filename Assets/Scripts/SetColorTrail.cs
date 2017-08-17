using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorTrail : MonoBehaviour {


    public TrailRenderer trail;
    public Paleta.tags idColor = Paleta.tags.Jugador_1;



    private void Awake()
    {        
        Recursos.cambioColor += ActualizarColor;
       // PedirColor();
    }


    private void Start()
    {
        if (trail.startColor != Color.green)
            trail.startColor = Color.green;
    }


    void PedirColor()
    {
        if (Recursos.instance != null)
            ActualizarColor(Recursos.instance.setColorActual.GetPaleta());
    }
    

    void ActualizarColor(Color[] c)
    {           
        trail.startColor = Color.green; 
    }

    private void OnDestroy()
    {
        Recursos.cambioColor -= ActualizarColor;
    }

    private void OnEnable()
    {
        PedirColor();
    }
}
