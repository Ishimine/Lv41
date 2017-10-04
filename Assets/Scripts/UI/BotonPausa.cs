using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonPausa : MonoBehaviour {

    static BotonPausa instance;


    public Sprite pausaSprite;
    public Sprite playSprite;
    Image render;

    public AudioSource audio;
    public AudioClip pausaIn;
    public AudioClip pausaOut;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            render = GetComponent<Image>(); CheckPausa();
            SelectorNivel.NivelCargado += CheckPausa;
            audio = GetComponent<AudioSource>();
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
            audio.clip = pausaIn;
            if (GameController.inGame)
            {
                audio.Play();
                MusicDJ.instance.audio.Pause();
            }
        }
        else
        {

            render.sprite = pausaSprite;
            audio.clip = pausaOut;
            if (GameController.inGame)
            {
                audio.Play();
                MusicDJ.instance.audio.UnPause();
            }
        }

    }
}
