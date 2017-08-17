using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonDeNivel : MonoBehaviour {

    public bool usarTexto;
    public bool usarIndiceParaTexto;
    public Text txt;
    public int indiceNivel = 1; 


    public void CargarNivel()
    {
        SelectorNivel.CargarNivel(indiceNivel);
    }

    public void CargarNivelAnterior()
    {
        SelectorNivel.SiguienteNivel();
    }

    public void CargarNivelSiguiente()
    {
        SelectorNivel.NivelAnterior();
    }

    public void Restart()
    {
        SelectorNivel.ReiniciarNivel();
    }

    public void CargarMenu()
    {
        SelectorNivel.CargarNivel(0);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        CheckText();
    }
#endif

    public void CheckText()
    {
        if (usarTexto)
        {
            if(txt!= null) txt.gameObject.SetActive(true);
            if(usarIndiceParaTexto)
            {
                if (txt != null) txt.text = indiceNivel.ToString();
            }
        }
        else if (txt != null)
            txt.gameObject.SetActive(false);
    }

}
