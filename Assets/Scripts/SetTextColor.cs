using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextColor : MonoBehaviour {

    Text txt;
    public Paleta.tags id = Paleta.tags.tXt_1;

    private void Awake()
    {
        txt = GetComponent<Text>();

        Recursos.cambioColor += ActualizarColor;
        PedirColor();
    }

    public void PedirColor()
    {
        if (Recursos.instance != null)
            ActualizarColor(Recursos.instance.setColorActual.GetPaleta());
    }



    void ActualizarColor(Color[] c)
    {
        if(txt != null)
            txt.color = c[(int)id];
    }

    private void OnDestroy()
    {
        Recursos.cambioColor -= ActualizarColor;
    }

    private void OnEnable()
    {
        PedirColor();
    }
}
