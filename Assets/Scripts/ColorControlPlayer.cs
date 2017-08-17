using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControlPlayer : MonoBehaviour {

    public SpriteRenderer[] grupoA;
    public SpriteRenderer[] grupoB;
    public TrailRenderer trail;


    public void CambiarColor(int grupo_Id, Color color)
    {
        if (grupo_Id == 0)        
            CambiarColor(grupoA, color);        
        else
            CambiarColor(grupoB, color);
    }

    private void CambiarColor(SpriteRenderer[] grupo, Color color)
    {
        foreach (SpriteRenderer item in grupo)
        {
            if (item != null)
            {
                item.color = color;
                trail.startColor = color;
                //trail.endColor = Color.clear;
            }
        }
    }
}
