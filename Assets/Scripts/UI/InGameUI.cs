using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour {

    public CuentaRegresiva contador;
    public GameObject finDeNivel;
    public ContadorUI tiempo;
    public GameObject menuPausa;

    public void Reiniciar()
    {
        finDeNivel.SetActive(false);
        tiempo.gameObject.SetActive(true);
        menuPausa.SetActive(false);
        tiempo.Restart();
    }

    

}
