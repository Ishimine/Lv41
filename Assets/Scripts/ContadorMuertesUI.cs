using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ContadorMuertesUI : MonoBehaviour {

    Text txt;

    public void Restart()
    {
        txt.text = "0";
    }

    void Awake()
    {
        txt = GetComponent<Text>();
        CheckpointManager.muerte += ActualizarTexto;
        txt.text = "0";

    }


    void ActualizarTexto(int n)
    {
        if (txt != null)
            txt.text = n.ToString();
    }
}
