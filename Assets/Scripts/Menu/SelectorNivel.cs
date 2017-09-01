using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectorNivel : MonoBehaviour
{
    public Slider slider;
    public Text progresoTxt;
    public GameObject pantallaDeCarga;

    public static SelectorNivel instance;

    public delegate void trigger();

    public static event trigger NivelCargado;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            pantallaDeCarga.SetActive(false);
        }
        else
            Destroy(this.gameObject);            
    }

    void CambioDeEscena()
    {
        if (NivelCargado != null) NivelCargado();
    }

    public static void CargarNivel(int n)
    {
        //Debug.Log(SceneManager.sceneCountInBuildSettings);
        Time.timeScale = 1;
        DirectorGravedad.ReestablecerGravedadInstantaneo();
        DirectorGravedad.LimpiarLista();
        
        if (n < SceneManager.sceneCountInBuildSettings)
        {
            instance.CargarAsincrono(n);
        }
        else
        {
            instance.CargarAsincrono(0);
        }
        
    }

    public void CargarAsincrono(int n)
    {
        pantallaDeCarga.SetActive(true);

        StartCoroutine(CargarNivelAsincrono(n));
    }

    public static void ReiniciarNivel()
    {
        //CargarNivel(SceneManager.GetActiveScene().buildIndex);
        GameController.ReiniciarNivel();
    }


    public static void SiguienteNivel()
    {
        CargarNivel(SceneManager.GetActiveScene().buildIndex+1);
    }


    public static void NivelAnterior()
    {
        CargarNivel(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public IEnumerator CargarNivelAsincrono(int i)
    {
        canvasScript.instance.CargandoEscena();
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);

       

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progresoTxt.text = (progress * 100).ToString("00.0") + "%"; 

            yield return null;
        }
        CambioDeEscena();
        pantallaDeCarga.SetActive(false);

    }

}
