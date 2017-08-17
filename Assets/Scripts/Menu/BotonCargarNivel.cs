using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonCargarNivel : MonoBehaviour {

    public int indiceNivel;

    public void SiguienteNivel()
    {
        SelectorNivel.SiguienteNivel();
    }

    public void NivelAnterior()
    {
        SelectorNivel.NivelAnterior();
    }

    public void CargarNivel ()
    {
        SelectorNivel.CargarNivel(indiceNivel);
	}

    public void ReiniciarNivel()
    {
        SelectorNivel.ReiniciarNivel();
    }
}
