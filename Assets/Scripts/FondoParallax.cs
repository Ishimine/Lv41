using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoParallax : MonoBehaviour {

    Camera cam;


    Vector3 camPosAnt;
    Vector3 camPosAct;


    public float indiceDesplazamiento;
    Vector3 desplazamiento;




    private void Start()
    {
        cam = Camera.main;
        camPosAct = cam.transform.position;
        camPosAnt = camPosAct;
    }

    
    private void LateUpdate()
    {
        camPosAct = cam.transform.position;
        CalcularDesplazamiento();
        AplicarDesplazamiento();
        camPosAnt = camPosAct;
    }




    /// <summary>
    /// Recibe el desplazamiento de la camara y calcula el propio desplazamiento en relacion a la distancia con la camara principal y el indice de desplazamiento
    /// </summary>
    /// <param name="desplazamientoDeCamara"></param>
    void CalcularDesplazamiento()
    {
        Vector3 desplazamientoDeCamara = camPosAct - camPosAnt;
        desplazamiento = desplazamientoDeCamara * indiceDesplazamiento;
    }

    void AplicarDesplazamiento()
    {
        transform.Translate(desplazamiento);
    }

}
