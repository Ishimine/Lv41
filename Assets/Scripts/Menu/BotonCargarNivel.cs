using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonCargarNivel : MonoBehaviour
{
   public enum tipoDeBoton {Normal,Siguiente,Anterior};
    public tipoDeBoton tipo = tipoDeBoton.Normal;

    public int indiceNivel;



    private void OnEnable()
    {
        indiceNivel = SceneManager.GetActiveScene().buildIndex;

        if (tipo == tipoDeBoton.Siguiente)
            indiceNivel++;
        else if (tipo == tipoDeBoton.Anterior)
            indiceNivel--;
        if (tipo != tipoDeBoton.Normal) RevisarEstado();
    }

    public void RevisarEstado()
    {
        if (ArbitroNiveles.GetEstadoNivel(indiceNivel) == -1)
            GetComponent<Button>().interactable = false;
        else
            GetComponent<Button>().interactable = true;
    }
  


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
        GameController.ReiniciarNivel();
    }
}
