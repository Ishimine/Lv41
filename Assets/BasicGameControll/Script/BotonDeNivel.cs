using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonDeNivel : MonoBehaviour
{

    public BotonAnimacionContinua animacion;
    public enum estadoNivel {Bloqueado,Desbloqueado,Completado}
    public estadoNivel estado;
    public Image onda;
    public Image img;
    public bool usarTexto;
    public bool usarIndiceParaTexto;
    public Text txt;
    public int indiceNivel = 1;

    public Color colorBloqueado = Color.gray;
    public Color colorCompletado = Color.yellow;
    public Color colorDesbloqueado;

    Button boton;

    float tOnda = .5f;


    private void Start()
    {
        boton = GetComponent<Button>();
        NivelBloqueado();
    }

    private void OnDestroy()
    {        
    }

    public void RevisarEstadoNivel()
    {
        DataDeNivel records = ArbitroNiveles.instance.getDataNivelRecords(indiceNivel);
        if(records.idNivel == -1)
        {
            NivelBloqueado();
            return;
        }
        else if (records.idNivel == 0)
        {
            NivelDesbloqueado();
            return;
        }
        else if (records.idNivel == 1)
        {
            NivelCompletado();
            return;
        }

    }

    public void SetEstado(estadoNivel est)
    {
        estado = est;
        switch (est)
        {
            case estadoNivel.Bloqueado:
                NivelBloqueado();
                break;
            case estadoNivel.Desbloqueado:
                NivelDesbloqueado();
                break;
            case estadoNivel.Completado:
                NivelCompletado();
                break;
        }
    }

    public void CargarNivel()
    {
        SelectorNivel.CargarNivel(indiceNivel);
    }

    public void CargarNivelAnterior()
    {
        SelectorNivel.SiguienteNivel();
    }

    public void CargarNivelSiguiente()
    {
        SelectorNivel.NivelAnterior();
    }

    public void Restart()
    {
        SelectorNivel.ReiniciarNivel();
    }

    public void CargarMenu()
    {
        SelectorNivel.CargarNivel(0);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        CheckText();
    }
#endif

    public void CheckText()
    {
        if (usarTexto)
        {
            if(txt!= null) txt.gameObject.SetActive(true);
            if(usarIndiceParaTexto)
            {
                if (txt != null) txt.text = indiceNivel.ToString();
            }
        }
        else if (txt != null)
            txt.gameObject.SetActive(false);
    }




    public void NivelBloqueado()
    {
        transform.rotation = Quaternion.identity;
        animacion.tamanio = false;

        boton.interactable = false;

        onda.color = new Color(1,1,1,0);
        img.color = colorBloqueado;
        txt.color = Color.black;
    }

    public void NivelDesbloqueado()
    {
        animacion.tamanio = true;
        boton.interactable = true;
        onda.color = colorDesbloqueado;
        img.color = colorDesbloqueado;
        StartCoroutine(FXonda());
        txt.color = Color.white;

    }

    public void NivelCompletado()
    {
        transform.rotation = Quaternion.identity;
        animacion.tamanio = false;
        animacion.girar = false;
        boton.interactable = true;
        onda.color = colorCompletado;
        img.color = colorCompletado;
        StartCoroutine(FXonda());
        txt.color = Color.white;

    }


    IEnumerator FXonda()
    {
        float t = 0;
        Color cOrig = onda.color;
        Color transparente = new Color(cOrig.r, cOrig.g, cOrig.b, 0);
        while (t <= 1)
        {
            onda.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.6f, t);
            if (t < .5f)
            {
                onda.color = Color.Lerp(transparente, cOrig, t);
            }
            else
            {
                onda.color = Color.Lerp(cOrig,transparente, t);
            }
            t += Time.deltaTime / tOnda;
            yield return null;
        }
        onda.color = transparente;
    }
}
