using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipalUI : MonoBehaviour {

    public GameObject quit;
    public GameObject niveles;
    public GameObject botonesNiveles;
    public GameObject btns;


    public void Reiniciar()
    {
        quit.SetActive(true);
        niveles.SetActive(true);
        botonesNiveles.SetActive(false);
        btns.SetActive(false);
    }
}
