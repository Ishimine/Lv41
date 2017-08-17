using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorDeNiveles {


    public SelectorDeNiveles() { }

    public delegate void Trigger();
    public Trigger NivelCargado;

    void Awake()
    {
        SceneManager.sceneLoaded += Cargado;
    }

    void Cargado(Scene s, LoadSceneMode mod)
    {
        if (NivelCargado != null) NivelCargado();
    }

    public static void CargarNivel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public static void CargarNivel(string t)
    {
        SceneManager.LoadScene(t);
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Cargara la escena cuyo nombre sea menu
    /// </summary>
    public static void CargarMenu()
    {
        CargarNivel("Menu");
    }

    public static bool CargarNivelAnterior()
    {
        int i = SceneManager.GetActiveScene().buildIndex - 1;
        if(i < -1)
        {
            CargarNivel(i);
            return true;
        }
        else
        {
            Debug.Log("Intentando cargar nivel con ID menor a 0");
            return false;
        }
    }

    public static bool CargarNivelSiguiente()
    {
        int i = SceneManager.GetActiveScene().buildIndex + 1;
        if(i >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Intentando cargar nivel con ID mayor a las IDs existentes");
            return false;
        }
        else
        {
            CargarNivel(i);
            return true;
        }
    }



}
