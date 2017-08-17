using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerDeNiveles : MonoBehaviour {


    public BotonCargarNivel[] botonCargarNivel;

    public int pagina;

    private void Awake()
    {
        pagina = 0;
        AsignarIndices();
    }


    public void SigPagina()
    {
        if (pagina == 20)
        {
            return;
        }
        pagina += 10;        
        AsignarIndices();
    }

    public void AntPagina()
    {
        if(pagina == 0)
        {
            return;
        }
        pagina -= 10;
        AsignarIndices();
    }


    void AsignarIndices()
    {
        int a = botonCargarNivel.Length + 1;
        for (int i = 1; i < a; i++)
        {
            botonCargarNivel[i-1].indiceNivel = i + pagina;
            Text t = botonCargarNivel[i - 1].GetComponentInChildren<Text>();
            if(t != null) t.text = (i + pagina).ToString();
        }        
    }
}
