using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ContadorUI : MonoBehaviour {

    Text txt;

    public void Restart()
    {
        txt.text = "0.00";
    }

	void Awake()
    {
        txt = GetComponent<Text>();
        FindObjectOfType<GameController>().actTiempo += ActualizarTexto;
	}
	
	
	void ActualizarTexto (float n)
    {
        if(txt != null)
            txt.text = n.ToString("0.00");
    }
}
