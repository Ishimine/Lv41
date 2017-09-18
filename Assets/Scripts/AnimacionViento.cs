using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionViento : MonoBehaviour {

    public bool prendido = false;
    public SpriteRenderer render;
    public Color colorPrendido;
    public Color colorApagado;
    public float t = 0f;
    float velPrendido = 4f;
    float velApagado = 1.6f;


    private void Update()
    {
        if ((t > 1 && prendido) || (t < 0 && !prendido))
            return;
        if (prendido)
        {
            t += Time.deltaTime * velPrendido;

        }
        else
        {
            t -= Time.deltaTime * velApagado;
        }
        render.color = Color.Lerp(colorApagado, colorPrendido, t);

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        prendido = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        prendido = false;

    }

    /*
    IEnumerator Prender()
    {
        t = 0;
        while (render.color != colorPrendido)
        {
            t += Time.deltaTime;
            render.color = Color.Lerp(colorApagado, colorPrendido, t);            
            yield return null;
        }
    }

    IEnumerator Apagar()
    {
        t = 0;
        while (render.color != colorApagado)
        {
            t += Time.deltaTime;
            render.color = Color.Lerp(colorPrendido, colorApagado, t);
            yield return null;
        }
    }*/
}
