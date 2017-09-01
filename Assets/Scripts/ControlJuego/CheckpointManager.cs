using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour {

    static int muertes = 0;

    public static CheckpointManager instance;
    public CheckPoint[] puntos;
    public static int CheckPointActual = -1;
    public static EsferaJugador player;

    public delegate void act(int i);
    public static act muerte;

    public static int getMuertes()
    {
        return muertes;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
        SceneManager.sceneLoaded += NivelCargado;
    }

    public static void Reiniciar()
    {
        foreach(CheckPoint cp in instance.puntos)
        {
            cp.PuntoDesactivado();
        }
        CheckPointActual = -1;
        instance.LastCheckPoint();
        muertes = 0;

        if (muerte != null) muerte(0);
    }

    void NivelCargado(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            muertes = 0;
            CheckPointActual = -1;
            player = FindObjectOfType<EsferaJugador>();
            player.muerto += LastCheckPoint;
            puntos = FindObjectsOfType<CheckPoint>();            
        }
    }

    public static void UsarUltimoCP()
    {
        instance.LastCheckPoint();
    }

        public void LastCheckPoint()
    {

        player.trail.SetActive(false);
        muertes++;
        if (muerte != null) muerte(muertes);
        Vector3 target;

        if (CheckPointActual != -1)
        { target = puntos[CheckPointActual].transform.position; }
        else
        { target = FindObjectOfType<GameController>().pPartida.transform.position; }
        TouchControl.instance.OcultarBarras();
        player.transform.position = target;        
        //Camera.main.GetComponent<SeguirObjetivo>().PosicionarCamara(target);
        player.GetComponent<Rigidbody2D>().velocity = Physics2D.gravity.normalized*-1;
        player.GetComponent<Rigidbody2D>().angularVelocity = 0;
        DirectorGravedad.ReestablecerGravedadInstantaneo();
        //player.activarTrail();
        player.trail.SetActive(true);
        player.dead = false;
        player.RevivirJugador();

        Camera.main.transform.position = target - Vector3.forward * Camera.main.transform.position.z;

        Camera.main.GetComponent<CameraFollow>().SetCenter(target);


        TouchControl.BorrarBarras();


    }

    public static void ActualizarPunto(int i)
    {
        if (i > CheckPointActual)
        {
            CheckPointActual = i;
            Debug.Log("CP Actualizado a " + i);
        }

    }


}
