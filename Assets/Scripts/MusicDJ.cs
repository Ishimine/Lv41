using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicDJ : MonoBehaviour {

    public AudioClip[] pistas;
    public AudioSource audio;



    private void Awake()
    {
        GameController.PreSesionDeJuego += Iniciarlizar;
        Reproducir(0);
    }

    private void OnDestroy()
    {
        GameController.PreSesionDeJuego -= Iniciarlizar;

    }

    public void Iniciarlizar()
    {

        int i = SceneManager.GetActiveScene().buildIndex;

        if(i==0)
        {
            Reproducir(i);
        }   
        else if(i < 11)
        {
            Reproducir(1);
        }
        else if (i < 21)
        {
            Reproducir(2);
        }
        else
        {
            Reproducir(3);
        }
    }

    public void Reproducir(int id)
    {
        if (audio.clip == pistas[id])
            return;
        audio.clip = pistas[id];
        audio.Play();
    }

}
