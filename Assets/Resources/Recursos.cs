using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Recursos : MonoBehaviour
{

    public static Recursos instance;

    public enum idioma { Castellano, Ingles };
    public enum Text { niveles, salir };

    public delegate void Actualizacion(Color[] c);
    public static Actualizacion cambioColor;


    public int colorActual = 1;
    public int ColorActual
    {
        set
        {
            colorActual = value;
            cambioColor(setColorActual.GetPaleta());
        }
        get { return colorActual; }
    }

    [SerializeField] public string nombreAct;
    [SerializeField] public Paleta setColorActual;

    [SerializeField] public Paleta[] sets;
    

    public static idioma IdiomaActual = idioma.Castellano;

    static string[] niveles = new string[] {"Niveles","Levels"};

    static string[] salir = new string[] { "Salir", "Quit" };



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SelectorNivel.NivelCargado += NivelCargado;
        }
    }

    void NivelCargado()
    {
        cambioColor = null;
        AplicarSet();
    }


    private void Start()
    {
        AplicarSet();
    }

    void AplicarSet()
    {
        for (int i = 0; i< setColorActual.color.Length; i++)
        {
            setColorActual = sets[colorActual];
        }
        if(cambioColor != null) cambioColor(setColorActual.GetPaleta());

        nombreAct = sets[colorActual].Nombre;
        Camera.main.backgroundColor = setColorActual.color[0].tono;
    }

    
    private void OnValidate()
    {
        AplicarSet();
    }
    


    public void ProximaPaleta()
    {
        colorActual++;
        if (colorActual >= sets.Length)
        {
            colorActual = 0;
        }
        AplicarSet();
    }

}
