using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class GameController : MonoBehaviour {


    public GameObject player;
    public GameObject meta;
    public Transform pPartida;
    public Transform pMeta;
    public static GameController instance;

    public static float tiempo;

    static public bool enPausa;
    static public bool inGame;
    static public bool enMenu;
    static public bool swipeActivo = false;
    TimeSpan timeS;


    public delegate void actFloat(float tiempo);
    public event actFloat actTiempo;


    public delegate void EstadoDeJuego(bool x);
    public static event EstadoDeJuego pausado;

    public delegate void Gatillo();
    public static event Gatillo FinSesionDeJuego;
    public static event Gatillo InicioSesionDeJuego;
    public static event Gatillo PostFinSesionDeJuego;
    // public static Gatillo pFinal;    
    public static event Gatillo PreSesionDeJuego;


    static bool cancelShowngTooltip;

    public static float getTiempo()
    {
        return tiempo;
    }

    void Awake ()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);

            instance = this;
            //pFinal = null;
            FinSesionDeJuego = null;
            PreSesionDeJuego = null;
            actTiempo = null;
            pausado = null;
            this.gameObject.SetActive(true);
            SceneManager.sceneLoaded += NivelCargado;
            FinSesionDeJuego += ActivarPantallaFinDeNivel;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void NivelCargado()
    {
        NivelCargado(SceneManager.GetActiveScene(),LoadSceneMode.Additive);
    }

    void NivelCargado(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            canvasScript.instance.Home();
            enMenu = true;
        }
        else
        {
            canvasScript.instance.EnJuego();
            enMenu = false;
            pMeta = GameObject.FindGameObjectWithTag("pMeta").transform;
            pPartida = GameObject.FindGameObjectWithTag("pPartida").transform;

            if ((pMeta == null) || (pPartida == null))
            {
                Debug.LogError("pMeta/pPartida NO EXISTE");
                SelectorNivel.CargarNivel(0);
            }
            this.gameObject.SetActive(true);
            InicializarNivel();
        }

    }
    

        public static void IniciarSesionDeJuego()
    {
        if (InicioSesionDeJuego != null) InicioSesionDeJuego();
        Time.timeScale = 1;
        inGame = true;
    }

  

    void Update()
    {
        if (inGame && !enPausa)
        {
            tiempo += Time.deltaTime;
            if (actTiempo != null) actTiempo(tiempo);
        }
    }

    IEnumerator CuentaRegresivaPreSesionDeJuego()
    {

        yield return new WaitForSecondsRealtime(3.5f);
        IniciarSesionDeJuego();
    }
       



    /// <summary>
    /// Crea la "Meta" en el punto designado al igual que spawnea al jugador en el punto de partida designado
    /// </summary>
    void CrearPartidaMeta()
    {
        GameObject p = Instantiate<GameObject>(player, pPartida);
        PreSesionDeJuego += p.GetComponent<EsferaJugador>().cuenta.IniciarCuentaRegresiva;
        Instantiate<GameObject>(meta, pMeta);
        Camera.main.transform.position = p.transform.position;
    }


    void InicializarNivel()
    {
        swipeActivo = true;
        inGame = false;
        enPausa = false;
        tiempo = 0;
        CrearPartidaMeta();
        Time.timeScale = 0;
        canvasScript.instance.EnJuego();
        if (PreSesionDeJuego != null)
            PreSesionDeJuego();
        StartCoroutine(CuentaRegresivaPreSesionDeJuego());
    }

    public static void ReiniciarNivel()
    {
        enPausa = true;
        Pausa();
        SelectorNivel.ReiniciarNivel();
    }

    public static void Pausa()
    {
        if (!enPausa)
        {
            canvasScript.instance.Pausa(true);
            swipeActivo = false;
            Time.timeScale = 0;
        }
        else
        {
            canvasScript.instance.Pausa(false);
            swipeActivo = true;
            Time.timeScale = 1;
        }
        enPausa = !enPausa;
        if (pausado != null)
            pausado(enPausa);
    }

    public static void FinNivel()
    {
        swipeActivo = false;
        inGame = false;
        if (FinSesionDeJuego != null)
            FinSesionDeJuego();
    }


    void ActivarPantallaFinDeNivel()
    {
        StartCoroutine(CuentaRegresivaFinal());
    }
     

    IEnumerator CuentaRegresivaFinal()
    {
        yield return new WaitForSeconds(1.7f);
        canvasScript.instance.NivelTerminado();
        if (PostFinSesionDeJuego != null) PostFinSesionDeJuego();
    }


    public static void CargarSiguienteNivel()
    {
        SelectorNivel.CargarNivel(SceneManager.GetActiveScene().buildIndex + 1);
    }


     float getTime()
    {
        return tiempo;
    }

}
