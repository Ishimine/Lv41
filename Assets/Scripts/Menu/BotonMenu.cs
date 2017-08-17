using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonMenu : MonoBehaviour {

	


	public void irMenuPrincipal()
    {
        Debug.Log("Menu Principal");

        SelectorNivel.CargarNivel(0);
    }
}
