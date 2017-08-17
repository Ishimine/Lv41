using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataDeNivel {

    [SerializeField]
    public int idNivel;
    [SerializeField]
    public int barras;
    [SerializeField]
    public int muertes;
    [SerializeField]
    public float tiempo;


    public DataDeNivel(int i, int barras, int muertes, float tiempo)
    {
        idNivel = i;
        this.barras = barras;
        this.muertes = muertes;
        this.tiempo = tiempo;
    }
    
}
