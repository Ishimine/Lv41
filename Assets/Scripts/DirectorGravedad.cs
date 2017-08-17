using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorGravedad : MonoBehaviour
{

    static Vector2 gravedadOriginal = new Vector2(0,-9.81f);
    static float gravedadY = -9.81f;
    static List<BloqueGatilloGravedad> botones = new List<BloqueGatilloGravedad>();
    
    static Vector2 gravedadObjetivo;

    public static DirectorGravedad instance;

    private Vector2 vel;

    public float smooth;
    public float maxVel;

    void Awake()
    {
        if(instance == null)
        {
            gravedadObjetivo = Physics2D.gravity;
            instance = this;
            SceneManager.sceneLoaded += LimpiarLista;
            SelectorNivel.NivelCargado += LimpiarLista;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public static void CambiarGravedad(Vector2 dir)
    {
        if (EsMismaGravedad(dir))
        {            
            ReestablecerGravedad();
        }
        else
        {
            //Debug.Log("Nueva Gravedad: " + dir * gravedadY);
            AplicarNuevaGravedad(dir * gravedadY);
        }
    }

    public static void AplicarNuevaGravedad(Vector2 n)
    {
        gravedadObjetivo = n;
        EncenderBotones();
    }

    private void FixedUpdate()
    {               
        if(gravedadObjetivo != Physics2D.gravity)
        {
           // Debug.Log("Gravedad Actual: " + Physics2D.gravity);
           // Debug.Log("Gravedad Objetivo: " + gravedadObjetivo);
            //Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, gravedadObjetivo, 3f * Time.fixedDeltaTime);

            Physics2D.gravity = Vector2.SmoothDamp(Physics2D.gravity, gravedadObjetivo, ref vel, smooth, maxVel, Time.fixedDeltaTime);

        }
    }

    public static void ReestablecerGravedad()
    {
        gravedadObjetivo = gravedadOriginal;
        ApagarBotones();
    }

    public static void ReestablecerGravedadInstantaneo()
    {
        gravedadObjetivo = gravedadOriginal;
        Physics2D.gravity = gravedadOriginal;
        ApagarBotones();
    }

    public static void AgregarBoton(BloqueGatilloGravedad n)
    {
        botones.Add(n);
    }

    public static void EncenderBotones()
    {
        foreach (BloqueGatilloGravedad item in botones)
        {
            //print(item.name);
            if (EsMismaGravedad (item.direccionGravedad))
                item.Encender();
            else
                item.Apagar();
        }
    }

    public static  bool EsMismaGravedad(Vector2 dir)
    {
        if (dir * gravedadY == gravedadObjetivo)
            return true;
        else
            return false;
    }


    public static void ApagarBotones()
    {
        foreach (BloqueGatilloGravedad item in botones)
        {
            item.Apagar();
        }
    }
    


    public static void LimpiarLista(Scene s, LoadSceneMode ld)
    {
        botones.Clear();
        ReestablecerGravedad();
    }


    public static void LimpiarLista()
    {
        botones.Clear();
        ReestablecerGravedadInstantaneo();
    }





}
