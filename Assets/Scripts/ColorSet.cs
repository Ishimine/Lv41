using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorSet : MonoBehaviour {

    SpriteRenderer render;
    SpriteRenderer[] renders;


    public Paleta.tags id;

    public Color colorActual;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        if (render == null)
            renders = GetComponentsInChildren<SpriteRenderer>();
        Recursos.cambioColor += ActualizarColor;
        PedirColor();
    }
    

    void ActualizarColor(Color[] c)
    {
        if ((int)id < c.Length)
        {
            if (render != null)
                render.color = c[(int)id];
            else if(renders != null)
            {
                foreach (SpriteRenderer r in renders)
                {
                    r.color = c[(int)id];
                }
            }
        }
        else
            Debug.Log("ID de color invalido");
        colorActual = c[(int)id];
    }


    void PedirColor()
    {
        ActualizarColor(Recursos.instance.setColorActual.GetPaleta());
    }


#if UNITY_EDITOR
   /* private void OnValidate()
    {
        if(Recursos.instance != null)
            PedirColor();
    }*/
#endif

    private void OnDestroy()
    {
        Recursos.cambioColor -= ActualizarColor;

    }

    private void OnEnable()
    {
        PedirColor();
    }
}
