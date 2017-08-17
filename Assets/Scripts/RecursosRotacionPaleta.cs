using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecursosRotacionPaleta : MonoBehaviour {

    public Text text;



	public void ProximaPaleta()
    {
        if (Recursos.instance == null)
            return;
        Recursos.instance.ProximaPaleta();

        if (Recursos.instance.nombreAct != "")
            text.text = Recursos.instance.nombreAct;
        else
            text.text = Recursos.instance.colorActual.ToString();
    }
}
