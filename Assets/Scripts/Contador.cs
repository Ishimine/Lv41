using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Contador : MonoBehaviour
{
    public enum tipoDeContador {Fraccion, Entero, Tiempo}
    public tipoDeContador TipoDeContador;

    public bool desactivado = false;

    public Text txt;
    public Image img;
    public Image borde;

    public Color cNormal = Color.white;
    public Color cDesactivado = Color.gray;
    public Color cActivado = Color.yellow;
    public Color cInvisible = Color.clear;

    public Rigidbody2D PopTxt;

    public void Iniciar(float act, float recordObjetivo, float recordActual)
    {
        borde.color = cInvisible;
        txt.color = Color.white;
        desactivado = false;
        if (recordActual <= recordObjetivo)
        {
            if (act < recordActual) AplicarTexto(act);
            else  AplicarTexto(recordActual);
            AplicarColorDorado();
        }
        else
        {
            ResetearColor();
            StartCoroutine(AnimacionRecord(act, recordObjetivo));
        }
    }

    void AplicarTexto(float x)
    {
        switch (TipoDeContador)
        {
            case tipoDeContador.Fraccion:
                txt.text = x.ToString("0.0");
                break;
            case tipoDeContador.Entero:
                txt.text = x.ToString("0");
                break;
            case tipoDeContador.Tiempo:
                txt.text = x.ToString("0.00");
                break;
            default:
                break;
        }
    }

    public void ResetearColor()
    {
        txt.color = Color.white;
        img.color = cNormal;
    }


    public void AplicarColorDorado()
    {
        txt.color = cActivado;
        img.color = cActivado;        
    }

    

    IEnumerator AnimacionRecord(float act, float record)
    {
        yield return new WaitForSeconds(1f);
        float x = 0;
        float t = 0;

        while(t < 2)
        {
            t += Time.deltaTime;
            x = Mathf.Lerp(0, act, t / 2);            
            if (!desactivado && x > record)
            {
                txt.color = cDesactivado;
                StartCoroutine(AnimarIconoDesactivado());
                desactivado = true;
                ActivarTextoPop(x.ToString("0"));
            }
            AplicarTexto(x);
            yield return null;
        }
        if (x <= record) StartCoroutine(AnimarIconoActivado());

    //    Debug.Log(gameObject.name + "Valor final de x: " + x);
    }

    void ActivarTextoPop(string t)
    {
        PopTxt.gameObject.transform.localPosition = Vector3.zero;
        Text txt = PopTxt.GetComponent<Text>();
        txt.text = t;
        txt.color = Color.red;
        PopTxt.gameObject.SetActive(true);
        PopTxt.AddForce(Vector2.up*100,ForceMode2D.Impulse);
        StartCoroutine(DesactivarTxtPop(1.5f, txt));
    }

    IEnumerator DesactivarTxtPop(float x, Text txt)
    {
        float t = 0;
        while (t < x)
        {
            txt.color = Color.Lerp(Color.red, new Color(255,0,0,0), t/x);
            t += Time.deltaTime;
            yield return null;
        }
        PopTxt.gameObject.SetActive(false);
    }

    IEnumerator AnimarIconoDesactivado()
    {
        float t = 0;
        Vector3 escalaOrig = img.gameObject.transform.localScale;
        Vector3 escalaObjetivo = escalaOrig * 1.2f;
        float tAux;
        while (t < .7f)
        {
            tAux = t / .7f;
            img.color = Color.Lerp(cNormal, cDesactivado, tAux);

            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AnimarIconoActivado()
    {
        //      Debug.Log("Record roto ahora mismo");
        borde.color = cActivado;
        txt.color = Color.yellow;

        float t = 0;
        Vector3 escalaOrig = img.gameObject.transform.localScale;
        Vector3 escalaObjetivo = escalaOrig * 1.2f;

        float tAux;
        while (t < .7f)
        {
            tAux = t / .7f;

            if (tAux < .8)
            {
                img.gameObject.transform.localScale = Vector3.Lerp(escalaOrig, escalaObjetivo, tAux / .8f);
            }
            else
            {
                img.gameObject.transform.localScale = Vector3.Lerp(escalaObjetivo, escalaOrig, (tAux-.8f) / .2f);
            }
            img.color = Color.Lerp(cNormal, cActivado, tAux);

            t += Time.deltaTime;       
            yield return null;
        }
    }

}
