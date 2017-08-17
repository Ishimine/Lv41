using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonPausa : MonoBehaviour {

    static BotonPausa instance;


    public Sprite pausaSprite;
    public Sprite playSprite;
    Image render;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            render = GetComponent<Image>(); CheckPausa();
            SelectorNivel.NivelCargado += CheckPausa;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Pausa()
    {
        if (!GameController.inGame)
            return;
        GameController.Pausa();
        CheckPausa();
    }

    void CheckPausa()
    {
        if (GameController.enPausa)
        {
            render.sprite = playSprite;
        }
        else
        {
            render.sprite = pausaSprite;
        }
    }
}
