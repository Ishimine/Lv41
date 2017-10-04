using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicDJ : MonoBehaviour {

    public AudioClip[] pistas;
    public AudioSource audio;
    public AudioLowPassFilter low;

    public static MusicDJ instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        GameController.PreSesionDeJuego += Iniciarlizar;
        Reproducir(0);
    }

    public void LowPass(bool x)
    {
        low.enabled = x;
    }
    

    public void MedioVolumen()
    {
        audio.volume = .5f;
    }

    private void OnDestroy()
    {
        GameController.PreSesionDeJuego -= Iniciarlizar;
    }

    public void Iniciarlizar()
    {
        LowPass(false);
        audio.volume = 1;
        int i = SceneManager.GetActiveScene().buildIndex;

        if (i == 0)
        {
            Reproducir(i);
        }
        else
            Reproducir(Random.Range(1, pistas.Length) - 1);
        /*
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
        }*/
    }

    public void Reproducir(int id)
    {
        if (audio.clip == pistas[id])
            return;
        audio.clip = pistas[id];
        audio.Play();
    }

}
