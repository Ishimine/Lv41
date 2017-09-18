using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CartelRecord : MonoBehaviour {

    public float tiempo = 1.5f;
    public Color color = Color.yellow;

    public Image img;
    public Text txt;


    public void Awake()
    {
        GameController.PreSesionDeJuego += InOutNormal;
        //img.gameObject.SetActive(false);
        //txt.gameObject.SetActive(false);
    }





    public void InOutNormal()
    {
        transform.parent.gameObject.SetActive(true);
        img.gameObject.SetActive(true);
        txt.gameObject.SetActive(true);
        FadeIn();
        Esperar(1f);
        FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(img.color,new Color(1, 0.92f, 0.016f, 0),tiempo));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(img.color, color, tiempo));
    }

    IEnumerator Fade(Color cInicial, Color cFinal, float tiempo)
    {
        float t = 0;
        while(t < 1)
        {
            txt.color = Color.Lerp(cInicial,cFinal,t);
            img.color = Color.Lerp(cInicial, cFinal, t);
            t += Time.deltaTime / tiempo;
            yield return null;
        }
    }
    
    IEnumerator Esperar(float x)
    {
        yield return new WaitForSeconds(x);
    }
}
