using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Paleta
{
    public enum tags { Fondo, Barras, Jugador_1, Jugador_2, tXt_1, txt_2, terreno_1, terreno_2, terreno_3 };

    public tags nTags;

    [SerializeField] private string nombre;
    public string Nombre        { get { return nombre; } }
    [SerializeField] public Color_Tag[] color = new Color_Tag[9];



    [System.Serializable]
    public struct Color_Tag
    {
        [SerializeField] public string nombre;
        [SerializeField] public Color tono;
    }

    public Paleta()
    {

        color[0].nombre = "Fondo";
        color[1].nombre = "Barra"; 
        color[2].nombre = "Jugador_1";
        color[3].nombre = "Jugador_2";
        color[4].nombre = "Txt_1";
        color[5].nombre = "Txt_2";
        color[6].nombre = "Terreno_1";
        color[7].nombre = "Terreno_2";
        color[8].nombre = "Terreno_3";

        for(int i=0;i<color.Length;i++)
        {
            color[i].tono = Color.white;
        }
    }

    public Color[] GetPaleta()
    {
        Color[] p = new Color[color.Length];

        for(int i = 0; i < color.Length; i++)
        {
            p[i] = color[i].tono;
        }

        return p;
    }



}
