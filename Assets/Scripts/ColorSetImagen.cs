using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ColorSetImagen : MonoBehaviour {


    public bool aplicarHijos = false;
    Image img;
    public Paleta.tags id = Paleta.tags.tXt_1;
    public Color color;

    private void Awake()
    {
        img = GetComponent<Image>();

        Recursos.cambioColor += ActualizarColor;
        PedirColor();
    }

    void PedirColor()
    {
        if (Recursos.instance != null)
            ActualizarColor(Recursos.instance.setColorActual.GetPaleta());
    }



    void ActualizarColor(Color[] c)
    {
        color = c[(int)id];
        AplicarColor();
    }

    public void AplicarColor()
    {
        if (!aplicarHijos)
        {
            if(img !=null) img.color = color;
        }
        else
        {
            Image[] hijos = GetComponentsInChildren<Image>();
            foreach(Image i in hijos)
            {
                i.color = color;
            }

            /*Text[] hijosT = GetComponentsInChildren<Text>();
            foreach (Text i in hijosT)
            {
                i.color = color;
            }*/
        }
    }

    private void OnEnable()
    {
        AplicarColor();

    }

    private void OnDestroy()
    {
        Recursos.cambioColor -= ActualizarColor;
    }

}
