using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour {

    static MenuPausa instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameController.pausado += Activar;
        }
    }


    void Activar(bool enPausa)
    {
        if (enPausa)
        {
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }
}
