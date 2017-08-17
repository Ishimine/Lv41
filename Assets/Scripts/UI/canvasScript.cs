using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class canvasScript : MonoBehaviour {
    
    Animator anim;
    public GameObject inGameUI;
    public GameObject menuPrincipal;    
    public GameObject eventSys;
   public static canvasScript instance;

    public pantallaFinNivel pFinDeNivel;

    


    void Awake()
    {
        if (instance == null)
        {
            anim = GetComponent<Animator>();
            instance = this;
            GameController.pausado += Pausado;
        }      
        else
        {
            Destroy(eventSys);
            Destroy(this.gameObject);
        }
    }

    public void Pausa(bool x)
    {
        if (x) anim.SetTrigger("PausaIn");
        else anim.SetTrigger("InGame");
    }
    public void Home()
    {
        anim.SetTrigger("NivelesOut");
    }

    public void EnJuego()
    {
        anim.SetTrigger("InGame");
    }

    
    public void CargandoEscena()
    {
        anim.SetTrigger("PantallaCarga");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            menuPrincipal.SetActive(true);
            inGameUI.SetActive(false);
        }
        else
        {
            inGameUI.SetActive(true);
            menuPrincipal.SetActive(false);
            reiniciarInGameUI();
        }
    }

    void NivelCargado(Scene scene, LoadSceneMode mode)
    {

        if (scene.buildIndex == 0)
        {
            menuPrincipal.SetActive(true);
            inGameUI.SetActive(false);
        }
        else
        {
            //pFinDeNivel.ResetAnimaciones();

            inGameUI.SetActive(true);
            menuPrincipal.SetActive(false);
        }
        anim.SetTrigger("Reset");
    }


    public void NivelTerminado()
    {
        anim.SetTrigger("FinDeNivelIn");
    }
    



    void reiniciarInGameUI()
    {
        inGameUI.GetComponent<InGameUI>().Reiniciar();
    }


    void Pausado(bool x)
    {
        if(x)
            anim.SetTrigger("PausaIn");
        else
            anim.SetTrigger("PausaOut");
    }



}
