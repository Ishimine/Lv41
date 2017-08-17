using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonConAnimacion : MonoBehaviour {

    public float tiempoEspera = 1.5f;

    public Animator anim;

    public GameObject[] objsParaActivar;
    public GameObject[] objsParaDesactivar;

    public Animator canvasAnim;
    public string TriggerName;


    bool tocado = false;

    private void Awake()
    {
        tocado = false;
        GetComponent<Button>().interactable = true;
    }

    public void Apretado()
    {
        if (tocado)
            return;
        if(anim != null) anim.SetTrigger("Tocado");
        tocado = true;
        StartCoroutine(Espera());
    }

    private void OnEnable()
    {
        tocado = false;
        if (anim != null) anim.SetTrigger("Aparecer");        
    }


    IEnumerator Espera()
    {
        yield return new WaitForSeconds(tiempoEspera);
        ActivarObjetos();
        DesactivarObjetos();
        if(TriggerName != "" && canvasAnim != null) canvasAnim.SetTrigger(TriggerName);
    }


    void ActivarObjetos()
    {

        foreach(GameObject g in objsParaActivar)
        {

            g.SetActive(true);

            Button b = g.GetComponent<Button>();
            if (b != null) b.interactable = true;

        }
    }

    void DesactivarObjetos()
    {
        foreach (GameObject g in objsParaDesactivar)
        {
            g.SetActive(false);

        }
    }
}
