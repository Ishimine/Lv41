using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contenedorDeTerreno : MonoBehaviour {

    

	public void EliminarContenedor()
    {
        int i = transform.childCount-1;


        for (int t = -1; i > t ;--i)
        {
            transform.GetChild(i).SetParent(transform.parent);
        }
        DestroyImmediate(this.gameObject);
    }
}
