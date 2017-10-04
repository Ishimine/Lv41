using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosPorArreglo : MonoBehaviour {

    public AudioSource audio1;
    public AudioSource audio2;
    public AudioClip[] audios;



    public void ReproducirAudio(int pista, int i)
    {
        if (audios.Length >= i)
        {
            if (pista == 0)
            {
                audio1.clip = audios[i];
                audio1.Play();
            }
            else
            {
                audio2.clip = audios[i];
                audio2.Play();
            }
        }
    }
}
